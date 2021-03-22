using RedFolder.WebCrawl.Crawler.Helpers;
using Xunit;

namespace RedFolder.WebCrawl.Crawler.Command.UnitTests
{
    public class LinksExtractor_ShouldExtractLinks
    {
        [Fact]
        public void ReturnsLinkForSrcWithDoubleQuote()
        {
            var extractor = new ContentLinksExtractor(@"https://www.red-folder.com");

            var results = extractor.Extract(@"<script src=""/test.js""></script>");

            Assert.NotNull(results);
            Assert.Equal(1, results.Count);
            Assert.Equal("https://www.red-folder.com/test.js", results[0]);
        }

        [Fact]
        public void ReturnsLinkForSrcWithSingleQuote()
        {
            var extractor = new ContentLinksExtractor(@"https://www.red-folder.com");

            var results = extractor.Extract(@"<script src='/test.js'></script>");

            Assert.NotNull(results);
            Assert.Equal(1, results.Count);
            Assert.Equal("https://www.red-folder.com/test.js", results[0]);
        }

        [Fact]
        public void ReturnsLinkForSrcWithDoubleQuoteAndAdditonalSpaces()
        {
            var extractor = new ContentLinksExtractor(@"https://www.red-folder.com");

            var results = extractor.Extract(@"<script src = ""/test.js""></script>");

            Assert.NotNull(results);
            Assert.Equal(1, results.Count);
            Assert.Equal("https://www.red-folder.com/test.js", results[0]);
        }

        [Fact]
        public void ReturnsLinkForSitemapLoc()
        {
            var extractor = new ContentLinksExtractor(@"https://www.red-folder.com");

            var results = extractor.Extract(@"<loc>https://www.red-folder.com/test.js</loc>");

            Assert.NotNull(results);
            Assert.Equal(1, results.Count);
            Assert.Equal("https://www.red-folder.com/test.js", results[0]);
        }

        [Fact]
        public void ReturnsMultipleLinks()
        {
            var content = @"<loc>https://www.red-folder.com/test.js</loc>" +
                          @"<script src = ""/test.js""></script>" +
                          @"<script src='/test.js'></script>" +
                          @"<script src=""/test.js""></script>";

            var extractor = new ContentLinksExtractor(@"https://www.red-folder.com");

            var results = extractor.Extract(content);

            Assert.NotNull(results);
            Assert.Equal(4, results.Count);
            Assert.Equal("https://www.red-folder.com/test.js", results[0]);
            Assert.Equal("https://www.red-folder.com/test.js", results[1]);
            Assert.Equal("https://www.red-folder.com/test.js", results[2]);
            Assert.Equal("https://www.red-folder.com/test.js", results[3]);
        }
    }
}
