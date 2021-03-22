using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RedFolder.WebCrawl.Crawler.Helpers
{
    public class ClientWrapper: IClientWrapper
    {
        private HttpStatusCode _lastHttpStatusCode = HttpStatusCode.Unused;
        private string _lastResponse = null;
        private HttpResponseHeaders _lastHttpResponseHeaders;

        private ILogger _log;

        public ClientWrapper(ILogger log)
        {
            _log = log;
        }

        public HttpStatusCode GetLastHttpStatusCode()
        {
            return _lastHttpStatusCode;
        }

        public string GetLastResponse()
        {
            return _lastResponse;
        }

        public HttpResponseHeaders GetLastHttpResponseHeaders()
        {
            return _lastHttpResponseHeaders;
        }

        public void Process(string url)
        {
            _log.LogInformation($"Start of process of {url}");
            try
            {
                _log.LogInformation("Creating HttpClientHandler");
                using (HttpClientHandler httpClientHanlder = new HttpClientHandler())
                {
                    httpClientHanlder.AllowAutoRedirect = false;

                    // https://social.msdn.microsoft.com/Forums/en-US/ca6372be-3169-4fb5-870f-bfbea605faf6/azure-webapp-webjob-exception-could-not-create-ssltls-secure-channel?forum=windowsazurewebsitespreview
                    //_log.Info("Specifically setting Security Protocol to TLS 1.2");
                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    _log.LogInformation("Creating HttpClient");
                    using (HttpClient client = new HttpClient(httpClientHanlder))
                    {
                        _log.LogInformation("Creating HttpRequestMessage");
                        using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
                        {
                            _log.LogInformation("Sending request");
                            using (HttpResponseMessage response = client.SendAsync(request).Result)
                            {
                                _log.LogInformation($"Prossing result - Http status {response.StatusCode}");
                                _lastHttpStatusCode = response.StatusCode;
                                _lastResponse = response.Content.ReadAsStringAsync().Result;
                                _lastHttpResponseHeaders = response.Headers;
                            }
                        }
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
