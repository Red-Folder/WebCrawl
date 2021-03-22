using Microsoft.Extensions.Logging;
using RedFolder.WebCrawl.Crawler.Helpers;
using RedFolder.WebCrawl.Crawler.Models;
using System.Collections.Generic;

namespace RedFolder.WebCrawl.Crawler
{
    public class Crawler
    {
        private string _githubDomain = @"https://github.com/red-folder";
        private string _gistDomain = @"https://gist.github.com";

        private string _host;

        private IProcessUrl _processor;

        public Crawler(string host, ILogger log)
        {
            _host = host;

            var internalDomains = new List<string>
            {
                _host,
                _githubDomain
            };

            _processor = new CloudflareCgiProcesser()
                            .Next(new LegacyProcessor()
                            .Next(new ImageProcessor(new ClientWrapper(log))
                            .Next(new ContentProcessor(new ClientWrapper(log))
                            .Next(new KnownPageProcessor()
                            .Next(new EmailProcessor()
                            .Next(new ExternalPageProcessor(internalDomains)
                            .Next(new PodcastRoadmapProcessor(new ClientWrapper(log)))
                            .Next(new PageProcessor(_gistDomain, new ClientWrapper(log), null)
                            .Next(new PageProcessor(_githubDomain, new ClientWrapper(log), null)
                            .Next(new PageProcessor(_host, new ClientWrapper(log), new ContentLinksExtractor(_host))
                            .Next(new UnknownProcessor()))))))))));
        }

        public UrlInfo Crawl(string url)
        {
            return _processor.Process(url);
        }
    }
}
