namespace Headless.UnitTests
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;

    /// <summary>
    ///     The <see cref="HtmlElementFinderTests" />
    ///     class tests the <see cref="DefaultHtmlElementFinder{T}" /> class.
    /// </summary>
    [TestClass]
    public class HtmlElementFinderTests
    {
        /// <summary>
        ///     Runs a test for all by attribute executes case insensitive query in node context.
        /// </summary>
        [TestMethod]
        public void AllByAttributeExecutesCaseInsensitiveQueryInNodeContextTest()
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
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            var actual = form.Find<HtmlFormElement>().AllByAttribute("name", "data").ToList();

            actual.Count.Should().Be(1);
            actual[0].Should().BeAssignableTo<HtmlInput>();
            actual[0].Name.Should().Be("Data");
        }

        /// <summary>
        ///     Runs a test for all by attribute executes case insensitive query.
        /// </summary>
        [TestMethod]
        public void AllByAttributeExecutesCaseInsensitiveQueryTest()
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
</html>";

            var page = new HtmlPageStub(Html);

            var actual = page.Find<HtmlFormElement>().AllByAttribute("name", "IsSET").ToList();

            actual.Count.Should().Be(1);
            actual[0].Should().BeAssignableTo<HtmlCheckBox>();
            actual[0].Name.Should().Be("IsSet");
        }

        /// <summary>
        ///     Runs a test for all by attribute throws exception with empty attribute name.
        /// </summary>
        [TestMethod]
        public void AllByAttributeThrowsExceptionWithEmptyAttributeNameTest()
        {
            var target = Substitute.For<HtmlElementFinderBase<HtmlElement>>();

            Action action = () => target.AllByAttribute(string.Empty, "Test");

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for all by attribute throws exception with null attribute name.
        /// </summary>
        [TestMethod]
        public void AllByAttributeThrowsExceptionWithNullAttributeNameTest()
        {
            var target = Substitute.For<HtmlElementFinderBase<HtmlElement>>();

            Action action = () => target.AllByAttribute(null, "Test");

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for all by attribute throws exception with white space attribute name.
        /// </summary>
        [TestMethod]
        public void AllByAttributeThrowsExceptionWithWhiteSpaceAttributeNameTest()
        {
            var target = Substitute.For<HtmlElementFinderBase<HtmlElement>>();

            Action action = () => target.AllByAttribute("  ", "Test");

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for all by attribute with case insensitive flag executes case insensitive query.
        /// </summary>
        [TestMethod]
        public void AllByAttributeWithCaseInsensitiveFlagExecutesCaseInsensitiveQueryTest()
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
</html>";

            var page = new HtmlPageStub(Html);

            var actual = page.Find<HtmlFormElement>().AllByAttribute("name", "IsSET", true).ToList();

            actual.Count.Should().Be(1);
            actual[0].Should().BeAssignableTo<HtmlCheckBox>();
            actual[0].Name.Should().Be("IsSet");
        }

        /// <summary>
        ///     Runs a test for all by attribute with case sensitive flag executes case sensitive query.
        /// </summary>
        [TestMethod]
        public void AllByAttributeWithCaseSensitiveFlagExecutesCaseSensitiveQueryTest()
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
</html>";

            var page = new HtmlPageStub(Html);

            var actual = page.Find<HtmlFormElement>().AllByAttribute("name", "IsSet", false).ToList();

            actual.Count.Should().Be(1);
            actual[0].Should().BeAssignableTo<HtmlCheckBox>();
            actual[0].Name.Should().Be("IsSet");

            actual = page.Find<HtmlFormElement>().AllByAttribute("name", "IsSET", false).ToList();

            actual.Count.Should().Be(0);
        }

        /// <summary>
        ///     Runs a test for all by name executes case insensitive query with name filter.
        /// </summary>
        [TestMethod]
        public void AllByNameExecutesCaseInsensitiveQueryWithNameFilterTest()
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

            var form = page.Find<HtmlForm>().ByName("Test");

            var actual = form.Find<HtmlFormElement>().AllByName("DATA").ToList();

            actual.Count.Should().Be(1);
            actual[0].Should().BeAssignableTo<HtmlInput>();
            actual[0].Name.Should().Be("Data");
        }

        /// <summary>
        ///     Runs a test for all by name with case insensitive flag executes case insensitive query with name filter.
        /// </summary>
        [TestMethod]
        public void AllByNameWithCaseInsensitiveFlagExecutesCaseInsensitiveQueryWithNameFilterTest()
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

            var form = page.Find<HtmlForm>().ByName("Test");

            var actual = form.Find<HtmlFormElement>().AllByName("DATA", true).ToList();

            actual.Count.Should().Be(1);
            actual[0].Should().BeAssignableTo<HtmlInput>();
            actual[0].Name.Should().Be("Data");
        }

        /// <summary>
        ///     Runs a test for all by name with case sensitive flag executes case sensitive query with name filter.
        /// </summary>
        [TestMethod]
        public void AllByNameWithCaseSensitiveFlagExecutesCaseSensitiveQueryWithNameFilterTest()
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

            var form = page.Find<HtmlForm>().ByName("Test");

            var actual = form.Find<HtmlFormElement>().AllByName("Data", false).ToList();

            actual.Count.Should().Be(1);
            actual[0].Should().BeAssignableTo<HtmlInput>();
            actual[0].Name.Should().Be("Data");

            actual = form.Find<HtmlFormElement>().AllByName("DATA", false).ToList();

            actual.Count.Should().Be(0);
        }

        /// <summary>
        ///     Runs a test for all by predicate executes returns filtered results.
        /// </summary>
        [TestMethod]
        public void AllByPredicateExecutesReturnsFilteredResultsTest()
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

            var actual = page.Find<HtmlElement>().AllByPredicate(x => x is HtmlInput).ToList();

            actual.Count.Should().Be(1);
            actual[0].Should().BeAssignableTo<HtmlInput>();
        }

        /// <summary>
        ///     Runs a test for all by predicate throws exception with null predicate.
        /// </summary>
        [TestMethod]
        public void AllByPredicateThrowsExceptionWithNullPredicateTest()
        {
            var target = Substitute.For<HtmlElementFinderBase<HtmlElement>>();

            Action action = () => target.AllByPredicate(null);

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for all by tag name executes case insensitive query with tag filter.
        /// </summary>
        [TestMethod]
        public void AllByTagNameExecutesCaseInsensitiveQueryWithTagFilterTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <INPUT type='text' name='Data' />
        </form>
        <form name='SecondTest'>
            <input type='checkbox' name='IsSet' />
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            var actual = form.Find<AnyHtmlElement>().AllByTagName("input").ToList();

            actual.Count.Should().Be(1);
        }

        /// <summary>
        ///     Runs a test for all by tag name fails to resolve element in XHTML document.
        /// </summary>
        /// <remarks>Related to https://github.com/roryprimrose/Headless/issues/10 </remarks>
        [TestMethod]
        public void AllByTagNameFailsToResolveElementInXHTMLDocumentTest()
        {
            const string Html = @"<html xmlns=""http://www.w3.org/1999/xhtml""> 
<head> 
<title>IIS 8.0 Detailed Error - 500.0 - Internal Server Error</title>  
</head> 
<body> 
<div id=""content""> 
<div class=""content-container""> 
  <h3>HTTP Error 500.0 - Internal Server Error</h3> 
  <h4>The page cannot be displayed because an internal server error has occurred.</h4> 
</div> 
</div> 
</body> 
</html>";

            var page = new HtmlPageStub(Html);

            var elements = page.Find<AnyHtmlElement>().AllByTagName("h3").ToList();

            elements.Count.Should().Be(1);
        }

        /// <summary>
        ///     Runs a test for all by tag name throws exception with empty tag name.
        /// </summary>
        [TestMethod]
        public void AllByTagNameThrowsExceptionWithEmptyTagNameTest()
        {
            var target = Substitute.For<HtmlElementFinderBase<AnyHtmlElement>>();

            Action action = () => target.AllByTagName(string.Empty);

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for all by tag name throws exception with null tag name.
        /// </summary>
        [TestMethod]
        public void AllByTagNameThrowsExceptionWithNullTagNameTest()
        {
            var target = Substitute.For<HtmlElementFinderBase<AnyHtmlElement>>();

            Action action = () => target.AllByTagName(null);

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for all by tag name throws exception with white space tag name.
        /// </summary>
        [TestMethod]
        public void AllByTagNameThrowsExceptionWithWhiteSpaceTagNameTest()
        {
            var target = Substitute.For<HtmlElementFinderBase<AnyHtmlElement>>();

            Action action = () => target.AllByTagName("  ");

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for all by text executes case insensitive search.
        /// </summary>
        [TestMethod]
        public void AllByTextExecutesCaseInsensitiveSearchTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <p id='pascal'>First</p>
        <p id='lower'>first</p>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var actual = page.Find<HtmlElement>().AllByText("first").ToList();

            actual.Count.Should().Be(2);
        }

        /// <summary>
        ///     Runs a test for all by text with case insensitive flag executes case insensitive search.
        /// </summary>
        [TestMethod]
        public void AllByTextWithCaseInsensitiveFlagExecutesCaseInsensitiveSearchTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <p id='pascal'>First</p>
        <p id='lower'>first</p>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var actual = page.Find<HtmlElement>().AllByText("first", true).ToList();

            actual.Count.Should().Be(2);
        }

        /// <summary>
        ///     Runs a test for all by text with case sensitive flag executes case sensitive search.
        /// </summary>
        [TestMethod]
        public void AllByTextWithCaseSensitiveFlagExecutesCaseSensitiveSearchTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <p id='pascal'>First</p>
        <p id='lower'>first</p>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var actual = page.Find<HtmlElement>().AllByText("first", false).ToList();

            actual.Count.Should().Be(1);
            actual[0].Id.Should().Be("lower");
        }

        /// <summary>
        ///     Runs a test for all by text with null criteria matches empty HTML elements.
        /// </summary>
        [TestMethod]
        public void AllByTextWithNullCriteriaMatchesEmptyHtmlElementsTest()
        {
            const string Html = @"
<html>
    <body>
        <p id='pascal'></p>
        <p id='lower'>first</p>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var actual = page.Find<HtmlElement>().AllByText(null).ToList();

            actual.Count.Should().Be(1);
            actual[0].Id.Should().Be("pascal");
        }

        /// <summary>
        ///     Runs a test for all by value executes case insensitive query with value filter.
        /// </summary>
        [TestMethod]
        public void AllByValueExecutesCaseInsensitiveQueryWithValueFilterTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form value='Test'>
            <input type='text' value='Data' />
        </form>
        <form value='SecondTest'>
            <input type='checkbox' value='IsSet' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByValue("Test");

            var actual = form.Find<HtmlFormElement>().AllByValue("DATA").ToList();

            actual.Count.Should().Be(1);
            actual[0].Should().BeAssignableTo<HtmlInput>();
            actual[0].Value.Should().Be("Data");
        }

        /// <summary>
        ///     Runs a test for all by value with case insensitive flag executes case insensitive query with value filter.
        /// </summary>
        [TestMethod]
        public void AllByValueWithCaseInsensitiveFlagExecutesCaseInsensitiveQueryWithValueFilterTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form value='Test'>
            <input type='text' value='Data' />
        </form>
        <form value='SecondTest'>
            <input type='checkbox' value='IsSet' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByValue("Test");

            var actual = form.Find<HtmlFormElement>().AllByValue("DATA", true).ToList();

            actual.Count.Should().Be(1);
            actual[0].Should().BeAssignableTo<HtmlInput>();
            actual[0].Value.Should().Be("Data");
        }

        /// <summary>
        ///     Runs a test for all by value with case sensitive flag executes case sensitive query with value filter.
        /// </summary>
        [TestMethod]
        public void AllByValueWithCaseSensitiveFlagExecutesCaseSensitiveQueryWithValueFilterTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form value='Test'>
            <input type='text' value='Data' />
        </form>
        <form value='SecondTest'>
            <input type='checkbox' value='IsSet' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByValue("Test");

            var actual = form.Find<HtmlFormElement>().AllByValue("Data", false).ToList();

            actual.Count.Should().Be(1);
            actual[0].Should().BeAssignableTo<HtmlInput>();
            actual[0].Value.Should().Be("Data");

            actual = form.Find<HtmlFormElement>().AllByValue("DATA", false).ToList();

            actual.Count.Should().Be(0);
        }

        /// <summary>
        ///     Runs a test for all executes base query.
        /// </summary>
        [TestMethod]
        public void AllExecutesBaseQueryTest()
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

            var actual = page.Find<AnyHtmlElement>().All().ToList();

            actual.Count.Should().Be(7);
        }

        /// <summary>
        ///     Runs a test for by attribute executes case insensitive query in node context.
        /// </summary>
        [TestMethod]
        public void ByAttributeExecutesCaseInsensitiveQueryInNodeContextTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test' id='FirstForm'>
            <input type='text' id='DataId' name='Data' />
        </form>
        <form name='SecondTest'>
            <input type='checkbox' name='IsSet' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByAttribute("Name", "test");

            form.Id.Should().Be("FirstForm");
            form.Name.Should().Be("Test");
        }

        /// <summary>
        ///     Runs a test for by name with case insensitive flag executes case insensitive query in node context.
        /// </summary>
        [TestMethod]
        public void ByAttributeWithCaseInsensitiveFlagExecutesCaseInsensitiveQueryInNodeContextTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test' id='FirstForm'>
            <input type='text' id='DataId' name='Data' />
        </form>
        <form name='SecondTest'>
            <input type='checkbox' name='IsSet' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByAttribute("Name", "test", true);

            form.Id.Should().Be("FirstForm");
            form.Name.Should().Be("Test");
        }

        /// <summary>
        ///     Runs a test for by name with case sensitive flag executes case sensitive query in node context.
        /// </summary>
        [TestMethod]
        public void ByAttributeWithCaseSensitiveFlagExecutesCaseSensitiveQueryInNodeContextTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test' id='FirstForm'>
            <input type='text' id='DataId' name='Data' />
        </form>
        <form name='SecondTest'>
            <input type='checkbox' name='IsSet' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByAttribute("name", "Test", false);

            form.Id.Should().Be("FirstForm");
            form.Name.Should().Be("Test");
        }

        /// <summary>
        ///     Runs a test for by id executes case insensitive query in node context.
        /// </summary>
        [TestMethod]
        public void ByIdExecutesCaseInsensitiveQueryInNodeContextTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <input type='text' id='DATAId' name='Data' />
        </form>
        <form name='SecondTest'>
            <input type='checkbox' name='IsSet' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            var actual = form.Find<HtmlElement>().ById("DataId");

            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<HtmlInput>();
            actual.Id.Should().Be("DATAId");
        }

        /// <summary>
        ///     Runs a test for by id with case insensitive flag executes case insensitive query in node context.
        /// </summary>
        [TestMethod]
        public void ByIdWithCaseInsensitiveFlagExecutesCaseInsensitiveQueryInNodeContextTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <input type='text' id='DATAId' name='Data' />
        </form>
        <form name='SecondTest'>
            <input type='checkbox' name='IsSet' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            var actual = form.Find<HtmlElement>().ById("DataId", true);

            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<HtmlInput>();
            actual.Id.Should().Be("DATAId");
        }

        /// <summary>
        ///     Runs a test for by id with case sensitive flag executes case sensitive query in node context.
        /// </summary>
        [TestMethod]
        public void ByIdWithCaseSensitiveFlagExecutesCaseSensitiveQueryInNodeContextTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <input type='text' id='DATAId' name='Data' />
        </form>
        <form name='SecondTest'>
            <input type='checkbox' name='IsSet' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            var actual = form.Find<HtmlElement>().ById("DATAId", false);

            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<HtmlInput>();
            actual.Id.Should().Be("DATAId");

            Action action = () => form.Find<HtmlElement>().ById("DataId", false);

            action.ShouldThrow<InvalidHtmlElementMatchException>();
        }

        /// <summary>
        ///     Runs a test for by name executes case insensitive query in node context.
        /// </summary>
        [TestMethod]
        public void ByNameExecutesCaseInsensitiveQueryInNodeContextTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <input type='text' id='DataId' name='Data' />
        </form>
        <form name='SecondTest'>
            <input type='checkbox' name='IsSet' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            var actual = form.Find<HtmlFormElement>().ByName("DATA");

            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<HtmlInput>();
            actual.Name.Should().Be("Data");
        }

        /// <summary>
        ///     Runs a test for by name with case insensitive flag executes case insensitive query in node context.
        /// </summary>
        [TestMethod]
        public void ByNameWithCaseInsensitiveFlagExecutesCaseInsensitiveQueryInNodeContextTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <input type='text' id='DataId' name='Data' />
        </form>
        <form name='SecondTest'>
            <input type='checkbox' name='IsSet' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            var actual = form.Find<HtmlFormElement>().ByName("DATA", true);

            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<HtmlInput>();
            actual.Name.Should().Be("Data");
        }

        /// <summary>
        ///     Runs a test for by name with case sensitive flag executes case sensitive query in node context.
        /// </summary>
        [TestMethod]
        public void ByNameWithCaseSensitiveFlagExecutesCaseSensitiveQueryInNodeContextTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <input type='text' id='DataId' name='Data' />
        </form>
        <form name='SecondTest'>
            <input type='checkbox' name='IsSet' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            var actual = form.Find<HtmlFormElement>().ByName("Data", false);

            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<HtmlInput>();
            actual.Name.Should().Be("Data");

            Action action = () => form.Find<HtmlFormElement>().ByName("DATA", false);

            action.ShouldThrow<InvalidHtmlElementMatchException>();
        }

        /// <summary>
        ///     Runs a test for by predicate returns matching element.
        /// </summary>
        [TestMethod]
        public void ByPredicateReturnsMatchingElementTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <input type='text' id='DataId' name='Data' />
        </form>
        <form name='SecondTest'>
            <input type='checkbox' name='IsSet' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByPredicate(x => x.Name == "SecondTest");

            form.Name.Should().Be("SecondTest");
        }

        /// <summary>
        ///     Runs a test for by predicate throws exception with null predicate.
        /// </summary>
        [TestMethod]
        public void ByPredicateThrowsExceptionWithNullPredicateTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <input type='text' id='DataId' name='Data' />
        </form>
        <form name='SecondTest'>
            <input type='checkbox' name='IsSet' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            Action action = () => page.Find<HtmlForm>().ByPredicate(null);

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for by tag name executes case insensitive query in node context.
        /// </summary>
        [TestMethod]
        public void ByTagNameExecutesCaseInsensitiveQueryInNodeContextTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <input type='text' id='DataId' name='Data' />
        </form>
        <form name='SecondTest'>
            <input type='checkbox' name='IsSet' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            var actual = form.Find<AnyHtmlElement>().ByTagName("input");

            actual.Should().NotBeNull();
        }

        /// <summary>
        ///     Runs a test for by text executes case insensitive query in node context.
        /// </summary>
        [TestMethod]
        public void ByTextExecutesCaseInsensitiveQueryInNodeContextTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <p id='target'>Stuff</p>
        <p>More</p>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var actual = page.Find<HtmlElement>().ByText("stuff");

            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<AnyHtmlElement>();
            actual.Id.Should().Be("target");
        }

        /// <summary>
        ///     Runs a test for by text with case insensitive flag executes case insensitive query in node context.
        /// </summary>
        [TestMethod]
        public void ByTextWithCaseInsensitiveFlagExecutesCaseInsensitiveQueryInNodeContextTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <p id='target'>Stuff</p>
        <p>More</p>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var actual = page.Find<HtmlElement>().ByText("STUFF", true);

            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<AnyHtmlElement>();
            actual.Id.Should().Be("target");
        }

        /// <summary>
        ///     Runs a test for by text with case sensitive flag executes case sensitive query in node context.
        /// </summary>
        [TestMethod]
        public void ByTextWithCaseSensitiveFlagExecutesCaseSensitiveQueryInNodeContextTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <p id='target'>Stuff</p>
        <p>More</p>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var actual = page.Find<AnyHtmlElement>().ByText("Stuff", false);

            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<AnyHtmlElement>();
            actual.Id.Should().Be("target");

            Action action = () => page.Find<AnyHtmlElement>().ByText("stuff", false);

            action.ShouldThrow<InvalidHtmlElementMatchException>();
        }

        /// <summary>
        ///     Runs a test for by value executes case insensitive query in node context.
        /// </summary>
        [TestMethod]
        public void ByValueExecutesCaseInsensitiveQueryInNodeContextTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form value='Test'>
            <input type='text' id='DataId' value='Data' />
        </form>
        <form value='SecondTest'>
            <input type='checkbox' value='IsSet' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByValue("Test");

            var actual = form.Find<HtmlFormElement>().ByValue("DATA");

            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<HtmlInput>();
            actual.Value.Should().Be("Data");
        }

        /// <summary>
        ///     Runs a test for by value with case insensitive flag executes case insensitive query in node context.
        /// </summary>
        [TestMethod]
        public void ByValueWithCaseInsensitiveFlagExecutesCaseInsensitiveQueryInNodeContextTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form value='Test'>
            <input type='text' id='DataId' value='Data' />
        </form>
        <form value='SecondTest'>
            <input type='checkbox' value='IsSet' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByValue("Test");

            var actual = form.Find<HtmlFormElement>().ByValue("DATA", true);

            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<HtmlInput>();
            actual.Value.Should().Be("Data");
        }

        /// <summary>
        ///     Runs a test for by value with case sensitive flag executes case sensitive query in node context.
        /// </summary>
        [TestMethod]
        public void ByValueWithCaseSensitiveFlagExecutesCaseSensitiveQueryInNodeContextTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form value='Test'>
            <input type='text' id='DataId' value='Data' />
        </form>
        <form value='SecondTest'>
            <input type='checkbox' value='IsSet' />
        </form>
    </body>
</html>
";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByValue("Test");

            var actual = form.Find<HtmlFormElement>().ByValue("Data", false);

            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<HtmlInput>();
            actual.Value.Should().Be("Data");

            Action action = () => form.Find<HtmlFormElement>().ByValue("DATA", false);

            action.ShouldThrow<InvalidHtmlElementMatchException>();
        }
    }
}