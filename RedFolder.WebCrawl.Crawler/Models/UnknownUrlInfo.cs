namespace RedFolder.WebCrawl.Crawler.Models
{
    public class UnknownUrlInfo : BaseUrlInfo
    {
        public UnknownUrlInfo(string url)
        {
            _url = url;
            _invalidationMessage = "Unknown url type";
        }
    }
}
