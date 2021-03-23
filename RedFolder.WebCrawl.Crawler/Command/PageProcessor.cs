using RedFolder.WebCrawl.Crawler.Helpers;
using RedFolder.WebCrawl.Crawler.Models;
using System.Collections.Generic;
using System.Linq;

namespace RedFolder.WebCrawl.Crawler
{
    public class PageProcessor : IProcessUrl
    {
        private readonly string _domain;
        private readonly ILinksExtractor _linksExtrator;
        private readonly IHttpClientWrapper _httpClient;

        public PageProcessor(string domain, IHttpClientWrapper httpClient, ILinksExtractor linksExtractor)
        {
            _domain = domain;
            _linksExtrator = linksExtractor;
            _httpClient = httpClient;
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
            _httpClient.Get(url);

            if (_httpClient.LastHttpStatusCode == System.Net.HttpStatusCode.OK || _httpClient.LastHttpStatusCode == System.Net.HttpStatusCode.MovedPermanently)
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
                        links = _linksExtrator.Extract(_httpClient.LastHttpResponse);
                    }
                    else
                    {
                        links = _linksExtrator.Extract(_httpClient.LastHttpResponseHeaders.GetValues("location").FirstOrDefault());
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
                    InvalidationMessage = $"Unexpected Status code: {_httpClient.LastHttpStatusCode}",
                    UrlType = UrlInfo.UrlTypes.Page
                };
            }
        }
    }
}
