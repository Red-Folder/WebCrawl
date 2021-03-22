namespace RedFolder.WebCrawl.Crawler.Models
{
    public class Link
    {
        public string Source { get; private set; }
        public string Target { get; private set; }

        public Link(string source, string target)
        {
            Source = source;
            Target = target;
        }
    }
}
