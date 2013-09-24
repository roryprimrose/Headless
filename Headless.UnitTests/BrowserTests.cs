namespace Headless.UnitTests
{
    using System;
    using System.Net;
    using System.Net.Http;
    using FluentAssertions;
    using Headless.Activation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;

    /// <summary>
    ///     The <see cref="BrowserTests" />
    ///     class tests the <see cref="Browser" /> class.
    /// </summary>
    [TestClass]
    public class BrowserTests
    {
        /// <summary>
        ///     Runs a test for browse to throws exception with null page factory.
        /// </summary>
        [TestMethod]
        public void BrowseToThrowsExceptionWithNullPageFactoryTest()
        {
            using (var browser = new Browser())
            {
                var location = new Uri("https://google.com");
                var request = new HttpRequestMessage(HttpMethod.Get, location);

                Action action = () => browser.Execute<TextPageWrapper>(request, HttpStatusCode.OK, null);

                action.ShouldThrow<ArgumentNullException>();
            }
        }

        /// <summary>
        ///     Runs a test for can clear cookies without previous request.
        /// </summary>
        [TestMethod]
        public void CanClearCookiesWithoutPreviousRequestTest()
        {
            using (var browser = new Browser())
            {
                browser.UseCookies = true;
                browser.ClearCookies();

                browser.Cookies.Count.Should().Be(0);
                browser.UseCookies.Should().BeTrue();
            }
        }

        /// <summary>
        ///     Runs a test for execute throws exception with null request message.
        /// </summary>
        [TestMethod]
        public void ExecuteThrowsExceptionWithNullRequestMessageTest()
        {
            using (var browser = new Browser())
            {
                var pageFactory = Substitute.For<IPageFactory>();

                Action action = () => browser.Execute<TextPageWrapper>(null, HttpStatusCode.OK, pageFactory);

                action.ShouldThrow<ArgumentNullException>();
            }
        }

        /// <summary>
        ///     Runs a test for page returns null when no request made.
        /// </summary>
        [TestMethod]
        public void PageReturnsNullWhenNoRequestMadeTest()
        {
            using (var browser = new Browser())
            {
                browser.Page.Should().BeNull();
            }
        }
    }
}