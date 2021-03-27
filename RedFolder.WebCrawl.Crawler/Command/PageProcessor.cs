using RedFolder.WebCrawl.Crawler.Helpers;
using RedFolder.WebCrawl.Crawler.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace RedFolder.WebCrawl.Crawler
{
    public class PageProcessor : IProcessUrl
    {
        private readonly string _domain;
        private readonly ILinksExtractor _linksExtrator;
        private readonly IHttpClientFactory _clientFactory;

        public PageProcessor(string domain, IHttpClientFactory clientFactory, ILinksExtractor linksExtractor)
        {
            _domain = domain;
            _linksExtrator = linksExtractor;
            _clientFactory = clientFactory;
        }

        public UrlInfo Process(string url)
        {
            if (CanBeHandled(url))
            {
                return Handle(url);
            }

            return null;
        }

        private bool CanBeHandled(string url)
        {
            return url.StartsWith(_domain);
        }

        private UrlInfo Handle(string url)
        {
            var httpClient = _clientFactory.CreateClient("default");
            var response = httpClient.GetAsync(url).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.MovedPermanently)
            {
                if (_linksExtrator == null)
                {
                    return new UrlInfo
                    {
                        Url = url,
                        UrlType = UrlInfo.UrlTypes.Page
                    };
                }
                else
                {
                    IList<string> links = null;
                    if (_linksExtrator is ContentLinksExtractor)
                    {
                        links = _linksExtrator.Extract(response.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        links = _linksExtrator.Extract(response.Headers.GetValues("location").FirstOrDefault());
                    }

                    return new UrlInfo
                    {
                        Url = url,
                        Links = links,
                        UrlType = UrlInfo.UrlTypes.Page
                    };
                }
            }
            else
            {
                return new UrlInfo
                {
                    Url = url,
                    InvalidationMessage = $"Unexpected Status code: {response.StatusCode}",
                    UrlType = UrlInfo.UrlTypes.Page
                };
            }
        }
    }
}
