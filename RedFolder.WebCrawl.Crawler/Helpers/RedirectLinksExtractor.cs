using System.Collections.Generic;

namespace RedFolder.WebCrawl.Crawler.Helpers
{
    public class RedirectLinksExtractor : ILinksExtractor
    {
        public IList<string> Extract(string url)
        {
            var links = new List<string>();
            links.Add(url.Trim());
            return links;
        }
    }
}
