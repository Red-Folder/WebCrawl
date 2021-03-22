using System.Collections.Generic;

namespace RedFolder.WebCrawl.Crawler.Helpers
{
    public interface ILinksExtractor
    {
        IList<string> Extract(string content);
    }
}
