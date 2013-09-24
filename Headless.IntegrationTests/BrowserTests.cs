namespace Headless.IntegrationTests
{
    using System;
    using System.Net;
    using FluentAssertions;
    using Headless.IntegrationTests.Pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="BrowserTests" />
    ///     class tests the <see cref="Browser" /> class.
    /// </summary>
    [TestClass]
    public class BrowserTests
    {
        /// <summary>
        ///     Runs a test for page returns loaded page on successful request.
        /// </summary>
        [TestMethod]
        public void PageReturnsLoadedPageOnSuccessfulRequestTest()
        {
            using (var browser = new Browser())
            {
                var page = browser.GoTo<HomeIndexPage>();

                browser.Page.Should().Be(page);
            }
        }

        /// <summary>
        /// Runs a test for page returns loaded page when HTTP outcome fails.
        /// </summary>
        [TestMethod]
        public void PageReturnsLoadedPageWhenHttpOutcomeFailsTest()
        {
            using (var browser = new Browser())
            {
                Action action = () => browser.GoTo(Redirect.Index, HttpStatusCode.BadRequest);

                action.ShouldThrow<HttpOutcomeException>();

                browser.Page.Should().NotBeNull();
            }
        }

        /// <summary>
        /// Runs a test for page returns loaded page when location validation fails.
        /// </summary>
        [TestMethod]
        public void PageReturnsLoadedPageWhenLocationValidationFailsTest()
        {
            using (var browser = new Browser())
            {
                Action action = () => browser.GoTo<HomeAboutPage>(Home.Echo);

                action.ShouldThrow<HttpOutcomeException>();

                browser.Page.Should().NotBeNull();
            }
        }
    }
}