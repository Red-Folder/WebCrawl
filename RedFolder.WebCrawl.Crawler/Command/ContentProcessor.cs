using RedFolder.WebCrawl.Crawler.Helpers;
using RedFolder.WebCrawl.Crawler.Models;

namespace RedFolder.WebCrawl.Crawler
{
    public class ContentProcessor : IProcessUrl
    {
        private readonly IHttpClientWrapper _httpClient;

        public ContentProcessor(IHttpClientWrapper httpClient)
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
            if (url.EndsWith(".css")) return true;
            if (url.EndsWith(".js")) return true;

            return false;
        }

        private UrlInfo Handle(string url)
        {
            _httpClient.Get(url);

            return new UrlInfo
            {
                Url = url,
                InvalidationMessage = _httpClient.LastHttpStatusCode == System.Net.HttpStatusCode.OK ? "" : $"Unexpected Status code: {_httpClient.LastHttpStatusCode}",
                UrlType = UrlInfo.UrlTypes.Content
            };
        }
    }
}
