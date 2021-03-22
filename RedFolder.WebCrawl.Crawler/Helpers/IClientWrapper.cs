using System.Net;
using System.Net.Http.Headers;

namespace RedFolder.WebCrawl.Crawler.Helpers
{
    public interface IClientWrapper
    {
        HttpStatusCode GetLastHttpStatusCode();
        string GetLastResponse();
        HttpResponseHeaders GetLastHttpResponseHeaders();
        void Process(string url);
    }
}
