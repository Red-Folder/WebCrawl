using RedFolder.WebCrawl.Crawler.Models;
using System.Collections.Generic;

namespace RedFolder.WebCrawl.Crawler.Helpers
{
    public interface ILinksExtractor
    {
        IList<IUrlInfo> Extract(string content);
    }
}
