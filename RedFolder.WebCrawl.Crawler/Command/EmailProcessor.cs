using RedFolder.WebCrawl.Crawler.Models;
using System.Collections.Generic;

namespace RedFolder.WebCrawl.Crawler
{
    public class EmailProcessor : BaseProcessor
    {
        private IList<string> _knownEmails;

        public EmailProcessor()
        {
            _knownEmails = PopulateKnownEmails();
        }

        public override UrlInfo Process(string url)
        {
            if (CanBeHandled(url))
            {
                return new UrlInfo
                {
                    Url = url
                };
            }
            else
            {
                return base.Process(url);
            }
        }

        private bool CanBeHandled(string url)
        {
            if (_knownEmails.Contains(url)) return true;

            return false;
        }

        private IList<string> PopulateKnownEmails()
        {
            return new List<string>
            {
                @"mailto:mark@red-folder.com"
            };
        }
    }
}
