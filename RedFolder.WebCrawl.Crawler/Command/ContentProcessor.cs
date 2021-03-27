using RedFolder.WebCrawl.Crawler.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace RedFolder.WebCrawl.Crawler
{
    public class ContentProcessor : IProcessUrl
    {
        private readonly IHttpClientFactory _clientFactory;

        public ContentProcessor(IHttpClientFactory clientFatory)
        {
            _clientFactory = clientFatory;
        }

        public async Task<UrlInfo> Process(string url)
        {
            if (CanBeHandled(url))
            {
                return await Handle(url);
            }

            return null;
        }

        private bool CanBeHandled(string url)
        {
            if (url.EndsWith(".css")) return true;
            if (url.EndsWith(".js")) return true;

            return false;
        }

        private async Task<UrlInfo> Handle(string url)
        {
            var httpClient = _clientFactory.CreateClient("default");
            var response = await httpClient.GetAsync(url);

            return new UrlInfo
            {
                Url = url,
                InvalidationMessage = response.StatusCode == System.Net.HttpStatusCode.OK ? "" : $"Unexpected Status code: {response.StatusCode}",
                UrlType = UrlInfo.UrlTypes.Content
            };
        }
    }
}
