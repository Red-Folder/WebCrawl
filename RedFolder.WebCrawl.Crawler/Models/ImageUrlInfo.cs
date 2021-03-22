namespace RedFolder.WebCrawl.Crawler.Models
{
    public class ImageUrlInfo : BaseUrlInfo
    {
        public ImageUrlInfo(string url)
        {
            _url = url;
        }

        public ImageUrlInfo(string url, string invalidationMessage)
        {
            _url = url;
            _invalidationMessage = invalidationMessage;
        }
    }
}
