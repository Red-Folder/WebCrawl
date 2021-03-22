namespace RedFolder.WebCrawl.Crawler.Models
{
    public class ContentUrlInfo : BaseUrlInfo
    {
        public ContentUrlInfo(string url)
        {
            _url = url;
        }

        public ContentUrlInfo(string url, string invalidationMessage)
        {
            _url = url;
            _invalidationMessage = invalidationMessage;
        }
    }
}
