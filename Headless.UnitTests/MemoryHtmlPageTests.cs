namespace Headless.UnitTests
{
    using System;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="MemoryHtmlPageTests" />
    ///     class tests the <see cref="MemoryHtmlPage" /> class.
    /// </summary>
    [TestClass]
    public class MemoryHtmlPageTests
    {
        /// <summary>
        ///     Runs a test for can create from in memory HTML.
        /// </summary>
        [TestMethod]
        public void CanCreateFromInMemoryHtmlTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <select name='Data'>
               <option>1</option>
            </select>
        </form>
    </body>
</html>";

            using (var browser = new Browser())
            {
                var target = new MemoryHtmlPage(browser, Html);

                target.Find<HtmlForm>().Singular();
            }
        }

        /// <summary>
        ///     Runs a test for throws exception when created with empty HTML.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithEmptyHtmlTest()
        {
            using (var browser = new Browser())
            {
                Action action = () => new MemoryHtmlPage(browser, string.Empty);

                action.ShouldThrow<ArgumentException>();
            }
        }

        /// <summary>
        ///     Runs a test for throws exception when created with null browser.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithNullBrowserTest()
        {
            Action action = () => new MemoryHtmlPage(null, Guid.NewGuid().ToString());

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for throws exception when created with null HTML.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithNullHtmlTest()
        {
            using (var browser = new Browser())
            {
                Action action = () => new MemoryHtmlPage(browser, null);

                action.ShouldThrow<ArgumentException>();
            }
        }

        /// <summary>
        ///     Runs a test for throws exception when created with white space HTML.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithWhiteSpaceHtmlTest()
        {
            using (var browser = new Browser())
            {
                Action action = () => new MemoryHtmlPage(browser, " ");

                action.ShouldThrow<ArgumentException>();
            }
        }
    }
}