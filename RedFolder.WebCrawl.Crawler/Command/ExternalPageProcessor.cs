using RedFolder.WebCrawl.Crawler.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedFolder.WebCrawl.Crawler
{
    public class ExternalPageProcessor : IProcessUrl
    {
        private readonly IList<string> _internalDomains;

        public ExternalPageProcessor(IList<string> internalDomains)
        {
            _internalDomains = internalDomains;
        }

        public Task<UrlInfo> Process(string url)
        {
            if (CanBeHandled(url))
            {
                return Task.FromResult(new UrlInfo
                {
                    Url = url,
                    UrlType = UrlInfo.UrlTypes.ExternalPage
                });
            }

            return Task.FromResult<UrlInfo>(null);
        }

        private bool CanBeHandled(string url)
        {
            if (_internalDomains.Where(x => url.StartsWith(x)).Count() > 0) return false;

            return true;
        }
    }
}
