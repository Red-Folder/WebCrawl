using System.Collections.Generic;
using System.Linq;

namespace RedFolder.WebCrawl.Crawler.Models
{
    public class CrawlState
    {
        private List<string> _toCrawl = new List<string>();
        private Dictionary<string, UrlInfo> _crawled = new Dictionary<string, UrlInfo>();

        public void AddUrl(string url)
        {
            _toCrawl.Add(url);
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
            foreach (var urlInfo in results)
            {
                if (_toCrawl.Contains(urlInfo.Url)) _toCrawl.Remove(urlInfo.Url);

                if (_crawled.ContainsKey(urlInfo.Url))
                {
                    _crawled[urlInfo.Url] = urlInfo;
                }
                else
                {
                    _crawled.Add(urlInfo.Url, urlInfo);
                }

                // Populate with any new links
                var newUrls = results.Where(x => x.HasLinks).SelectMany(x => x.Links).Distinct();

                var newUrlsToAdd = newUrls
                                    .Where(x => !_crawled.ContainsKey(x))
                                    .Where(x => !_toCrawl.Contains(x))
                                    .ToList();
                _toCrawl.AddRange(newUrlsToAdd);
            }
        }
    }
}
