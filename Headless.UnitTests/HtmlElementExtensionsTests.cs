namespace Headless.UnitTests
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="HtmlElementExtensionsTests" />
    ///     class tests the <see cref="HtmlElementExtensions" /> class.
    /// </summary>
    [TestClass]
    public class HtmlElementExtensionsTests
    {
        /// <summary>
        ///     Runs a test for ensure single returns single instance in set.
        /// </summary>
        [TestMethod]
        public void EnsureSingleReturnsSingleInstanceInSetTest()
        {
            const string Html = "<form name='Test' />";

            var page = new HtmlPageStub(Html);
            var element = new HtmlForm(page, ((IHtmlPage)page).Node);
            var elements = new List<HtmlElement>
            {
                element
            };

            var actual = elements.EnsureSingle();

            actual.Should().Be(element);
        }

        /// <summary>
        ///     Runs a test for ensure single throws exception when multiple elements found.
        /// </summary>
        [TestMethod]
        public void EnsureSingleThrowsExceptionWhenMultipleElementsFoundTest()
        {
            const string Html = "<form name='Test' />";

            var page = new HtmlPageStub(Html);
            var element = new HtmlForm(page, ((IHtmlPage)page).Node);
            var elements = new List<HtmlElement>
            {
                element, 
                element
            };

            Action action = () => elements.EnsureSingle();

            action.ShouldThrow<InvalidHtmlElementMatchException>();
        }

        /// <summary>
        ///     Runs a test for ensure single throws exception when no elements found.
        /// </summary>
        [TestMethod]
        public void EnsureSingleThrowsExceptionWhenNoElementsFoundTest()
        {
            var elements = new List<HtmlElement>();

            Action action = () => elements.EnsureSingle();

            action.ShouldThrow<InvalidHtmlElementMatchException>();
        }

        /// <summary>
        ///     Runs a test for has class returns false when element lacks class attribute.
        /// </summary>
        [TestMethod]
        public void HasClassReturnsFalseWhenElementLacksClassAttributeTest()
        {
            const string Html = "<html />";

            var page = new HtmlPageStub(Html);

            var target = page.Find<AnyHtmlElement>().All().EnsureSingle();

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

            var target = page.Find<AnyHtmlElement>().All().EnsureSingle();

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

            var target = page.Find<AnyHtmlElement>().All().EnsureSingle();

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

            var target = page.Find<AnyHtmlElement>().All().EnsureSingle();

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

            var target = page.Find<AnyHtmlElement>().All().EnsureSingle();

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

            var target = page.Find<AnyHtmlElement>().All().EnsureSingle();

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

            var target = page.Find<AnyHtmlElement>().All().EnsureSingle();

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

            var target = page.Find<AnyHtmlElement>().All().EnsureSingle();

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

            var target = page.Find<AnyHtmlElement>().All().EnsureSingle();

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

            var target = page.Find<AnyHtmlElement>().All().EnsureSingle();

            Action action = () => target.HasClass("  ");

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for has class throws exception when element is null.
        /// </summary>
        [TestMethod]
        public void HasClassThrowsExceptionWhenElementIsNullTest()
        {
            var target = (HtmlElement)null;

            Action action = () => target.HasClass("test");

            action.ShouldThrow<ArgumentNullException>();
        }
    }
}