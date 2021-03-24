using System.Collections.Generic;

namespace RedFolder.WebCrawl.Crawler.Models
{
    public class CrawlRequest
    {
        public string Host { get; set; }

        public List<string> HostSynonyms { get; set; }
    }
}
