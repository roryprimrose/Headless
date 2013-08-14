namespace Headless.IntegrationTests
{
    using FluentAssertions;
    using Headless.IntegrationTests.Pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="TextPageTests" />
    ///     class tests the ability to download text files.
    /// </summary>
    [TestClass]
    public class TextPageTests
    {
        /// <summary>
        ///     Runs a test for can download text file data.
        /// </summary>
        [TestMethod]
        public void CanDownloadTextFileDataTest()
        {
            using (var browser = new Browser())
            {
                // TODO: Update DynamicPage so that it isn't restricted to HTML pages
                // It should look at the MIME type of the HTTP response to determine whether the dynamic page
                // returns HtmlPage, TextPage or BinaryPage.
                var page = browser.GoTo<TextContentPage>(Content.TextTest);

                page.Result.TraceResults();

                page.Content.Should().Be("This is a test text file");
            }
        }
    }
}