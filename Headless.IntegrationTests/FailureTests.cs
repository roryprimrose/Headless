namespace Headless.IntegrationTests
{
    using System.Net;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="FailureTests" />
    ///     class tests the failure response scenarios.
    /// </summary>
    [TestClass]
    public class FailureTests
    {
        /// <summary>
        ///     Runs a test for browser correctly validates non200 status code response.
        /// </summary>
        [TestMethod]
        public void BrowserCorrectlyValidatesNon200StatusCodeResponseTest()
        {
            using (var browser = new Browser())
            {
                var page = browser.GoTo(Home.Failure, HttpStatusCode.InternalServerError);

                ((IHtmlPage)page).Result.TraceResults();
            }
        }

        /// <summary>
        ///     Runs a test for browser exposes loaded page when status code validation fails.
        /// </summary>
        [TestMethod]
        public void BrowserExposesLoadedPageWhenStatusCodeValidationFailsTest()
        {
            using (var browser = new Browser())
            {
                try
                {
                    browser.GoTo(Home.Failure);
                }
                catch (HttpOutcomeException ex)
                {
                    browser.Page.Result.TraceResults();
                }
            }
        }
    }
}