using System;
using System.Collections.Generic;

namespace RedFolder.WebCrawl.Crawler.Models
{
    public class CrawlResults
    {
        public string Host { get; private set; }

        public DateTime Timestamp { get; private set; }

        public IList<Url> Urls { get; private set; }

        public IList<Link> Links { get; private set; }

        public CrawlResults(string host, List<Url> urls, List<Link> links)
        {
            Host = host;
            Timestamp = DateTime.UtcNow;
            Urls = urls;
            Links = links;
        }
    }
}
