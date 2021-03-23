using RedFolder.WebCrawl.Crawler.Models;
using System.Collections.Generic;
using System.Linq;

namespace RedFolder.WebCrawl.Crawler
{
    public class ExternalPageProcessor : IProcessUrl
    {
        private readonly IList<string> _internalDomains;

        public ExternalPageProcessor(IList<string> internalDomains)
        {
            _internalDomains = internalDomains;
        }

        public UrlInfo Process(string url)
        {
            if (CanBeHandled(url))
            {
                return new UrlInfo
                {
                    Url = url,
                    UrlType = UrlInfo.UrlTypes.ExternalPage
                };
            }

            return null;
        }

        private bool CanBeHandled(string url)
        {
            if (_internalDomains.Where(x => url.StartsWith(x)).Count() > 0) return false;

            return true;
        }
    }
}
