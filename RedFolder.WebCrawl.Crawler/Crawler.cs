using Microsoft.Extensions.Logging;
using RedFolder.WebCrawl.Crawler.Helpers;
using RedFolder.WebCrawl.Crawler.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedFolder.WebCrawl.Crawler
{
    public class Crawler
    {
        private IDictionary<string, IUrlInfo> urls = new Dictionary<string, IUrlInfo>();
        private int _maxDepth = 10;
        private string _githubDomain = @"https://github.com/red-folder";
        private string _gistDomain = @"https://gist.github.com";

        private string _host;

        private IProcessUrl _processor;

        private string _problems = "";

        public Crawler(CrawlRequest request, ILogger log)
        {
            _host = request.Host;

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

        public void AddUrl(string url)
        {
            urls.Add(url, new AwaitingProcessingUrlInfo(url));
        }

        public CrawlResults Crawl()
        {
            var currentDepth = 0;
            while (urls.Where(x => x.Value is AwaitingProcessingUrlInfo).Count() > 0  && currentDepth < _maxDepth)
            {
                currentDepth++;

                var urlsToAdd = new List<IUrlInfo>();
                foreach (var key in urls.Where(x => x.Value is AwaitingProcessingUrlInfo).Select(x => x.Key))
                {
                    var info = ProcessUrl(key);

                    urlsToAdd.Add(info);
                }

                // Update the Url items
                foreach (var urlInfo in urlsToAdd)
                {
                    urls[urlInfo.Url] = urlInfo;
                }

                // Populate with any new links
                var newUrls = urlsToAdd.Where(x => x.HasLinks).SelectMany(x => x.Links).Distinct();

                var newUrlsToAdd = newUrls.Where(x => !urls.Keys.Contains(x.Url)).ToList();
                foreach (var newUrl in newUrls)
                {
                    if (!urls.Keys.Contains(newUrl.Url))
                    {
                        urls.Add(newUrl.Url, newUrl);
                    }
                }
            }

            return ProduceResult();
        }

        private CrawlResults ProduceResult()
        {
            // Get all the urls
            var tmpUrls = urls.Values.Select(x => new Url(x.Url, x.Valid, x.InvalidationMessage)).ToList();

            // Get all the links
            var tmpLinks = urls.Values.Where(x => x.HasLinks)
                            .SelectMany(x =>
                                x.Links.Select(y => new Link(x.Url, y.Url))).ToList();

            return new CrawlResults( _host, tmpUrls, tmpLinks);
        }


        public bool Valid()
        {
            return _problems.Length == 0;
        }

        private void CheckForProblems()
        {
            StringBuilder builder = new StringBuilder();

            foreach (var url in urls.Keys)
            {
                var urlInfo = urls[url];

                if (!urlInfo.Valid)
                {
                    builder.AppendLine(urlInfo.ToString());

                    var referencedIn = urls.Where(x => x.Value.HasLinks && x.Value.Links.Where(y => y.Url == urlInfo.Url).Count() > 0).Select(x => x.Key);
                    foreach (var reference in referencedIn)
                    {
                        builder.AppendFormat("\tReferenced in: {0}", reference);
                        builder.AppendLine();
                    }
                }

            }

            _problems = builder.ToString();
        }

        private IUrlInfo ProcessUrl(string url)
        {
            return _processor.Process(url);
        }

        public override string ToString()
        {
            return _problems;
        }
    }
}
