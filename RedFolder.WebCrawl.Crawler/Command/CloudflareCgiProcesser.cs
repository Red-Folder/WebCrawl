using RedFolder.WebCrawl.Crawler.Models;

namespace RedFolder.WebCrawl.Crawler
{
    public class CloudflareCgiProcesser : IProcessUrl
    {
        public UrlInfo Process(string url)
        {
            if (CanBeHandled(url))
            {
                return new UrlInfo
                {
                    Url = url,
                    UrlType = UrlInfo.UrlTypes.CloudflareCgi
                };
            }

            return null;
        }

        private bool CanBeHandled(string url)
        {
            if (url.StartsWith(@"https://www.red-folder.com/cdn-cgi")) return true;

            return false;
        }
    }
}
