using RedFolder.WebCrawl.Crawler.Models;

namespace RedFolder.WebCrawl.Crawler
{
    public class CloudflareCgiProcesser : BaseProcessor
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
            if (url.StartsWith(@"https://www.red-folder.com/cdn-cgi")) return true;

            return false;
        }

        private IUrlInfo Handle(string url)
        {
            return new CloudflareCgiUrlInfo(url);
        }
    }
}
