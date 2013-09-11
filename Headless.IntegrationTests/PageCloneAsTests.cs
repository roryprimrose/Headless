namespace Headless.IntegrationTests
{
    using Headless.IntegrationTests.Pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="PageCloneAsTests" />
    ///     class tests the page clone as functionality.
    /// </summary>
    [TestClass]
    public class PageCloneAsTests
    {
        /// <summary>
        ///     Runs a test for can clone dynamic page as page model.
        /// </summary>
        [TestMethod]
        public void CanCloneDynamicPageAsPageModelTest()
        {
            using (var browser = new Browser())
            {
                var page = browser.GoTo(Redirect.Index);

                var clonedPage = page.CloneAs<RedirectIndexPage>();
                var redirectPage = (RedirectIndexPage)clonedPage;

                redirectPage.External.Click();
            }
        }

        /// <summary>
        ///     Runs a test for can clone IHtmlPage as page model.
        /// </summary>
        [TestMethod]
        public void CanCloneIHtmlPageAsPageModelTest()
        {
            using (var browser = new Browser())
            {
                var page = browser.GoTo<RedirectIndexPage>(Redirect.Index) as IHtmlPage;

                var redirectPage = page.CloneAs<RedirectIndexPage>();

                redirectPage.External.Click();
            }
        }
    }
}