using RedFolder.WebCrawl.Crawler.Helpers;
using RedFolder.WebCrawl.Crawler.Models;
using System;

namespace RedFolder.WebCrawl.Crawler
{
    public class ContentProcessor : HttpClientBaseProcessor
    {
        public ContentProcessor(IClientWrapper httpClient) : base(httpClient)
        {
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
            if (url.EndsWith(".css")) return true;
            if (url.EndsWith(".js")) return true;

            return false;
        }

        private UrlInfo Handle(string url)
        {
            HttpGet(url);

            return new UrlInfo
            {
                Url = url,
                InvalidationMessage = LastHttpStatusCode == System.Net.HttpStatusCode.OK ? "" : $"Unexpected Status code: {LastHttpStatusCode}"
            };
        }
    }
}
