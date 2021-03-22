using RedFolder.WebCrawl.Crawler.Models;

namespace RedFolder.WebCrawl.Crawler
{
    public partial class BaseProcessor : IProcessUrl
    {
        private IProcessUrl _next = null;

        public IProcessUrl Next(IProcessUrl nextProcessor)
        {
            _next = nextProcessor;
            return this;
        }

        public virtual UrlInfo Process(string url)
        {
            if (_next == null)
            {
                return null;
            }
            else
            {
                return _next.Process(url);
            }
        }
    }
}
