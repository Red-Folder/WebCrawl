using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using RedFolder.WebCrawl.Crawler.Models;
using System;

namespace RedFolder.WebCrawl
{
    public class CrawlUrl
    {
        private readonly Func<string, Crawler.Crawler> _crawlerFactory;

        public CrawlUrl(Func<string, Crawler.Crawler> crawlerFactory)
        {
            _crawlerFactory = crawlerFactory;
        }

        [FunctionName("CrawlUrl")]
        public UrlInfo Run([ActivityTrigger] CrawlUrlRequest request)
        {
            var crawler = _crawlerFactory(request.Host);

            return crawler.Crawl(request.Url);
        }
    }
}