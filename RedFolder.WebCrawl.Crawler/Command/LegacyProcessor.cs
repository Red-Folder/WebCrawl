using RedFolder.WebCrawl.Crawler.Models;
using System;

namespace RedFolder.WebCrawl.Crawler
{
    public class LegacyProcessor : BaseProcessor
    {
        public override UrlInfo Process(string url)
        {
            if (CanBeHandled(url))
            {
                return new UrlInfo
                {
                    Url = url,
                    InvalidationMessage = "Legacy Reference"
                };
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
    }
}
