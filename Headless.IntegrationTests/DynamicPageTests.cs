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
        /// Runs a test for navigate to about.
        /// </summary>
        [TestMethod]
        public void NavigateToAboutTest()
        {
            using (var browser = new Browser())
            {
                var result = browser.GoTo(Home.Index);

                ((HttpStatusCode)result.StatusCode).Should().Be(HttpStatusCode.OK);

                Assert.Inconclusive("The rest of this functionality has not yet been writte");
                //var aboutPage = result.About.Click<HomeAboutPage>();

                //aboutPage.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }
    }
}