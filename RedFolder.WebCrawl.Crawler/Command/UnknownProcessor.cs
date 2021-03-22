using RedFolder.WebCrawl.Crawler.Models;

namespace RedFolder.WebCrawl.Crawler
{
    public class UnknownProcessor : BaseProcessor
    {
        public override IUrlInfo Process(string url)
        {
            return new UnknownUrlInfo(url);
        }
    }
}
