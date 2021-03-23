using RedFolder.WebCrawl.Crawler.Helpers;
using RedFolder.WebCrawl.Crawler.Models;

namespace RedFolder.WebCrawl.Crawler
{
    public class PodcastRoadmapProcessor : IProcessUrl
    {
        private readonly IHttpClientWrapper _httpClient;

        public PodcastRoadmapProcessor(IHttpClientWrapper httpClient)
        {
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
            return url.Contains("/podcasts/roadmap");
        }

        private UrlInfo Handle(string url)
        {
            _httpClient.Get(url);

            return new UrlInfo
            {
                Url = url,
                InvalidationMessage = _httpClient.LastHttpStatusCode == System.Net.HttpStatusCode.Redirect ? null : $"Unexpected Status code: {_httpClient.LastHttpStatusCode}",
                UrlType = UrlInfo.UrlTypes.PodcastRoadmap
            };
        }
    }
}
