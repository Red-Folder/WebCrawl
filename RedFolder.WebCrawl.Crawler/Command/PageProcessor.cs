using RedFolder.WebCrawl.Crawler.Helpers;
using RedFolder.WebCrawl.Crawler.Models;
using System.Collections.Generic;
using System.Linq;

namespace RedFolder.WebCrawl.Crawler
{
    public class PageProcessor : HttpClientBaseProcessor
    {
        private string _domain;
        private ILinksExtractor _linksExtrator;

        public PageProcessor(string domain, IClientWrapper httpClient, ILinksExtractor linksExtractor) : base(httpClient)
        {
            _domain = domain;
            _linksExtrator = linksExtractor;
        }

        public override UrlInfo Process(string url)
        {
            if (CanBeHandled(url))
            {
                return Handle(url);
            }
            else
            {
                return base.Process(url);
            }
        }

        private bool CanBeHandled(string url)
        {
            return url.StartsWith(_domain);
        }

        private UrlInfo Handle(string url)
        {
            HttpGet(url);
            if (LastHttpStatusCode == System.Net.HttpStatusCode.OK || LastHttpStatusCode == System.Net.HttpStatusCode.MovedPermanently)
            {
                if (_linksExtrator == null)
                {
                    return new UrlInfo
                    {
                        Url = url
                    };
                }
                else
                {
                    IList<string> links = null;
                    if (_linksExtrator is ContentLinksExtractor)
                    {
                        links = _linksExtrator.Extract(LastHttpResponse);
                    }
                    else
                    {
                        links = _linksExtrator.Extract(LastHttpResponseHeaders.GetValues("location").FirstOrDefault());
                    }

                    return new UrlInfo
                    {
                        Url = url,
                        Links = links
                    };
                }
            }
            else
            {
                return new UrlInfo
                {
                    Url = url,
                    InvalidationMessage = $"Unexpected Status code: {LastHttpStatusCode}"
                };
            }
        }
    }
}
