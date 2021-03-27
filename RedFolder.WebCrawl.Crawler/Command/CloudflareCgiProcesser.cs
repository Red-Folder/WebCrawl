using RedFolder.WebCrawl.Crawler.Models;
using System.Threading.Tasks;

namespace RedFolder.WebCrawl.Crawler
{
    public class CloudflareCgiProcesser : IProcessUrl
    {
        public Task<UrlInfo> Process(string url)
        {
            if (CanBeHandled(url))
            {
                return Task.FromResult(new UrlInfo
                {
                    Url = url,
                    UrlType = UrlInfo.UrlTypes.CloudflareCgi
                });
            }

            return Task.FromResult<UrlInfo>(null);
        }

        private bool CanBeHandled(string url)
        {
            if (url.StartsWith(@"https://www.red-folder.com/cdn-cgi")) return true;

            return false;
        }
    }
}
