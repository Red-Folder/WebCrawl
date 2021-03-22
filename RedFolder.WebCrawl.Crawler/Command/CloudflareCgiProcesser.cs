using RedFolder.WebCrawl.Crawler.Models;

namespace RedFolder.WebCrawl.Crawler
{
    public class CloudflareCgiProcesser : BaseProcessor
    {
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
            if (url.StartsWith(@"https://www.red-folder.com/cdn-cgi")) return true;

            return false;
        }
    }
}
