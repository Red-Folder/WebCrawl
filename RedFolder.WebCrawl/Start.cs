using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DurableTask.Core;
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
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            var request = await req.Content.ReadAsAsync<CrawlRequest>();
            if (request?.Host == null)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("Host must be provided")
                };
            }

            // Purge history until https://github.com/Azure/azure-functions-durable-extension/issues/892 is available
            var purgeResult = await starter.PurgeInstanceHistoryAsync(DateTime.MinValue, null, new[]
            {
                OrchestrationStatus.Completed,
                OrchestrationStatus.Canceled,
                OrchestrationStatus.Failed,
                OrchestrationStatus.Terminated
            });
            log.LogInformation($"History purge removed {purgeResult.InstancesDeleted} instances");

            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("Crawl", request);
            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}