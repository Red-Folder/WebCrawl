using RedFolder.WebCrawl.Crawler.Models;
using System.Collections.Generic;
using System.Linq;

namespace RedFolder.WebCrawl.Crawler
{
    public class ExternalPageProcessor : BaseProcessor
    {
        private IList<string> _internalDomains;

        public ExternalPageProcessor(IList<string> internalDomains)
        {
            _internalDomains = internalDomains;
        }

        public override UrlInfo Process(string url)
        {
            if (CanBeHandled(url))
            {
                return new UrlInfo
                {
                    Url = url
                };
            }
            else
            {
                return base.Process(url);
            }
        }

        private bool CanBeHandled(string url)
        {
            if (_internalDomains.Where(x => url.StartsWith(x)).Count() > 0) return false;

            return true;
        }
    }
}
