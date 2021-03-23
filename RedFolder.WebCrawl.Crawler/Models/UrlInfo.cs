using System.Collections.Generic;

namespace RedFolder.WebCrawl.Crawler.Models
{
    public partial class UrlInfo
    {
        public string Url { get; set; }
        public string InvalidationMessage { get; set; }
        public IList<string> Links { get; set; }

        public UrlTypes UrlType { get; set; }

        public bool HasLinks => Links != null && Links.Count > 0;

        public bool Valid => string.IsNullOrEmpty(InvalidationMessage);

        public override string ToString()
        {
            if (Valid)
            {
                return $"{Url} - Valid";
            }
            else
            {
                return $"{Url} - {InvalidationMessage}";
            }
        }

        public enum UrlTypes
        {
            Unknown,
            CloudflareCgi,
            Content,
            Email,
            ExternalPage,
            Image,
            KnownPage,
            Legacy,
            PodcastRoadmap,
            Page,
            Exception
        }
    }
}
