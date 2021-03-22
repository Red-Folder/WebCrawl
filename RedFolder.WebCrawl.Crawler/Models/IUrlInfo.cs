using System.Collections.Generic;

namespace RedFolder.WebCrawl.Crawler.Models
{
    public interface IUrlInfo
    {
        string Url { get; }
        bool Valid { get; }
        string InvalidationMessage { get; }
        bool HasLinks { get; }
        IList<IUrlInfo> Links { get; }
    }
}
