using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using RedFolder.WebCrawl.Crawler.Models;

namespace RedFolder.WebCrawl
{
    public static class Start
    {
        [FunctionName("Start")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            var request = await req.Content.ReadAsAsync<CrawlRequest>();
            if (request == null) request = new CrawlRequest();
            request.Host = request.Host ?? "https://red-folder.com";

            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("Crawl", request);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}