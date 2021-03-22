using RedFolder.WebCrawl.Crawler.Models;

namespace RedFolder.WebCrawl.Crawler
{
    public class UnknownProcessor : BaseProcessor
    {
        public override UrlInfo Process(string url)
        {
            return new UrlInfo
            {
                Url = url,
                InvalidationMessage = "Unknown url type"
            };
        }
    }
}
