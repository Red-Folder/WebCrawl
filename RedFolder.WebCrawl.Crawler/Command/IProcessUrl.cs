using RedFolder.WebCrawl.Crawler.Models;

namespace RedFolder.WebCrawl.Crawler
{
    public interface IProcessUrl
    {
        UrlInfo Process(string url);
        IProcessUrl Next(IProcessUrl nextProcessor);
    }
}
