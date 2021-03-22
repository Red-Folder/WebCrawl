using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using RedFolder.WebCrawl.Crawler.Models;

namespace RedFolder.WebCrawl
{
    public static class Crawl
    {
        private const int MAX_CRAWL_DEPTH = 10;

        [FunctionName("Crawl")]
        public static async Task<CrawlResults> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context,
            ILogger log)
        {
            var request = context.GetInput<CrawlRequest>();

            var state = new CrawlState();
            state.AddUrl($"{request.Host}/sitemap.xml");

            var currentDepth = 0;
            while (state.HasAwaiting && currentDepth < MAX_CRAWL_DEPTH)
            {
                var urlsToCrawl = state.Awaiting();

                var crawlTasks = urlsToCrawl
                                    .Select(x => context.CallActivityAsync<UrlInfo>("CrawlUrl", x))
                                    .ToList();

                await Task.WhenAll(crawlTasks.ToArray());

                var result = crawlTasks.Select(x => x.Result).ToList();

                state.UpdateWithResults(result);

                currentDepth++;
            }

            return new CrawlResults(request.Host, state);
        }
    }
}