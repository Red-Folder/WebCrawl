using RedFolder.WebCrawl.Crawler.Models;
using System.Collections.Generic;

namespace RedFolder.WebCrawl.Crawler.Helpers
{
    public class RedirectLinksExtractor : ILinksExtractor
    {
        public IList<IUrlInfo> Extract(string url)
        {
            var links = new List<IUrlInfo>();
            links.Add(new AwaitingProcessingUrlInfo(url.Trim()));
            return links;
        }
    }
}
