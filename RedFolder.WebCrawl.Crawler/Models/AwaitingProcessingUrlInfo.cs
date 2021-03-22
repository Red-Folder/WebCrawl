namespace RedFolder.WebCrawl.Crawler.Models
{
    internal class AwaitingProcessingUrlInfo : BaseUrlInfo
    {
        internal AwaitingProcessingUrlInfo(string url)
        {
            _url = url;
            _invalidationMessage = "Awaiting processing";
        }
    }
}
