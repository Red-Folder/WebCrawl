using Microsoft.Extensions.Logging;
using RedFolder.WebCrawl.Crawler.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedFolder.WebCrawl.Crawler
{
    public class Crawler
    {
        private readonly SortedList<int, IProcessUrl> _processors;
        private readonly ILogger<Crawler> _log;

        public Crawler(SortedList<int, IProcessUrl> processors, ILogger<Crawler> log)
        {
            _processors = processors;
            _log = log;
        }

        public async Task<UrlInfo> Crawl(string url)
        {
            try
            {
                UrlInfo result = null;

                foreach (var processor in _processors.Values)
                {
                    result = await processor.Process(url);

                    if (result != null) break;
                }

                if (result == null)
                {
                    result = new UrlInfo
                    {
                        Url = url,
                        InvalidationMessage = "Unknown url type",
                        UrlType = UrlInfo.UrlTypes.Unknown
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, $"Unhandled exception when processing {url}");
                return new UrlInfo
                {
                    Url = url,
                    InvalidationMessage = $"Exception: {ex.Message}",
                    UrlType = UrlInfo.UrlTypes.Exception
                };
            }
        }
    }
}
