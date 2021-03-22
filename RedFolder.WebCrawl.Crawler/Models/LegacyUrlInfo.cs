namespace RedFolder.WebCrawl.Crawler.Models
{
    public class LegacyUrlInfo : BaseUrlInfo
    {
        public LegacyUrlInfo(string url)
        {
            _url = url;
        }

        public LegacyUrlInfo(string url, string invalidationMessage)
        {
            _url = url;
            _invalidationMessage = invalidationMessage;
        }
    }
}
