using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RedFolder.WebCrawl.Crawler.Helpers
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<HttpClientWrapper> _log;

        public HttpClientWrapper(IHttpClientFactory clientFactory, ILogger<HttpClientWrapper> log)
        {
            _clientFactory = clientFactory;
            _log = log;

            LastHttpStatusCode = HttpStatusCode.Unused;
        }

        public HttpStatusCode LastHttpStatusCode { get; private set; } 
        public string LastHttpResponse { get; private set; }
        public HttpResponseHeaders LastHttpResponseHeaders { get; private set; }

        public void Get(string url)
        {
            _log.LogInformation($"Start of process of {url}");
            try
            {
                _log.LogInformation("Creating HttpRequestMessage");
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    _log.LogInformation("Sending request");
                    var httpClient = _clientFactory.CreateClient("default");
                    using (HttpResponseMessage response = httpClient.SendAsync(request).Result)
                    {
                        _log.LogInformation($"Prossing result - Http status {response.StatusCode}");
                        LastHttpStatusCode = response.StatusCode;
                        LastHttpResponse = response.Content.ReadAsStringAsync().Result;
                        LastHttpResponseHeaders = response.Headers;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogInformation("Exception occured during Processing");
                _log.LogInformation($"Excpetion message: {ex.Message}");
                _log.LogInformation($"Stack trace: {ex.StackTrace}");
                throw ex;
            }
        }
    }
}
