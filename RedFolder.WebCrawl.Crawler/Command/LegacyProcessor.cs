using RedFolder.WebCrawl.Crawler.Models;

namespace RedFolder.WebCrawl.Crawler
{
    public class LegacyProcessor : IProcessUrl
    {
        public UrlInfo Process(string url)
        {
            if (CanBeHandled(url))
            {
                return new UrlInfo
                {
                    Url = url,
                    InvalidationMessage = "Legacy Reference",
                    UrlType = UrlInfo.UrlTypes.Legacy
                };
            }

            return null;
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
