namespace Headless.IntegrationTests
{
    using System;
    using Headless.IntegrationTests.Pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="LocationTests" />
    ///     class tests the location validation functionality.
    /// </summary>
    [TestClass]
    public class LocationTests
    {
        /// <summary>
        ///     Runs a test for page can validate against multiple locations.
        /// </summary>
        [TestMethod]
        public void PageCanValidateAgainstMultipleLocationsTest()
        {
            using (var browser = new Browser())
            {
                browser.GoTo<HomeIndexPage>(new Uri(Config.BaseWebAddress, "home/index/"));
                browser.GoTo<HomeIndexPage>(new Uri(Config.BaseWebAddress, "home/index"));
                browser.GoTo<HomeIndexPage>(new Uri(Config.BaseWebAddress, "/"));
                browser.GoTo<HomeIndexPage>();
            }
        }
    }
}