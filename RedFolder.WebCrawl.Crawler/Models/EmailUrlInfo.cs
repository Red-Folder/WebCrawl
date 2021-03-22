namespace RedFolder.WebCrawl.Crawler.Models
{
    public class EmailUrlInfo : BaseUrlInfo
    {
        public EmailUrlInfo(string url)
        {
            _url = url;
        }

        public EmailUrlInfo(string url, string invalidationMessage)
        {
            _url = url;
            _invalidationMessage = invalidationMessage;
        }
    }
}
