namespace Headless.IntegrationTests
{
    using System;
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
        ///     Runs a test for dynamic page correctly validates location.
        /// </summary>
        [TestMethod]
        public void DynamicPageCorrectlyValidatesLocationTest()
        {
            using (var browser = new Browser())
            {
                var redirectPage = browser.GoTo(Redirect.Index);

                var page = redirectPage.Temporary.Click();

                bool actual = page.IsOn(Home.About);

                actual.Should().BeTrue();

                actual = page.IsOn(Home.Echo);

                actual.Should().BeFalse();

                actual = page.IsOn(Redirect.Index);

                actual.Should().BeFalse();
            }
        }

        /// <summary>
        ///     Runs a test for dynamic page does not validation final location against original request.
        /// </summary>
        [TestMethod]
        public void DynamicPageDoesNotValidationFinalLocationAgainstOriginalRequestTest()
        {
            using (var browser = new Browser())
            {
                var page = browser.GoTo(Redirect.Temporary);

                bool actual = page.IsOn(Home.About);

                actual.Should().BeTrue();
            }
        }

        /// <summary>
        ///     Runs a test for dynamic returns binary page for binary media type.
        /// </summary>
        [TestMethod]
        public void DynamicReturnsBinaryPageForBinaryMediaTypeTest()
        {
            using (var browser = new Browser())
            {
                var page = browser.GoTo(Content.BinaryTest) as BinaryPage;

                page.Should().NotBeNull();

                browser.Page.Should().BeSameAs(page);
            }
        }

        /// <summary>
        ///     Runs a test for dynamic returns IHtmlPage for HTML media type.
        /// </summary>
        [TestMethod]
        public void DynamicReturnsIHtmlPageForHtmlMediaTypeTest()
        {
            using (var browser = new Browser())
            {
                var page = browser.GoTo(Redirect.Index) as IHtmlPage;

                page.Should().NotBeNull();

                browser.Page.Should().BeSameAs(page);
            }
        }

        /// <summary>
        ///     Runs a test for dynamic returns text page for text media type.
        /// </summary>
        [TestMethod]
        public void DynamicReturnsTextPageForTextMediaTypeTest()
        {
            using (var browser = new Browser())
            {
                var page = browser.GoTo(Content.TextTest) as TextPage;

                page.Should().NotBeNull();

                browser.Page.Should().BeSameAs(page);
            }
        }

        /// <summary>
        ///     Runs a test for go to location returns page.
        /// </summary>
        [TestMethod]
        public void GoToLocationReturnsPageTest()
        {
            using (var browser = new Browser())
            {
                var linksResult = browser.GoTo(Redirect.Index) as IPage;

                linksResult.Should().NotBeNull();
            }
        }

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

        /// <summary>
        ///     Runs a test for page throws exception when attempting to assign to dynamic member.
        /// </summary>
        [TestMethod]
        public void PageThrowsExceptionWhenAttemptingToAssignToDynamicMemberTest()
        {
            using (var browser = new Browser())
            {
                var result = browser.GoTo(Form.Index);

                Action action = () => result.Text = "Test";

                action.ShouldThrow<InvalidOperationException>();
            }
        }

        /// <summary>
        ///     Runs a test for page throws exception when requested element does not exist.
        /// </summary>
        [TestMethod]
        public void PageThrowsExceptionWhenRequestedElementDoesNotExistTest()
        {
            using (var browser = new Browser())
            {
                var result = browser.GoTo(Home.Index);

                Action action = () => result.ElementNotFound.Click();

                action.ShouldThrow<HtmlElementNotFoundException>();
            }
        }
    }
}