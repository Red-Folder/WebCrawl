using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using RedFolder.WebCrawl.Crawler.Models;

namespace RedFolder.WebCrawl
{
    public static class CrawlUrl
    {
        [FunctionName("CrawlUrl")]
        public static UrlInfo Run([ActivityTrigger] string url, ILogger log)
        {
            var crawler = new Crawler.Crawler("https://red-folder.com", log);

            return crawler.Crawl(url);
        }
    }
}