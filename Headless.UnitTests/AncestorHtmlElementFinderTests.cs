namespace Headless.UnitTests
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="AncestorHtmlElementFinderTests" />
    ///     class tests the <see cref="AncestorHtmlElementFinder{T}" /> class.
    /// </summary>
    [TestClass]
    public class AncestorHtmlElementFinderTests
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

            Action action = () => new AncestorHtmlElementFinder<HtmlForm>(element);

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

            Action action = () => new AncestorHtmlElementFinder<HtmlForm>(page);

            action.ShouldNotThrow();
        }

        /// <summary>
        ///     Runs a test for elements can be found on x HTML pages.
        /// </summary>
        [TestMethod]
        public void ElementsCanBeFoundOnXHtmlPagesTest()
        {
            // the namespace on the html forces the xml to use namespaces in xpath queries, or use local-name
            const string Html =
                @"<html xmlns=""http://www.w3.org/1999/xhtml""><body><input type=""submit"" name=""test"" value=""foo"" /></body></html>";

            var page = new HtmlPageStub(Html);

            var input = page.Find<HtmlButton>().ByName("test");

            var target = new AncestorHtmlElementFinder<AnyHtmlElement>(input);

            var button = target.AllByTagName("body").ToList();

            button.Count.Should().Be(1);
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

            var input = page.Find<HtmlInput>().ByName("Data");

            var target = new AncestorHtmlElementFinder<HtmlElement>(input);

            var query = target.BuildElementQuery();

            var actual = target.Execute(query).ToList();

            actual.Any(x => x.TagName == "input").Should().BeFalse();
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

            var input = page.Find<HtmlInput>().ByName("Data");

            var target = new AncestorHtmlElementFinder<HtmlElement>(input);

            var query = target.BuildElementQuery();

            var actual = target.Execute(query).ToList();

            actual.Count.Should().Be(3);

            actual.OfType<HtmlForm>().Count().Should().Be(1);
            actual.OfType<AnyHtmlElement>().Count(x => x.TagName == "body").Should().Be(1);
            actual.OfType<AnyHtmlElement>().Count(x => x.TagName == "html").Should().Be(1);
        }

        /// <summary>
        ///     Runs a test for execute on multiple tags returns elements from type hierarchy from specific node.
        /// </summary>
        [TestMethod]
        public void ExecuteOnMultipleTagsReturnsElementsFromTypeHierarchyFromSpecificNodeTest()
        {
            const string Html =
                "<html><head /><body><form name='Test'><select name='Data'><option>First</option></select></form></body></html>";

            var page = new HtmlPageStub(Html);

            var input = page.Find<HtmlListItem>().Singular();

            var target = new AncestorHtmlElementFinder<HtmlFormElement>(input);

            var query = target.BuildElementQuery();

            var actual = target.Execute(query).ToList();

            actual.Count.Should().Be(1);
            actual.OfType<HtmlList>().Count().Should().Be(1);
        }

        /// <summary>
        ///     Runs a test for execute on single tag returns specific element type.
        /// </summary>
        [TestMethod]
        public void ExecuteOnSingleTagReturnsSpecificElementTypeTest()
        {
            const string Html =
                "<html><head /><body><form name='Test'><input type='text' name='Data' /><input type='checkbox' name='IsSet' /></form></body></html>";

            var page = new HtmlPageStub(Html);

            var input = page.Find<HtmlInput>().ByName("Data");

            var target = new AncestorHtmlElementFinder<HtmlForm>(input);

            var query = target.BuildElementQuery();

            var actual = target.Execute(query).ToList();

            actual.Count.Should().Be(1);
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

            var input = page.Find<HtmlInput>().ByName("Data");

            var target = new AncestorHtmlElementFinder<HtmlInput>(input);

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

            var input = page.Find<HtmlInput>().ByName("Data");

            var target = new AncestorHtmlElementFinder<HtmlInput>(input);

            var query = target.BuildElementQuery();

            var actual = target.Execute(query).ToList();

            actual.Any(x => x.TagName == "input").Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for throws exception when created with null HTML element.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithNullHtmlElementTest()
        {
            Action action = () => new AncestorHtmlElementFinder<HtmlElement>((HtmlElement)null);

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for throws exception when created with null page.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithNullPageTest()
        {
            Action action = () => new AncestorHtmlElementFinder<HtmlElement>((IHtmlPage)null);

            action.ShouldThrow<ArgumentNullException>();
        }
    }
}