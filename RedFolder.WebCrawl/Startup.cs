using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RedFolder.WebCrawl.Crawler;
using RedFolder.WebCrawl.Crawler.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;

[assembly: FunctionsStartup(typeof(RedFolder.WebCrawl.Startup))]

namespace RedFolder.WebCrawl
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddHttpClient("default")
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    AllowAutoRedirect = false
                });

            builder.Services.AddTransient<IHttpClientWrapper, HttpClientWrapper>();

            builder.Services.AddTransient(provider =>
            {
                return new Func<string, SortedList<int, IProcessUrl>>((string host) =>
                {
                    var githubDomain = @"https://github.com/red-folder";
                    var gistDomain = @"https://gist.github.com";

                    var internalDomains = new List<string>
                    {
                        host,
                        githubDomain
                    };

                    return new SortedList<int, IProcessUrl>
                    {
                        { 100, new CloudflareCgiProcesser() },
                        { 200, new LegacyProcessor() },
                        { 300, new ImageProcessor(provider.GetService<IHttpClientWrapper>()) },
                        { 400, new ContentProcessor(provider.GetService<IHttpClientWrapper>()) },
                        { 500, new KnownPageProcessor() },
                        { 600, new EmailProcessor() },
                        { 700, new ExternalPageProcessor(internalDomains) },
                        { 800, new PodcastRoadmapProcessor(provider.GetService<IHttpClientWrapper>()) },
                        { 900, new PageProcessor(gistDomain, provider.GetService<IHttpClientWrapper>(), null) },
                        { 1000, new PageProcessor(githubDomain, provider.GetService<IHttpClientWrapper>(), null) },
                        { 1100, new PageProcessor(host, provider.GetService<IHttpClientWrapper>(), new ContentLinksExtractor(host)) }
                    };
                });
            });

            builder.Services.AddTransient(provider =>
            {
                return new Func<string, Crawler.Crawler>((string host) =>
                {
                    var processorsFactory = provider.GetService<Func<string, SortedList<int, IProcessUrl>>>();
                    var processors = processorsFactory(host);
                    var logger = provider.GetService<ILogger<Crawler.Crawler>>();

                    return new Crawler.Crawler(processors, logger);
                });
            });
        }
    }
}
