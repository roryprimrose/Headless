namespace Headless.UnitTests
{
    using System;
    using System.Linq;
    using System.Xml;
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
            var doc = new XmlDocument();

            doc.LoadXml("<form name='Test' />");

            var page = new HtmlPageStub(doc);

            var element = new HtmlForm(page, doc.DocumentElement);

            Action action = () => new DefaultHtmlElementFinder<HtmlForm>(element);

            action.ShouldNotThrow();
        }

        /// <summary>
        ///     Runs a test for can create from page.
        /// </summary>
        [TestMethod]
        public void CanCreateFromPageTest()
        {
            var doc = new XmlDocument();

            doc.LoadXml("<html><head /><body><form name='Test' /></body></html>");

            var page = new HtmlPageStub(doc);

            Action action = () => new DefaultHtmlElementFinder<HtmlForm>(page);

            action.ShouldNotThrow();
        }

        /// <summary>
        ///     Runs a test for execute returns any elements from type hierarchy.
        /// </summary>
        [TestMethod]
        public void ExecuteReturnsAnyElementsFromTypeHierarchyTest()
        {
            var doc = new XmlDocument();

            doc.LoadXml(
                "<html><head /><body><form name='Test'><input type='text' name='Data' /><input type='checkbox' name='IsSet' /></form></body></html>");

            var page = new HtmlPageStub(doc);

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
        ///     Runs a test for execute returns elements from type hierarchy.
        /// </summary>
        [TestMethod]
        public void ExecuteReturnsElementsFromTypeHierarchyTest()
        {
            var doc = new XmlDocument();

            doc.LoadXml(
                "<html><head /><body><form name='Test'><input type='text' name='Data' /><input type='checkbox' name='IsSet' /></form></body></html>");

            var page = new HtmlPageStub(doc);

            var target = new DefaultHtmlElementFinder<HtmlFormElement>(page);

            var query = target.BuildElementQuery();

            var actual = target.Execute(query).ToList();

            actual.Count.Should().Be(2);

            actual.OfType<HtmlInput>().Any().Should().BeTrue();
            actual.OfType<HtmlCheckBox>().Any().Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for execute returns filtered radio buttons.
        /// </summary>
        [TestMethod]
        public void ExecuteReturnsFilteredRadioButtonsTest()
        {
            var doc = new XmlDocument();

            doc.LoadXml(
                "<html><head /><body><form name='Test'><input type='radio' name='Data' /><input type='radio' name='Data' /></form></body></html>");

            var page = new HtmlPageStub(doc);

            var target = new DefaultHtmlElementFinder<HtmlRadioButton>(page);

            var query = target.BuildElementQuery();

            var actual = target.Execute(query).ToList();

            actual.Count.Should().Be(1);


            actual[0].Name.Should().Be("Data");
        }

        /// <summary>
        ///     Runs a test for execute returns specific element type.
        /// </summary>
        [TestMethod]
        public void ExecuteReturnsSpecificElementTypeTest()
        {
            var doc = new XmlDocument();

            doc.LoadXml("<html><head /><body><form name='Test' /></body></html>");

            var page = new HtmlPageStub(doc);

            var target = new DefaultHtmlElementFinder<HtmlForm>(page);

            var query = target.BuildElementQuery();

            var actual = target.Execute(query).ToList();

            actual.Count.Should().Be(1);

            actual[0].Name.Should().Be("Test");
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