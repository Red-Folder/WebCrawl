using RedFolder.WebCrawl.Crawler.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedFolder.WebCrawl.Crawler
{
    public class KnownPageProcessor: IProcessUrl
    {
        private readonly IList<string> _knownUrls;

        public KnownPageProcessor()
        {
            _knownUrls = PopulateKnownUrls();
        }

        public Task<UrlInfo> Process(string url)
        {
            if (CanBeHandled(url))
            {
                return Task.FromResult(new UrlInfo
                {
                    Url = url,
                    UrlType = UrlInfo.UrlTypes.KnownPage
                });
            }

            return Task.FromResult<UrlInfo>(null);
        }

        private bool CanBeHandled(string url)
        {
            if (url.StartsWith(@"https://www.googletagmanager.com")) return true;
            if (_knownUrls.Contains(url)) return true;

            return false;
        }

        private IList<string> PopulateKnownUrls()
        {
            return new List<string>
            {
                @"https://twitter.com/folderred",
                @"skype:red-folder?chat",
                //@"https://github.com/red-folder",
                //@"http://red-folder.blogspot.co.uk/",
                @"https://uk.linkedin.com/pub/mark-taylor/5/138/1ab",

                @"https://www.linkedin.com/pulse/business-meet-software-development-mark-taylor",
                @"https://www.linkedin.com/pulse/why-software-development-so-complex-mark-taylor",
                @"https://www.linkedin.com/pulse/what-makes-great-developer-mark-taylor",
                @"https://www.linkedin.com/pulse/what-agile-software-development-part-1-mark-taylor",
                @"https://www.linkedin.com/pulse/what-agile-software-development-part-2-mark-taylor",
                @"https://www.linkedin.com/pulse/what-security-mark-taylor",
                @"https://www.linkedin.com/pulse/zeigarnik-effect-mark-taylor",
                @"https://www.linkedin.com/pulse/focus-productivity-mark-taylor",
                @"https://www.linkedin.com/pulse/developer-anarchy-mark-taylor",
                @"https://www.linkedin.com/pulse/roi-failure-mark-taylor",
                @"https://www.linkedin.com/pulse/roi-hero-developer-mark-taylor",
                @"https://www.linkedin.com/pulse/roi-building-thing-right-mark-taylor",
                @"https://www.linkedin.com/pulse/roi-learning-mark-taylor",
                @"https://www.linkedin.com/pulse/when-you-going-admit-technology-company-mark-taylor",
                @"https://www.linkedin.com/pulse/software-development-maturity-model-mark-taylor",
                @"https://www.linkedin.com/pulse/roi-testing-mark-taylor",
                @"https://www.linkedin.com/pulse/market-economic-shock-mark-taylor",
                @"https://www.linkedin.com/pulse/roi-microsoft-certifications-mark-taylor",
                @"https://www.linkedin.com/pulse/roi-agile-implementation-mark-taylor",

                //@"http://blog.red-folder.com/2016/03/converting-to-aspnet-core-part-1.html",
                //@"http://blog.red-folder.com/2016/03/converting-to-aspnet-core-part-2.html",
                //@"http://blog.red-folder.com/2016/04/converting-to-aspnet-core-part-3.html",
                //@"http://blog.red-folder.com/2016/04/converting-to-aspnet-core-part-4.html",
                //@"http://blog.red-folder.com/2016/07/converting-to-aspnet-core-part-5.html",
                //@"http://blog.red-folder.com/2016/07/converting-to-aspnet-core-part-6.html",
                //@"http://blog.red-folder.com/2016/08/converting-to-aspnet-core-part-7.html",

                //@"http://red-folder.blogspot.co.uk/2015/06/microservices-practical-use.html",
                //@"http://red-folder.blogspot.co.uk/2015/06/microservices-source-code-management.html",

                //@"http://red-folder.blogspot.co.uk/2012/09/android-background-service-for-phonegap.html",
                //@"http://red-folder.blogspot.co.uk/2012/09/phonegap-android-background-service.html",
                //@"http://red-folder.blogspot.co.uk/2012/09/phonegap-android-background-service_11.html",
                //@"http://red-folder.blogspot.co.uk/2012/09/phonegap-service-tutorial-part-1.html",
                //@"http://red-folder.blogspot.co.uk/2012/09/phonegap-service-tutorial-part-2.html",
                //@"http://red-folder.blogspot.co.uk/2012/09/phonegap-service-tutorial-part-3.html",
                //@"http://red-folder.blogspot.co.uk/2013/08/automated-build-part-1.html",
                //@"http://red-folder.blogspot.co.uk/2013/08/automated-build-part-2-build-job_11.html",
                //@"http://red-folder.blogspot.co.uk/2013/08/automated-build-part-3-testing-basics.html",
                //@"http://red-folder.blogspot.co.uk/2013/08/automated-build-part-4-testing-job.html",
                //@"http://red-folder.blogspot.co.uk/2013/09/automated-build-part-5-master-job.html",
                //@"http://red-folder.blogspot.co.uk/2013/09/automated-build-part-6-output-job.html",

                //@"https://github.com/red-folder/ms-common-library",
                //@"https://github.com/red-folder/ms-base-webapi",
                //@"https://github.com/red-folder/ms-logger",
                //@"https://github.com/red-folder/ms-projects",
                //@"https://github.com/red-folder/bgs-core",
                //@"https://github.com/red-folder/bgs-sample",
                //@"https://github.com/red-folder/gps-service-plugin",
                //@"https://github.com/red-folder/scheduler-plugin",
                //@"https://github.com/red-folder/availability-monitor-plugin",
                //@"https://github.com/red-folder/cordova-plugin-smshandler",

                @"https://cdnjs.cloudflare.com/ajax/libs/d3/3.5.5/d3.js",

                @"https://en.wikipedia.org/wiki/microservices"
            };
        }
    }
}
