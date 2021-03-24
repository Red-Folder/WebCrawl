using System.Collections.Generic;
using System.Linq;

namespace RedFolder.WebCrawl.Crawler.Models
{
    public class CrawlState
    {
        private readonly CrawlRequest _configuration;

        private List<string> _toCrawl = new List<string>();
        private Dictionary<string, UrlInfo> _crawled = new Dictionary<string, UrlInfo>();

        public CrawlState(CrawlRequest configuration)
        {
            _configuration = configuration;
        }

        public void AddUrl(string url)
        {
            _toCrawl.Add(ConvertSynonyms(url));
        }

        public bool HasAwaiting => Awaiting().Count > 0;

        public IReadOnlyList<string> Awaiting()
        {
            return _toCrawl.AsReadOnly();
        }

        public IReadOnlyList<Url> AllUrls()
        {
            return _crawled
                    .Values
                    .Select(x => new Url(x.Url, x.Valid, x.InvalidationMessage))
                    .ToList()
                    .AsReadOnly();
        }

        public IReadOnlyList<Link> AllLinks()
        {
            return _crawled
                    .Values.Where(x => x.HasLinks)
                    .SelectMany(x => x.Links.Select(y => new Link(x.Url, y)))
                    .ToList()
                    .AsReadOnly();
        }

        public void UpdateWithResults(List<UrlInfo> results)
        {
            // Remove crawled urls
            results.ForEach(x => _toCrawl.Remove(x.Url));

            // Append the results
            foreach (var urlInfo in results)
            {
                urlInfo.Links = ConvertSynonyms(urlInfo.Links);

                if (_crawled.ContainsKey(urlInfo.Url))
                {
                    _crawled[urlInfo.Url] = urlInfo;
                }
                else
                {
                    _crawled.Add(urlInfo.Url, urlInfo);
                }
            }

            // Populate with any new links
            var newUrls = results.Where(x => x.HasLinks).SelectMany(x => x.Links).Distinct();
            var newUrlsToAdd = newUrls
                                .Where(x => !_crawled.ContainsKey(x))
                                .Where(x => !_toCrawl.Contains(x))
                                .Select(x => ConvertSynonyms(x))
                                .ToList();
            _toCrawl.AddRange(newUrlsToAdd);
        }

        private string ConvertSynonyms(string url)
        {
            if (_configuration.HostSynonyms != null && _configuration.HostSynonyms.Count > 0)
            {
                var foundSynonym = _configuration.HostSynonyms.FirstOrDefault(x => url.StartsWith(x));

                if (foundSynonym != null)
                {
                    return url.Replace(foundSynonym, _configuration.Host);
                }
            }

            return url;
        }

        private IList<string> ConvertSynonyms(IList<string> urls)
        {
            if (urls == null) return null;

            return urls.Select(x => ConvertSynonyms(x)).ToList();
        }
    }
}
