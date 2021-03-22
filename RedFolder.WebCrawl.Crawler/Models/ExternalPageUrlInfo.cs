namespace RedFolder.WebCrawl.Crawler.Models
{
    class ExternalPageUrlInfo : BaseUrlInfo
    {
        public ExternalPageUrlInfo(string url)
        {
            _url = url;
        }

        public ExternalPageUrlInfo(string url, string invalidationMessage)
        {
            _url = url;
            _invalidationMessage = invalidationMessage;
        }
    }
}
