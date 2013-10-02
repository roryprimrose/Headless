namespace Headless.UnitTests
{
    using System;
    using System.Linq;
    using System.Xml;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;

    /// <summary>
    ///     The <see cref="HtmlListItemTests" />
    ///     class tests the <see cref="HtmlListItem" /> class.
    /// </summary>
    [TestClass]
    public class HtmlListItemTests
    {
        /// <summary>
        ///     Runs a test for build post data returns empty set when item is selected.
        /// </summary>
        [TestMethod]
        public void BuildPostDataReturnsEmptySetWhenItemIsSelectedTest()
        {
            const string Html = "<form><select name='data'><option selected='selected' value='test'/></select></form>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().Singular();

            var parameters = form.BuildPostParameters(null).ToList();

            // Should only contain an entry for the select, not the option as well
            parameters.Count.Should().Be(1);
        }

        /// <summary>
        ///     Runs a test for post value returns text when value attribute is missing.
        /// </summary>
        [TestMethod]
        public void PostValueReturnsTextWhenValueAttributeIsMissingTest()
        {
            var page = Substitute.For<IHtmlPage>();
            var doc = new XmlDocument();

            doc.LoadXml("<option selected='selected'>contents</option>");

            var target = new HtmlListItem(page, doc.DocumentElement);

            target.PostValue.Should().Be("contents");
        }

        /// <summary>
        ///     Runs a test for post value returns text when value is empty.
        /// </summary>
        [TestMethod]
        public void PostValueReturnsTextWhenValueIsEmptyTest()
        {
            var page = Substitute.For<IHtmlPage>();
            var doc = new XmlDocument();

            doc.LoadXml("<option selected='selected' value=''>contents</option>");

            var target = new HtmlListItem(page, doc.DocumentElement);

            target.PostValue.Should().Be("contents");
        }

        /// <summary>
        ///     Runs a test for post value returns value attribute.
        /// </summary>
        [TestMethod]
        public void PostValueReturnsValueAttributeTest()
        {
            var page = Substitute.For<IHtmlPage>();
            var doc = new XmlDocument();

            doc.LoadXml("<option selected='selected' value='test'>contents</option>");

            var target = new HtmlListItem(page, doc.DocumentElement);

            target.PostValue.Should().Be("test");
        }

        /// <summary>
        ///     Runs a test for selected returns false when selected attribute is missing.
        /// </summary>
        [TestMethod]
        public void SelectedReturnsFalseWhenSelectedAttributeIsMissingTest()
        {
            var page = Substitute.For<IHtmlPage>();
            var doc = new XmlDocument();

            doc.LoadXml("<option value='test'>contents</option>");

            var target = new HtmlListItem(page, doc.DocumentElement);

            target.Selected.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for selected returns true when selected attribute defined and is empty.
        /// </summary>
        [TestMethod]
        public void SelectedReturnsTrueWhenSelectedAttributeDefinedAndIsEmptyTest()
        {
            var page = Substitute.For<IHtmlPage>();
            var doc = new XmlDocument();

            doc.LoadXml("<option selected='' value='test'>contents</option>");

            var target = new HtmlListItem(page, doc.DocumentElement);

            target.Selected.Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for selected returns true when selected attribute defined.
        /// </summary>
        [TestMethod]
        public void SelectedReturnsTrueWhenSelectedAttributeDefinedTest()
        {
            var page = Substitute.For<IHtmlPage>();
            var doc = new XmlDocument();

            doc.LoadXml("<option selected='selected' value='test'>contents</option>");

            var target = new HtmlListItem(page, doc.DocumentElement);

            target.Selected.Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for setting selected to false removes attribute.
        /// </summary>
        [TestMethod]
        public void SettingSelectedToFalseRemovesAttributeTest()
        {
            var page = Substitute.For<IHtmlPage>();
            var doc = new XmlDocument();

            doc.LoadXml("<option selected='selected' value='test'>contents</option>");

            var target = new HtmlListItem(page, doc.DocumentElement);

            target.Selected = false;

            target.Selected.Should().BeFalse();
            target.Html.Should().NotContain("selected");
        }

        /// <summary>
        ///     Runs a test for setting selected to true adds missing attribute.
        /// </summary>
        [TestMethod]
        public void SettingSelectedToTrueAddsMissingAttributeTest()
        {
            var page = Substitute.For<IHtmlPage>();
            var doc = new XmlDocument();

            doc.LoadXml("<option value='test'>contents</option>");

            var target = new HtmlListItem(page, doc.DocumentElement);

            target.Selected = true;

            target.Selected.Should().BeTrue();

            target.Html.Should().Contain("selected=\"selected\"");
        }

        /// <summary>
        ///     Runs a test for throws exception when created with null node.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithNullNodeTest()
        {
            var page = Substitute.For<IHtmlPage>();

            Action action = () => new HtmlListItem(page, null);

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for throws exception when created with null page.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithNullPageTest()
        {
            var doc = new XmlDocument();

            doc.LoadXml("<option selected='selected' value='test'/>");

            Action action = () => new HtmlListItem(null, doc.DocumentElement);

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for throws exception when created with unsupported node.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithUnsupportedNodeTest()
        {
            var page = Substitute.For<IHtmlPage>();
            var doc = new XmlDocument();

            doc.LoadXml("<form><select><option selected='selected' value='test'/></select></form>");

            Action action = () => new HtmlListItem(page, doc.DocumentElement);

            action.ShouldThrow<InvalidHtmlElementException>();
        }
    }
}