namespace Headless.UnitTests
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="DefaultHtmlElementFinderTests" />
    ///     class tests the <see cref="DefaultHtmlElementFinder{T}" /> class.
    /// </summary>
    [TestClass]
    public class DefaultHtmlElementFinderTests
    {
        /// <summary>
        ///     Runs a test for can create from HTML element.
        /// </summary>
        [TestMethod]
        public void CanCreateFromHtmlElementTest()
        {
            const string Html = "<form name='Test'></form>";

            var page = new HtmlPageStub(Html);

            var element = new HtmlForm(page, ((IHtmlPage)page).Node);

            Action action = () => new DefaultHtmlElementFinder<HtmlForm>(element);

            action.ShouldNotThrow();
        }

        /// <summary>
        ///     Runs a test for can create from page.
        /// </summary>
        [TestMethod]
        public void CanCreateFromPageTest()
        {
            const string Html = "<html><head /><body><form name='Test' /></body></html>";

            var page = new HtmlPageStub(Html);

            Action action = () => new DefaultHtmlElementFinder<HtmlForm>(page);

            action.ShouldNotThrow();
        }

        /// <summary>
        ///     Runs a test for execute on multiple tags does not return node in context.
        /// </summary>
        [TestMethod]
        public void ExecuteOnMultipleTagsDoesNotReturnNodeInContextTest()
        {
            const string Html =
                "<html><head /><body><form name='Test'><input type='text' name='Data' /><input type='checkbox' name='IsSet' /></form></body></html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            var target = new DefaultHtmlElementFinder<HtmlElement>(form);

            var query = target.BuildElementQuery();

            var actual = target.Execute(query).ToList();

            actual.Any(x => x.TagName == "form").Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for execute on multiple tags returns any elements from type hierarchy from root.
        /// </summary>
        [TestMethod]
        public void ExecuteOnMultipleTagsReturnsAnyElementsFromTypeHierarchyFromRootTest()
        {
            const string Html =
                "<html><head /><body><form name='Test'><input type='text' name='Data' /><input type='checkbox' name='IsSet' /></form></body></html>";

            var page = new HtmlPageStub(Html);

            var target = new DefaultHtmlElementFinder<HtmlElement>(page);

            var query = target.BuildElementQuery();

            var actual = target.Execute(query).ToList();

            actual.Count.Should().Be(6);

            actual.OfType<HtmlInput>().Any().Should().BeTrue();
            actual.OfType<HtmlCheckBox>().Any().Should().BeTrue();
            actual.OfType<HtmlForm>().Any().Should().BeTrue();
            actual.OfType<AnyHtmlElement>().Count().Should().Be(3);
        }

        /// <summary>
        ///     Runs a test for execute on multiple tags returns any elements from type hierarchy from specific node.
        /// </summary>
        [TestMethod]
        public void ExecuteOnMultipleTagsReturnsAnyElementsFromTypeHierarchyFromSpecificNodeTest()
        {
            const string Html =
                "<html><head /><body><form name='Test'><input type='text' name='Data' /><input type='checkbox' name='IsSet' /></form><form name='other'><input type='hidden' name='source' value'here' /></form></body></html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            var target = new DefaultHtmlElementFinder<HtmlElement>(form);

            var query = target.BuildElementQuery();

            var actual = target.Execute(query).ToList();

            actual.Count.Should().Be(2);

            actual.OfType<HtmlInput>().Any().Should().BeTrue();
            actual.OfType<HtmlCheckBox>().Any().Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for execute on multiple tags returns elements from type hierarchy from root.
        /// </summary>
        [TestMethod]
        public void ExecuteOnMultipleTagsReturnsElementsFromTypeHierarchyFromRootTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <input type='text' name='Data' /><input type='checkbox' name='IsSet' />
        </form>
        <form name='Second'>
            <input type='text' name='Data2' /><input type='checkbox' name='IsSet3' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var target = new DefaultHtmlElementFinder<HtmlInput>(page);

            var query = target.BuildElementQuery();

            var actual = target.Execute(query).ToList();

            actual.Count.Should().Be(2);
        }

        /// <summary>
        ///     Runs a test for execute on multiple tags returns elements from type hierarchy from specific node.
        /// </summary>
        [TestMethod]
        public void ExecuteOnMultipleTagsReturnsElementsFromTypeHierarchyFromSpecificNodeTest()
        {
            const string Html =
                "<html><head /><body><form name='Test'><input type='text' name='Data' /><input type='checkbox' name='IsSet' /></form></body></html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            var target = new DefaultHtmlElementFinder<HtmlInput>(form);

            var query = target.BuildElementQuery();

            var actual = target.Execute(query).ToList();

            actual.Count.Should().Be(1);
        }

        /// <summary>
        ///     Runs a test for execute on single tag returns filtered radio buttons.
        /// </summary>
        [TestMethod]
        public void ExecuteOnSingleTagReturnsFilteredRadioButtonsTest()
        {
            const string Html =
                "<html><head /><body><form name='Test'><input type='radio' name='Data' /><input type='radio' name='Data' /></form></body></html>";

            var page = new HtmlPageStub(Html);

            var target = new DefaultHtmlElementFinder<HtmlRadioButton>(page);

            var query = target.BuildElementQuery();

            var actual = target.Execute(query).ToList();

            actual.Count.Should().Be(1);

            actual[0].Name.Should().Be("Data");
        }

        /// <summary>
        ///     Runs a test for execute on single tag returns specific element type.
        /// </summary>
        [TestMethod]
        public void ExecuteOnSingleTagReturnsSpecificElementTypeTest()
        {
            const string Html = "<html><head /><body><form name='Test' /></body></html>";

            var page = new HtmlPageStub(Html);

            var target = new DefaultHtmlElementFinder<HtmlForm>(page);

            var query = target.BuildElementQuery();

            var actual = target.Execute(query).ToList();

            actual.Count.Should().Be(1);

            actual[0].Name.Should().Be("Test");
        }

        /// <summary>
        ///     Runs a test for execute on specific tag does not return node in context.
        /// </summary>
        [TestMethod]
        public void ExecuteOnSpecificTagDoesNotReturnNodeInContextTest()
        {
            const string Html =
                "<html><head /><body><form name='Test'><input type='text' name='Data' /><input type='checkbox' name='IsSet' /></form></body></html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            var target = new DefaultHtmlElementFinder<HtmlForm>(form);

            var query = target.BuildElementQuery();

            var actual = target.Execute(query).ToList();

            actual.Count.Should().Be(0);
        }

        /// <summary>
        ///     Runs a test for execute on wildcard tag name does not return node in context.
        /// </summary>
        [TestMethod]
        public void ExecuteOnWildcardTagNameDoesNotReturnNodeInContextTest()
        {
            const string Html =
                "<html><head /><body><form name='Test'><input type='text' name='Data' /><input type='checkbox' name='IsSet' /></form></body></html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            var target = new DefaultHtmlElementFinder<AnyHtmlElement>(form);

            var query = target.BuildElementQuery();

            var actual = target.Execute(query).ToList();

            actual.Any(x => x.TagName == "form").Should().BeFalse();
        }

        /// <summary>
        ///     Tests that an XHTML namespace does not cause XPATH queries to fail.
        /// </summary>
        [TestMethod]
        public void InputsCanBeFoundOnXHtmlPages()
        {
            // the namespace on the html forces the xml to use namespaces in xpath queries, or use local-name
            const string Html =
                @"<html xmlns=""http://www.w3.org/1999/xhtml""><body><input type=""submit"" name=""test"" value=""foo"" /></body></html>";

            var page = new HtmlPageStub(Html);

            var button = page.Find<HtmlButton>().AllByName("test").ToList();

            button.Count.Should().Be(1);
            button[0].Value.Should().Be("foo");
        }

        /// <summary>
        ///     Runs a test for throws exception when created with null HTML element.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithNullHtmlElementTest()
        {
            Action action = () => new DefaultHtmlElementFinder<HtmlElement>((HtmlElement)null);

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for throws exception when created with null page.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithNullPageTest()
        {
            Action action = () => new DefaultHtmlElementFinder<HtmlElement>((IHtmlPage)null);

            action.ShouldThrow<ArgumentNullException>();
        }
    }
}