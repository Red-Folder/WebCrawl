using RedFolder.WebCrawl.Crawler.Helpers;
using RedFolder.WebCrawl.Crawler.Models;
using System;

namespace RedFolder.WebCrawl.Crawler
{
    public class PodcastRoadmapProcessor : HttpClientBaseProcessor
    {
        public PodcastRoadmapProcessor(IClientWrapper httpClient) : base(httpClient)
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
            return url.Contains("/podcasts/roadmap");
        }

        private UrlInfo Handle(string url)
        {
            HttpGet(url);

            return new UrlInfo
            {
                Url = url,
                InvalidationMessage = LastHttpStatusCode == System.Net.HttpStatusCode.Redirect ? null : $"Unexpected Status code: {LastHttpStatusCode}"
            };
        }
    }
}
