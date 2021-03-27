using RedFolder.WebCrawl.Crawler.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedFolder.WebCrawl.Crawler
{
    public class EmailProcessor : IProcessUrl
    {
        private IList<string> _knownEmails;

        public EmailProcessor()
        {
            _knownEmails = PopulateKnownEmails();
        }

        public Task<UrlInfo> Process(string url)
        {
            if (CanBeHandled(url))
            {
                return Task.FromResult(new UrlInfo
                {
                    Url = url,
                    UrlType = UrlInfo.UrlTypes.Email
                });
            }

            return Task.FromResult<UrlInfo>(null);
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
