namespace Headless.IntegrationTests
{
    using System.Diagnostics;
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

                var searchPage = (IHtmlPage)linksResult.External.Click();

                var outcomes = searchPage.Result.Outcomes;

                // There should have been a redirection
                outcomes.Should().Contain(x => x.StatusCode == HttpStatusCode.Found);

                // One of the requests should have hit the original location defined by the page
                outcomes.Should().ContainSingle(x => x.Location == searchPage.Location);

                searchPage.StatusCode.Should().Be(HttpStatusCode.OK);

                foreach (var outcome in outcomes)
                {
                    Trace.WriteLine(outcome);
                }

                Trace.WriteLine(
                    "Total response time: " + searchPage.Result.ResponseTime.TotalMilliseconds + " milliseconds");
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

                var aboutPage = (IHtmlPage)linksResult.Temporary.Click();

                // There should have been a redirection
                aboutPage.Result.Outcomes.Should().ContainSingle(x => x.StatusCode == HttpStatusCode.Found);

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
                var linksResult = browser.GoTo(Redirect.Index);

                var aboutPage = (IHtmlPage)linksResult.Permanent.Click();

                // There should have been a redirection
                aboutPage.Result.Outcomes.Should().ContainSingle(x => x.StatusCode == HttpStatusCode.MovedPermanently);

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
                var result = browser.GoTo(Home.Index);

                ((IHtmlPage)result).StatusCode.Should().Be(HttpStatusCode.OK);

                var aboutPage = (IHtmlPage)result.About.Click();

                aboutPage.IsOn(Home.About).Should().BeTrue();
                aboutPage.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }
    }
}