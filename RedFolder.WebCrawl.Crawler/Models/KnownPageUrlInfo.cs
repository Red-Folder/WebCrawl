namespace RedFolder.WebCrawl.Crawler.Models
{
    public class KnownPageUrlInfo : BaseUrlInfo
    {
        public KnownPageUrlInfo(string url)
        {
            _url = url;
        }

        public KnownPageUrlInfo(string url, string invalidationMessage)
        {
            _url = url;
            _invalidationMessage = invalidationMessage;
        }
    }
}
