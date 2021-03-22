using System;
using System.Collections.Generic;

namespace RedFolder.WebCrawl.Crawler.Models
{
    public class CrawlResults
    {
        public string Host { get; private set; }

        public DateTime Timestamp { get; private set; }

        public IReadOnlyList<Url> Urls { get; private set; }

        public IReadOnlyList<Link> Links { get; private set; }

        public CrawlResults(string host, CrawlState state)
        {
            Host = host;
            Timestamp = DateTime.UtcNow;
            Urls = state.AllUrls();
            Links = state.AllLinks();
        }
    }
}
