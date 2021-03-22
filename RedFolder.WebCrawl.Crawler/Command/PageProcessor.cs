using RedFolder.WebCrawl.Crawler.Helpers;
using RedFolder.WebCrawl.Crawler.Models;
using System;
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

        public override IUrlInfo Process(string url)
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

        private IUrlInfo Handle(string url)
        {
            HttpGet(url);
            if (LastHttpStatusCode == System.Net.HttpStatusCode.OK || LastHttpStatusCode == System.Net.HttpStatusCode.MovedPermanently)
            {
                if (_linksExtrator == null)
                {
                    return new PageUrlInfo(url);
                }
                else
                {
                    IList<IUrlInfo> links = null;
                    if (_linksExtrator is ContentLinksExtractor)
                    {
                        links = _linksExtrator.Extract(LastHttpResponse);
                    }
                    else
                    {
                        links = _linksExtrator.Extract(LastHttpResponseHeaders.GetValues("location").FirstOrDefault());
                    }

                    if (links == null)
                    {
                        return new PageUrlInfo(url);
                    }
                    else
                    {
                        return new PageUrlInfo(url, links);
                    }
                }
            }
            else
            {
                return new PageUrlInfo(url, String.Format("Unexpected Status code: {0}", LastHttpStatusCode));
            }
        }
    }
}
