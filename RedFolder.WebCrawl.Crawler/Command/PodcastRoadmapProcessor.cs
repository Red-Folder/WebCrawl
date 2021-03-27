using RedFolder.WebCrawl.Crawler.Models;
using System.Net.Http;

namespace RedFolder.WebCrawl.Crawler
{
    public class PodcastRoadmapProcessor : IProcessUrl
    {
        private readonly IHttpClientFactory _clientFactory;

        public PodcastRoadmapProcessor(IHttpClientFactory clientFatory)
        {
            _clientFactory = clientFatory;
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
            var httpClient = _clientFactory.CreateClient("default");
            var response = httpClient.GetAsync(url).Result;

            return new UrlInfo
            {
                Url = url,
                InvalidationMessage = response.StatusCode == System.Net.HttpStatusCode.Redirect ? null : $"Unexpected Status code: {response.StatusCode}",
                UrlType = UrlInfo.UrlTypes.PodcastRoadmap
            };
        }
    }
}
