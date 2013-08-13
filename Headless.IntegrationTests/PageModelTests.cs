namespace Headless.IntegrationTests
{
    using System.Diagnostics;
    using System.Net;
    using FluentAssertions;
    using Headless.IntegrationTests.Pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="PageModelTests" />
    ///     class tests the <see cref="Browser" /> class.
    /// </summary>
    [TestClass]
    public class PageModelTests
    {
        /// <summary>
        ///     Runs a test for handle external redirect.
        /// </summary>
        [TestMethod]
        public void HandleExternalRedirectTest()
        {
            using (var browser = new Browser())
            {
                var linksResult = browser.GoTo<LinksIndexPage>();

                var searchPage = linksResult.External.Click<GoogleSearchPage>();

                // There should have been a redirection
                var result = browser.GetLastResult<GoogleSearchPage>();

                result.Outcomes.Should().Contain(x => x.StatusCode == HttpStatusCode.Found);

                // One of the requests should have hit the original location defined by the page
                result.Outcomes.Should().ContainSingle(x => x.Location == searchPage.Location);

                searchPage.StatusCode.Should().Be(HttpStatusCode.OK);

                foreach (var outcome in result.Outcomes)
                {
                    Trace.WriteLine(outcome);
                }

                Trace.WriteLine("Total response time: " + result.ResponseTime.TotalMilliseconds + " milliseconds");
            }
        }

        /// <summary>
        ///     Runs a test for handle non permanent redirect.
        /// </summary>
        [TestMethod]
        public void HandleNonPermanentRedirectTest()
        {
            using (var browser = new Browser())
            {
                var linksResult = browser.GoTo<LinksIndexPage>();

                var aboutPage = linksResult.Temporary.Click<HomeAboutPage>();

                // There should have been a redirection
                browser.GetLastResult<HomeAboutPage>()
                    .Outcomes.Should()
                    .ContainSingle(x => x.StatusCode == HttpStatusCode.Found);

                aboutPage.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        /// <summary>
        ///     Runs a test for handle permanent redirect.
        /// </summary>
        [TestMethod]
        public void HandlePermanentRedirectTest()
        {
            using (var browser = new Browser())
            {
                var linksResult = browser.GoTo<LinksIndexPage>();

                var aboutPage = linksResult.Permanent.Click<HomeAboutPage>();

                // There should have been a redirection
                browser.GetLastResult<HomeAboutPage>()
                    .Outcomes.Should()
                    .ContainSingle(x => x.StatusCode == HttpStatusCode.MovedPermanently);

                aboutPage.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        /// <summary>
        ///     Runs a test for navigate to about.
        /// </summary>
        [TestMethod]
        public void NavigateToAboutTest()
        {
            using (var browser = new Browser())
            {
                var result = browser.GoTo<HomeIndexPage>();

                var aboutPage = result.About.Click<HomeAboutPage>();

                aboutPage.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }
    }
}