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

        public override IUrlInfo Process(string url)
        {
            if (CanBeHandled(url))
            {
                return Handle(url);
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

        private IUrlInfo Handle(string url)
        {
            return new EmailUrlInfo(url);
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
