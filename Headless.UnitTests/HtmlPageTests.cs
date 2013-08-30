namespace Headless.UnitTests
{
    using System.Linq;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="HtmlPageTests" />
    ///     class tests the <see cref="HtmlPage" /> class.
    /// </summary>
    [TestClass]
    public class HtmlPageTests
    {
        /// <summary>
        ///     Runs a test for processes content with case insensitive attribute names.
        /// </summary>
        [TestMethod]
        public void ProcessesContentWithCaseInsensitiveAttributeNamesTest()
        {
            const string Html = @"
<html>
<body>
<FORm Name='test'></FORm>
</body>
</html>
";

            using (var content = StreamContentFactory.FromHtml(Html))
            {
                var page = new HtmlPageWrapper();

                page.AssignContent(content);

                var actual = page.Find<AnyHtmlElement>().AllByAttribute("name", "test");

                actual.Count().Should().Be(1);
            }
        }

        /// <summary>
        ///     Runs a test for processes content with case insensitive tag names.
        /// </summary>
        [TestMethod]
        public void ProcessesContentWithCaseInsensitiveTagNamesTest()
        {
            const string Html = @"
<html>
<body>
<FORm id='test'></FORm>
</body>
</html>
";

            using (var content = StreamContentFactory.FromHtml(Html))
            {
                var page = new HtmlPageWrapper();

                page.AssignContent(content);

                var actual = page.Find<AnyHtmlElement>().ByTagName("form");

                actual.Id.Should().Be("test");
            }
        }

        /// <summary>
        ///     Runs a test for processes content with mixed case tag names across start and end tags.
        /// </summary>
        [TestMethod]
        public void ProcessesContentWithMixedCaseTagNamesAcrossStartAndEndTagsTest()
        {
            const string Html = @"
<html>
<body>
<FORm id='test'></foRM>
</body>
</html>
";

            using (var content = StreamContentFactory.FromHtml(Html))
            {
                var page = new HtmlPageWrapper();

                page.AssignContent(content);

                var actual = page.Find<AnyHtmlElement>().ByTagName("form");

                actual.Id.Should().Be("test");
            }
        }
    }
}