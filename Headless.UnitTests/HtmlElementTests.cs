namespace Headless.UnitTests
{
    using System;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="HtmlElementTests" />
    ///     class tests the <see cref="HtmlElement" /> class.
    /// </summary>
    [TestClass]
    public class HtmlElementTests
    {
        /// <summary>
        ///     Runs a test for has class returns false when element lacks class attribute.
        /// </summary>
        [TestMethod]
        public void HasClassReturnsFalseWhenElementLacksClassAttributeTest()
        {
            const string Html = "<html />";

            var page = new HtmlPageStub(Html);

            var target = page.Find<AnyHtmlElement>().Singular();

            target.HasClass("test").Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for has class returns false when element lacks exact match on class.
        /// </summary>
        [TestMethod]
        public void HasClassReturnsFalseWhenElementLacksExactMatchOnClassTest()
        {
            const string Html = "<html class='test-stuff test_more tested' />";

            var page = new HtmlPageStub(Html);

            var target = page.Find<AnyHtmlElement>().Singular();

            target.HasClass("test").Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for has class returns false when element lacks specified class.
        /// </summary>
        [TestMethod]
        public void HasClassReturnsFalseWhenElementLacksSpecifiedClassTest()
        {
            const string Html = "<html class='haha' />";

            var page = new HtmlPageStub(Html);

            var target = page.Find<AnyHtmlElement>().Singular();

            target.HasClass("test").Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for has class returns true when class attribute contains specified class.
        /// </summary>
        [TestMethod]
        public void HasClassReturnsTrueWhenClassAttributeContainsSpecifiedClassTest()
        {
            const string Html = "<html class='stuff test works' />";

            var page = new HtmlPageStub(Html);

            var target = page.Find<AnyHtmlElement>().Singular();

            target.HasClass("test").Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for has class returns true when class attribute ends with specified class.
        /// </summary>
        [TestMethod]
        public void HasClassReturnsTrueWhenClassAttributeEndsWithSpecifiedClassTest()
        {
            const string Html = "<html class='stuff test' />";

            var page = new HtmlPageStub(Html);

            var target = page.Find<AnyHtmlElement>().Singular();

            target.HasClass("test").Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for has class returns true when class attribute starts with specified class.
        /// </summary>
        [TestMethod]
        public void HasClassReturnsTrueWhenClassAttributeStartsWithSpecifiedClassTest()
        {
            const string Html = "<html class='test stuff' />";

            var page = new HtmlPageStub(Html);

            var target = page.Find<AnyHtmlElement>().Singular();

            target.HasClass("test").Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for has class returns true when element has class.
        /// </summary>
        [TestMethod]
        public void HasClassReturnsTrueWhenElementHasClassTest()
        {
            const string Html = "<html class='test' />";

            var page = new HtmlPageStub(Html);

            var target = page.Find<AnyHtmlElement>().Singular();

            target.HasClass("test").Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for has class throws exception when class name is empty.
        /// </summary>
        [TestMethod]
        public void HasClassThrowsExceptionWhenClassNameIsEmptyTest()
        {
            const string Html = "<html class='test stuff' />";

            var page = new HtmlPageStub(Html);

            var target = page.Find<AnyHtmlElement>().Singular();

            Action action = () => target.HasClass(string.Empty);

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for has class throws exception when class name is null.
        /// </summary>
        [TestMethod]
        public void HasClassThrowsExceptionWhenClassNameIsNullTest()
        {
            const string Html = "<html class='test stuff' />";

            var page = new HtmlPageStub(Html);

            var target = page.Find<AnyHtmlElement>().Singular();

            Action action = () => target.HasClass(null);

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for has class throws exception when class name is white space.
        /// </summary>
        [TestMethod]
        public void HasClassThrowsExceptionWhenClassNameIsWhiteSpaceTest()
        {
            const string Html = "<html class='test stuff' />";

            var page = new HtmlPageStub(Html);

            var target = page.Find<AnyHtmlElement>().Singular();

            Action action = () => target.HasClass("  ");

            action.ShouldThrow<ArgumentException>();
        }
    }
}