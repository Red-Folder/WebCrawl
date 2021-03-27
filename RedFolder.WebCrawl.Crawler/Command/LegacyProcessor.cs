using RedFolder.WebCrawl.Crawler.Models;
using System.Threading.Tasks;

namespace RedFolder.WebCrawl.Crawler
{
    public class LegacyProcessor : IProcessUrl
    {
        public Task<UrlInfo> Process(string url)
        {
            if (CanBeHandled(url))
            {
                return Task.FromResult(new UrlInfo
                {
                    Url = url,
                    InvalidationMessage = "Legacy Reference",
                    UrlType = UrlInfo.UrlTypes.Legacy
                });
            }

            return Task.FromResult<UrlInfo>(null);
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
