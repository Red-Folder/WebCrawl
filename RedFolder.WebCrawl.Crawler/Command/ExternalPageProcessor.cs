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

        public override IUrlInfo Process(string url)
        {
            if (CanBeHandled(url))
            {
                return Handle(url);
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

        private IUrlInfo Handle(string url)
        {
            return new ExternalPageUrlInfo(url);
        }

        private IList<string> PopulateInternalDomains()
        {
            return new List<string>
            {
                //@"https://github.com/red-folder",
                //@"http://red-folder.blogspot.co.uk/",

            };
        }
    }
}
