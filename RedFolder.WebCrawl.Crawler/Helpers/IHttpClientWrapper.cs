using System.Net;
using System.Net.Http.Headers;

namespace RedFolder.WebCrawl.Crawler.Helpers
{
    public interface IHttpClientWrapper
    {
        HttpStatusCode LastHttpStatusCode { get; }
        string LastHttpResponse { get; }
        HttpResponseHeaders LastHttpResponseHeaders { get; }
        void Get(string url);
    }
}
