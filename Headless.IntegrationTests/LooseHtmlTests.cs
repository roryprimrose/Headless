namespace Headless.IntegrationTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="LooseHtmlTests" />
    ///     class tests the ability for Headless to read HTML content that is not strictly correct.
    /// </summary>
    [TestClass]
    public class LooseHtmlTests
    {
        /// <summary>
        ///     Runs a test for can read page without body.
        /// </summary>
        /// <remarks>This test is ignored because the associated DTD causes a timeout when a download if it is attempted.</remarks>
        [TestMethod]
        [Ignore]
        public void CanReadPageWithoutBodyTest()
        {
            using (var browser = new Browser())
            {
                var location = new Uri("https://cashbank.cashedge.com/cashedgeBank/CashedgeBankSite/LoginPage.jsp");

                // This method will already test a 200 response and whether the document could be loaded
                browser.GoTo(location);
            }
        }
    }
}