using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using RedFolder.WebCrawl.Crawler.Models;

namespace RedFolder.WebCrawl
{
    public static class Crawl
    {
        [FunctionName("Crawl")]
        public static async Task<CrawlResults> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context,
            ILogger log)
        {
            var request = context.GetInput<CrawlRequest>();

            var crawler = new Crawler.Crawler(request, log);
            crawler.AddUrl($"{request.Host}/sitemap.xml");
            var crawlResult = crawler.Crawl();

            return crawlResult;
        }
    }
}