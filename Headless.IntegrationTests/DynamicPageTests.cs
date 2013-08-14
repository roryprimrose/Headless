namespace Headless.IntegrationTests
{
    using System.Net;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="DynamicPageTests" />
    ///     class tests the dynamic HTML page support.
    /// </summary>
    [TestClass]
    public class DynamicPageTests
    {
        /// <summary>
        ///     Runs a test for handle external redirect.
        /// </summary>
        [TestMethod]
        public void HandleExternalRedirectTest()
        {
            using (var browser = new Browser())
            {
                var linksResult = browser.GoTo(Redirect.Index);

                ((IPage)linksResult).Result.TraceResults();

                var page = (IHtmlPage)linksResult.External.Click();

                page.Result.TraceResults();

                var outcomes = page.Result.Outcomes;

                // There should have been a redirection
                outcomes.Should().Contain(x => x.StatusCode == HttpStatusCode.Found);

                // One of the requests should have hit the original location defined by the page
                outcomes.Should().ContainSingle(x => x.Location == page.Location);

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
                var linksResult = browser.GoTo(Redirect.Index);

                ((IPage)linksResult).Result.TraceResults();

                var page = (IHtmlPage)linksResult.Temporary.Click();

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
                var linksResult = browser.GoTo(Redirect.Index);

                ((IPage)linksResult).Result.TraceResults();

                var page = (IHtmlPage)linksResult.Permanent.Click();

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
                var result = browser.GoTo(Home.Index);

                ((IPage)result).Result.TraceResults();

                ((IHtmlPage)result).StatusCode.Should().Be(HttpStatusCode.OK);

                var page = (IHtmlPage)result.About.Click();

                page.Result.TraceResults();

                page.IsOn(Home.About).Should().BeTrue();
                page.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }
    }
}