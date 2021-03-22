namespace RedFolder.WebCrawl.Crawler.Models
{
    public class Url
    {
        public string Path { get; private set; }

        public bool Valid { get; private set; }
        public string InvalidationMessage { get; private set; }

        public Url(string path, bool valid, string invalidationMessage = "")
        {
            Path = path;
            Valid = valid;
            InvalidationMessage = invalidationMessage;
        }
    }
}
