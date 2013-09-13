namespace Headless.IntegrationTests
{
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
                var linksResult = browser.GoTo<RedirectIndexPage>();

                ((IPage)linksResult).Result.TraceResults();

                var page = linksResult.External.Click<GoogleSearchPage>();

                page.Result.TraceResults();

                var outcomes = page.Result.Outcomes;

                // There should have been a redirection
                outcomes.Should().Contain(x => x.StatusCode == HttpStatusCode.Found);

                // One of the requests should have hit the original location defined by the page
                outcomes.Should().ContainSingle(x => x.Location == page.TargetLocation);

                page.StatusCode.Should().Be(HttpStatusCode.OK);
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
                var linksResult = browser.GoTo<RedirectIndexPage>();

                ((IPage)linksResult).Result.TraceResults();

                var page = linksResult.Temporary.Click<HomeAboutPage>();

                page.Result.TraceResults();

                // There should have been a redirection
                page.Result.Outcomes.Should().ContainSingle(x => x.StatusCode == HttpStatusCode.Found);

                page.StatusCode.Should().Be(HttpStatusCode.OK);
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
                var linksResult = browser.GoTo<RedirectIndexPage>();

                ((IPage)linksResult).Result.TraceResults();

                var page = linksResult.Permanent.Click<HomeAboutPage>();

                page.Result.TraceResults();

                // There should have been a redirection
                page.Result.Outcomes.Should().ContainSingle(x => x.StatusCode == HttpStatusCode.MovedPermanently);

                page.StatusCode.Should().Be(HttpStatusCode.OK);
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

                ((IPage)result).Result.TraceResults();

                var page = result.About.Click<HomeAboutPage>();

                page.Result.TraceResults();

                page.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }
    }
}