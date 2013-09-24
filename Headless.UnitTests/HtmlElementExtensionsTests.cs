namespace Headless.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Xml.XPath;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;

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
        ///     Runs a test for get HTML form for element returns parent form.
        /// </summary>
        [TestMethod]
        public void GetHtmlFormForElementReturnsParentFormTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <input type='text' name='Data' />
        </form>
        <form name='SecondTest'>
            <input type='checkbox' name='IsSet' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var input = page.Find<HtmlInput>().ByName("Data");
            var actual = input.GetHtmlForm();

            actual.Should().NotBeNull();
            actual.Name.Should().Be("Test");
        }

        /// <summary>
        ///     Runs a test for get HTML form for element returns source element when is form.
        /// </summary>
        [TestMethod]
        public void GetHtmlFormForElementReturnsSourceElementWhenIsFormTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <input type='text' name='Data' />
        </form>
        <form name='SecondTest'>
            <input type='checkbox' name='IsSet' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var target = page.Find<HtmlForm>().ByName("Test");

            var actual = target.GetHtmlForm();

            ReferenceEquals(target, actual).Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for get HTML form for element throws exception when form not found.
        /// </summary>
        [TestMethod]
        public void GetHtmlFormForElementThrowsExceptionWhenFormNotFoundTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <input type='text' name='Data' />
        </form>
        <form name='SecondTest'>
            <input type='checkbox' name='IsSet' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var target = page.Find<AnyHtmlElement>().AllByTagName("body").EnsureSingle();

            Action action = () => target.GetHtmlForm();

            action.ShouldThrow<HtmlElementNotFoundException>();
        }

        /// <summary>
        ///     Runs a test for get HTML form for element throws exception with null element.
        /// </summary>
        [TestMethod]
        public void GetHtmlFormForElementThrowsExceptionWithNullElementTest()
        {
            var target = (HtmlElement)null;

            Action action = () => target.GetHtmlForm();

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for get HTML form for node throws exception when no form found.
        /// </summary>
        [TestMethod]
        public void GetHtmlFormForNodeThrowsExceptionWhenNoFormFoundTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <input type='text' name='Data' />
        </form>
        <form name='SecondTest'>
            <input type='checkbox' name='IsSet' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            Action action = () => ((IHtmlPage)page).Document.GetHtmlForm(page);

            action.ShouldThrow<HtmlElementNotFoundException>();
        }

        /// <summary>
        ///     Runs a test for get HTML form for node throws exception with null node.
        /// </summary>
        [TestMethod]
        public void GetHtmlFormForNodeThrowsExceptionWithNullNodeTest()
        {
            var page = Substitute.For<IHtmlPage>();

            var target = (IXPathNavigable)null;

            Action action = () => target.GetHtmlForm(page);

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for get HTML form for node throws exception with null page.
        /// </summary>
        [TestMethod]
        public void GetHtmlFormForNodeThrowsExceptionWithNullPageTest()
        {
            var page = (IHtmlPage)null;

            var target = Substitute.For<IXPathNavigable>();

            Action action = () => target.GetHtmlForm(page);

            action.ShouldThrow<ArgumentNullException>();
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