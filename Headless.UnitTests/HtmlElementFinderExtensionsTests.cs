namespace Headless.UnitTests
{
    using System;
    using System.Linq;
    using System.Xml;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;

    /// <summary>
    ///     The <see cref="HtmlElementFinderExtensionsTests" />
    ///     class tests the <see cref="HtmlElementFinderExtensions" /> class.
    /// </summary>
    [TestClass]
    public class HtmlElementFinderExtensionsTests
    {
        /// <summary>
        ///     Runs a test for all by attribute executes query in node context.
        /// </summary>
        [TestMethod]
        public void AllByAttributeExecutesQueryInNodeContextTest()
        {
            var doc = new XmlDocument();

            doc.LoadXml(
                "<html><head /><body><form name='Test'><input type='text' name='Data' /></form><form name='SecondTest'><input type='checkbox' name='IsSet' /></form></body></html>");

            var page = new HtmlPageStub(doc);

            var form = page.Find<HtmlForm>().ByName("Test");

            var actual = form.Find<HtmlElement>().AllByAttribute("type", "text").ToList();

            actual.Count.Should().Be(1);
            actual[0].Should().BeAssignableTo<HtmlInput>();
        }

        /// <summary>
        ///     Runs a test for all by attribute executes query with attribute filter.
        /// </summary>
        [TestMethod]
        public void AllByAttributeExecutesQueryWithAttributeFilterTest()
        {
            var doc = new XmlDocument();

            doc.LoadXml(
                "<html><head /><body><form name='Test'><input type='text' name='Data' /></form><form name='SecondTest'><input type='checkbox' name='IsSet' /></form></body></html>");

            var page = new HtmlPageStub(doc);

            var actual = page.Find<HtmlElement>().AllByAttribute("name", "IsSet").ToList();

            actual.Count.Should().Be(1);
            actual[0].Should().BeAssignableTo<HtmlCheckBox>();
        }

        /// <summary>
        ///     Runs a test for all by attribute throws exception with empty attribute name.
        /// </summary>
        [TestMethod]
        public void AllByAttributeThrowsExceptionWithEmptyAttributeNameTest()
        {
            var target = Substitute.For<IHtmlElementFinder<HtmlElement>>();

            Action action = () => target.AllByAttribute(string.Empty, "Test");

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for all by attribute throws exception with null attribute name.
        /// </summary>
        [TestMethod]
        public void AllByAttributeThrowsExceptionWithNullAttributeNameTest()
        {
            var target = Substitute.For<IHtmlElementFinder<HtmlElement>>();

            Action action = () => target.AllByAttribute(null, "Test");

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for all by attribute throws exception with null finder.
        /// </summary>
        [TestMethod]
        public void AllByAttributeThrowsExceptionWithNullFinderTest()
        {
            var target = (IHtmlElementFinder<HtmlElement>)null;

            Action action = () => target.AllByAttribute("test", "Test");

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for all by attribute throws exception with white space attribute name.
        /// </summary>
        [TestMethod]
        public void AllByAttributeThrowsExceptionWithWhiteSpaceAttributeNameTest()
        {
            var target = Substitute.For<IHtmlElementFinder<HtmlElement>>();

            Action action = () => target.AllByAttribute("  ", "Test");

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for all by name executes query with name filter.
        /// </summary>
        [TestMethod]
        public void AllByNameExecutesQueryWithNameFilterTest()
        {
            var doc = new XmlDocument();

            doc.LoadXml(
                "<html><head /><body><form name='Test'><input type='text' name='Data' /></form><form name='SecondTest'><input type='checkbox' name='IsSet' /></form></body></html>");

            var page = new HtmlPageStub(doc);

            var form = page.Find<HtmlForm>().ByName("Test");

            var actual = form.Find<HtmlElement>().AllByName("Data").ToList();

            actual.Count.Should().Be(1);
            actual[0].Should().BeAssignableTo<HtmlInput>();
        }

        /// <summary>
        ///     Runs a test for all by name throws exception with null finder.
        /// </summary>
        [TestMethod]
        public void AllByNameThrowsExceptionWithNullFinderTest()
        {
            var target = (IHtmlElementFinder<HtmlElement>)null;

            Action action = () => target.AllByName("Test");

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for all by predicate executes returns filtered results.
        /// </summary>
        [TestMethod]
        public void AllByPredicateExecutesReturnsFilteredResultsTest()
        {
            var doc = new XmlDocument();

            doc.LoadXml(
                "<html><head /><body><form name='Test'><input type='text' name='Data' /></form><form name='SecondTest'><input type='checkbox' name='IsSet' /></form></body></html>");

            var page = new HtmlPageStub(doc);

            var actual = page.Find<HtmlElement>().AllByPredicate(x => x is HtmlInput).ToList();

            actual.Count.Should().Be(1);
            actual[0].Should().BeAssignableTo<HtmlInput>();
        }

        /// <summary>
        ///     Runs a test for all by predicate throws exception with null finder.
        /// </summary>
        [TestMethod]
        public void AllByPredicateThrowsExceptionWithNullFinderTest()
        {
            var target = (IHtmlElementFinder<HtmlElement>)null;

            Action action = () => target.AllByPredicate(x => true);

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for all by predicate throws exception with null predicate.
        /// </summary>
        [TestMethod]
        public void AllByPredicateThrowsExceptionWithNullPredicateTest()
        {
            var target = Substitute.For<IHtmlElementFinder<HtmlElement>>();

            Action action = () => target.AllByPredicate(null);

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for all by tag name executes query with tag filter.
        /// </summary>
        [TestMethod]
        public void AllByTagNameExecutesQueryWithTagFilterTest()
        {
            var doc = new XmlDocument();

            doc.LoadXml(
                "<html><head /><body><form name='Test'><input type='text' name='Data' /></form><form name='SecondTest'><input type='checkbox' name='IsSet' /></form></body></html>");

            var page = new HtmlPageStub(doc);

            var form = page.Find<HtmlForm>().ByName("Test");

            var actual = form.Find<AnyHtmlElement>().AllByTagName("input").ToList();

            actual.Count.Should().Be(1);
        }

        /// <summary>
        ///     Runs a test for all by tag name throws exception with empty tag name.
        /// </summary>
        [TestMethod]
        public void AllByTagNameThrowsExceptionWithEmptyTagNameTest()
        {
            var target = Substitute.For<IHtmlElementFinder<AnyHtmlElement>>();

            Action action = () => target.AllByTagName(string.Empty);

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for all by tag name throws exception with null finder.
        /// </summary>
        [TestMethod]
        public void AllByTagNameThrowsExceptionWithNullFinderTest()
        {
            var target = (IHtmlElementFinder<AnyHtmlElement>)null;

            Action action = () => target.AllByTagName("test");

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for all by tag name throws exception with null tag name.
        /// </summary>
        [TestMethod]
        public void AllByTagNameThrowsExceptionWithNullTagNameTest()
        {
            var target = Substitute.For<IHtmlElementFinder<AnyHtmlElement>>();

            Action action = () => target.AllByTagName(null);

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for all by tag name throws exception with white space tag name.
        /// </summary>
        [TestMethod]
        public void AllByTagNameThrowsExceptionWithWhiteSpaceTagNameTest()
        {
            var target = Substitute.For<IHtmlElementFinder<AnyHtmlElement>>();

            Action action = () => target.AllByTagName("  ");

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for all by text case sensitive throws exception with null finder.
        /// </summary>
        [TestMethod]
        public void AllByTextCaseSensitiveThrowsExceptionWithNullFinderTest()
        {
            var target = (IHtmlElementFinder<AnyHtmlElement>)null;

            Action action = () => target.AllByText("Test");

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for all by text executes case insensitive search.
        /// </summary>
        [TestMethod]
        public void AllByTextExecutesCaseInsensitiveSearchTest()
        {
            var doc = new XmlDocument();

            doc.LoadXml("<html><head /><body><p id='pascal'>First</p><p id='lower'>first</p></body></html>");

            var page = new HtmlPageStub(doc);

            var actual = page.Find<HtmlElement>().AllByText("first").ToList();

            actual.Count.Should().Be(2);
        }

        /// <summary>
        ///     Runs a test for all by text with case insensitive flag executes case insensitive search.
        /// </summary>
        [TestMethod]
        public void AllByTextWithCaseInsensitiveFlagExecutesCaseInsensitiveSearchTest()
        {
            var doc = new XmlDocument();

            doc.LoadXml("<html><head /><body><p id='pascal'>First</p><p id='lower'>first</p></body></html>");

            var page = new HtmlPageStub(doc);

            var actual = page.Find<HtmlElement>().AllByText("first", true).ToList();

            actual.Count.Should().Be(2);
        }

        /// <summary>
        ///     Runs a test for all by text with case sensitive flag executes case sensitive search.
        /// </summary>
        [TestMethod]
        public void AllByTextWithCaseSensitiveFlagExecutesCaseSensitiveSearchTest()
        {
            var doc = new XmlDocument();

            doc.LoadXml("<html><head /><body><p id='pascal'>First</p><p id='lower'>first</p></body></html>");

            var page = new HtmlPageStub(doc);

            var actual = page.Find<HtmlElement>().AllByText("first", false).ToList();

            actual.Count.Should().Be(1);
            actual[0].Id.Should().Be("lower");
        }

        /// <summary>
        ///     Runs a test for all executes base query.
        /// </summary>
        [TestMethod]
        public void AllExecutesBaseQueryTest()
        {
            var doc = new XmlDocument();

            doc.LoadXml(
                "<html><head /><body><form name='Test'><input type='text' name='Data' /></form><form name='SecondTest'><input type='checkbox' name='IsSet' /></form></body></html>");

            var page = new HtmlPageStub(doc);

            var actual = page.Find<AnyHtmlElement>().All().ToList();

            actual.Count.Should().Be(7);
        }

        /// <summary>
        ///     Runs a test for all throws exception with null finder.
        /// </summary>
        [TestMethod]
        public void AllThrowsExceptionWithNullFinderTest()
        {
            var target = (IHtmlElementFinder<HtmlElement>)null;

            Action action = () => target.All();

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for by id executes query in node context.
        /// </summary>
        [TestMethod]
        public void ByIdExecutesQueryInNodeContextTest()
        {
            var doc = new XmlDocument();

            doc.LoadXml(
                "<html><head /><body><form name='Test'><input type='text' id='DataId' name='Data' /></form><form name='SecondTest'><input type='checkbox' name='IsSet' /></form></body></html>");

            var page = new HtmlPageStub(doc);

            var form = page.Find<HtmlForm>().ByName("Test");

            var actual = form.Find<HtmlElement>().ById("DataId");

            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<HtmlInput>();
        }

        /// <summary>
        ///     Runs a test for by id throws exception with null finder.
        /// </summary>
        [TestMethod]
        public void ByIdThrowsExceptionWithNullFinderTest()
        {
            var target = (IHtmlElementFinder<HtmlElement>)null;

            Action action = () => target.ById("test");

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for by name executes query in node context.
        /// </summary>
        [TestMethod]
        public void ByNameExecutesQueryInNodeContextTest()
        {
            var doc = new XmlDocument();

            doc.LoadXml(
                "<html><head /><body><form name='Test'><input type='text' id='DataId' name='Data' /></form><form name='SecondTest'><input type='checkbox' name='IsSet' /></form></body></html>");

            var page = new HtmlPageStub(doc);

            var form = page.Find<HtmlForm>().ByName("Test");

            var actual = form.Find<HtmlElement>().ByName("Data");

            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<HtmlInput>();
        }

        /// <summary>
        ///     Runs a test for by name throws exception with null finder.
        /// </summary>
        [TestMethod]
        public void ByNameThrowsExceptionWithNullFinderTest()
        {
            var target = (IHtmlElementFinder<HtmlElement>)null;

            Action action = () => target.ByName("test");

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for by tag name executes query in node context.
        /// </summary>
        [TestMethod]
        public void ByTagNameExecutesQueryInNodeContextTest()
        {
            var doc = new XmlDocument();

            doc.LoadXml(
                "<html><head /><body><form name='Test'><input type='text' id='DataId' name='Data' /></form><form name='SecondTest'><input type='checkbox' name='IsSet' /></form></body></html>");

            var page = new HtmlPageStub(doc);

            var form = page.Find<HtmlForm>().ByName("Test");

            var actual = form.Find<AnyHtmlElement>().ByTagName("input");

            actual.Should().NotBeNull();
        }

        /// <summary>
        ///     Runs a test for by tag name throws exception with null finder.
        /// </summary>
        [TestMethod]
        public void ByTagNameThrowsExceptionWithNullFinderTest()
        {
            var target = (IHtmlElementFinder<AnyHtmlElement>)null;

            Action action = () => target.ByTagName("test");

            action.ShouldThrow<ArgumentNullException>();
        }
    }
}