using RedFolder.WebCrawl.Crawler.Models;
using System.Threading.Tasks;

namespace RedFolder.WebCrawl.Crawler
{
    public interface IProcessUrl
    {
        Task<UrlInfo> Process(string url);
    }
}
