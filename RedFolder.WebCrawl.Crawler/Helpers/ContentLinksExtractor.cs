using RedFolder.WebCrawl.Crawler.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RedFolder.WebCrawl.Crawler.Helpers
{
    public class ContentLinksExtractor: ILinksExtractor
    {
        private string _domain;

        private IList<string> patterns = new List<string>();

        public ContentLinksExtractor(string domain)
        {
            _domain = domain;

            // Inlcude spaces to avoid JavaScript object setup
            patterns.Add(@" src(\s*)=(\s*)(""|')(?<url>.*?)(""|')");
            patterns.Add(@" href(\s*)=(\s*)(""|')(?<url>.*?)(""|')");
            patterns.Add(@"<loc>(?<url>.*?)</loc>");
        }

        public IList<IUrlInfo> Extract(string content)
        {
            var links = new List<IUrlInfo>();

            foreach (var pattern in patterns)
            {
                foreach (Match match in Regex.Matches(content, pattern, RegexOptions.IgnoreCase))
                {
                    var formattedUrl = Format(match.Groups["url"].Value);
                    links.Add(new AwaitingProcessingUrlInfo(formattedUrl));
                }
            }

            return links;
        }

        private string Format(string url)
        {
            return EnsurePrefixed(url);
        }

        private string EnsurePrefixed(string url)
        {
            if (url.ToLower().StartsWith("http") ||
                url.ToLower().StartsWith("skype") ||
                url.ToLower().StartsWith("mailto"))
            {
                return url;
            }
            else
            {
                if (url.StartsWith("//"))
                {
                    return "https:" + url;
                }
                else
                {
                    if (url.StartsWith("/"))
                    {
                        return _domain + url;
                    }
                    else
                    {
                        return _domain + "/" + url;
                    }
                }
            }
        }
    }
}
