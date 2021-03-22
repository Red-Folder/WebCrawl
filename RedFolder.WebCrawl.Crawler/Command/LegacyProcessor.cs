using RedFolder.WebCrawl.Crawler.Models;
using System;

namespace RedFolder.WebCrawl.Crawler
{
    public class LegacyProcessor : BaseProcessor
    {
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
            // Want to remove old blog links
            if (url.ToLower().Contains("blogspot")) return true;
            if (url.ToLower().Contains("blog.red-folder.com")) return true;

            return false;
        }

        private IUrlInfo Handle(string url)
        {
            return new LegacyUrlInfo(url, String.Format("Legacy reference"));
        }
    }
}
