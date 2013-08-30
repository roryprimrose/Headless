namespace Headless.UnitTests.Activation
{
    using System;
    using System.Linq;
    using System.Xml.XPath;
    using FluentAssertions;
    using Headless.Activation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;

    /// <summary>
    ///     The <see cref="TypeExtensionsTests" />
    ///     class tests the <see cref="Headless.Activation.TypeExtensions" /> class.
    /// </summary>
    [TestClass]
    public class TypeExtensionsTests
    {
        /// <summary>
        ///     Runs a test for find best matching type matches case insensitive tag names.
        /// </summary>
        [TestMethod]
        public void FindBestMatchingTypeMatchesCaseInsensitiveTagNamesTest()
        {
            var node = Substitute.For<IXPathNavigable>();
            var navigator = Substitute.For<XPathNavigator>();

            node.CreateNavigator().Returns(navigator);
            navigator.Name.Returns("TEXTarea");

            var target = typeof(HtmlElement);

            var actual = target.FindBestMatchingType(node);

            actual.Should().Be(typeof(HtmlInput));
        }

        /// <summary>
        ///     Runs a test for find best matching type returns any HTML element when no match found.
        /// </summary>
        [TestMethod]
        public void FindBestMatchingTypeReturnsAnyHtmlElementWhenNoMatchFoundTest()
        {
            var node = Substitute.For<IXPathNavigable>();
            var navigator = Substitute.For<XPathNavigator>();

            node.CreateNavigator().Returns(navigator);
            navigator.Name.Returns("conflict");

            var target = typeof(string);

            var actual = target.FindBestMatchingType(node);

            actual.Should().Be(typeof(AnyHtmlElement));
        }

        /// <summary>
        ///     Runs a test for find best matching type returns type matching case insensitive tag name and attribute.
        /// </summary>
        [TestMethod]
        public void FindBestMatchingTypeReturnsTypeMatchingCaseInsensitiveTagNameAndAttributeTest()
        {
            var node = Substitute.For<IXPathNavigable>();
            var navigator = Substitute.For<XPathNavigator>();

            node.CreateNavigator().Returns(navigator);
            navigator.Name.Returns("inPUT");
            navigator.GetAttribute("type", string.Empty).Returns("suBMit");

            var target = typeof(HtmlElement);

            var actual = target.FindBestMatchingType(node);

            actual.Should().Be(typeof(HtmlButton));
        }

        /// <summary>
        ///     Runs a test for find best matching type returns type matching tag name and attribute.
        /// </summary>
        [TestMethod]
        public void FindBestMatchingTypeReturnsTypeMatchingTagNameAndAttributeTest()
        {
            var node = Substitute.For<IXPathNavigable>();
            var navigator = Substitute.For<XPathNavigator>();

            node.CreateNavigator().Returns(navigator);
            navigator.Name.Returns("input");
            navigator.GetAttribute("type", string.Empty).Returns("submit");

            var target = typeof(HtmlElement);

            var actual = target.FindBestMatchingType(node);

            actual.Should().Be(typeof(HtmlButton));
        }

        /// <summary>
        ///     Runs a test for find best matching type returns type matching tag name only.
        /// </summary>
        [TestMethod]
        public void FindBestMatchingTypeReturnsTypeMatchingTagNameOnlyTest()
        {
            var node = Substitute.For<IXPathNavigable>();
            var navigator = Substitute.For<XPathNavigator>();

            node.CreateNavigator().Returns(navigator);
            navigator.Name.Returns("textarea");

            var target = typeof(HtmlElement);

            var actual = target.FindBestMatchingType(node);

            actual.Should().Be(typeof(HtmlInput));
        }

        /// <summary>
        ///     Runs a test for find best matching type throws exception when multiple matches found.
        /// </summary>
        [TestMethod]
        public void FindBestMatchingTypeThrowsExceptionWhenMultipleMatchesFoundTest()
        {
            var node = Substitute.For<IXPathNavigable>();
            var navigator = Substitute.For<XPathNavigator>();

            node.CreateNavigator().Returns(navigator);
            navigator.Name.Returns("conflict");
            navigator.OuterXml.Returns("<conflict />");

            var target = typeof(HtmlElement);

            Action action = () => target.FindBestMatchingType(node);

            action.ShouldThrow<InvalidHtmlElementMatchException>();
        }

        /// <summary>
        ///     Runs a test for find best matching type throws exception with null node.
        /// </summary>
        [TestMethod]
        public void FindBestMatchingTypeThrowsExceptionWithNullNodeTest()
        {
            var target = typeof(string);

            Action action = () => target.FindBestMatchingType(null);

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for find best matching type throws exception with null type.
        /// </summary>
        [TestMethod]
        public void FindBestMatchingTypeThrowsExceptionWithNullTypeTest()
        {
            var node = Substitute.For<IXPathNavigable>();

            var target = (Type)null;

            Action action = () => target.FindBestMatchingType(node);

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for get matching types returns cached value.
        /// </summary>
        [TestMethod]
        public void GetMatchingTypesReturnsCachedValueTest()
        {
            var first = typeof(HtmlElement).GetMatchingTypes();
            var second = typeof(HtmlElement).GetMatchingTypes();

            ReferenceEquals(first, second).Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for get matching types returns creatable types.
        /// </summary>
        [TestMethod]
        public void GetMatchingTypesReturnsCreatableTypesTest()
        {
            var actual = typeof(HtmlElement).GetMatchingTypes();

            actual.All(x => x.IsAbstract == false).Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for get matching types returns types in hierarchy.
        /// </summary>
        [TestMethod]
        public void GetMatchingTypesReturnsTypesInHierarchyTest()
        {
            var actual = typeof(HtmlElement).GetMatchingTypes();

            actual.Should().NotBeNull();
            actual.Count.Should().BeGreaterThan(1);
            actual.All(x => typeof(HtmlElement).IsAssignableFrom(x)).Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for get matching types throws exception with null type.
        /// </summary>
        [TestMethod]
        public void GetMatchingTypesThrowsExceptionWithNullTypeTest()
        {
            var target = (Type)null;

            Action action = () => target.GetMatchingTypes();

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for get supported tags attributes with tags and attributes.
        /// </summary>
        [TestMethod]
        public void GetSupportedTagsAttributesWithTagsAndAttributesTest()
        {
            var actual = typeof(HtmlInput).GetSupportedTags();

            actual.Any(x => x.TagName == "textarea").Should().BeTrue();
            actual.Any(x => x.TagName == "input" && x.AttributeName == "type" && x.AttributeValue == "text")
                .Should()
                .BeTrue();
        }

        /// <summary>
        ///     Runs a test for get supported tags returns cached value.
        /// </summary>
        [TestMethod]
        public void GetSupportedTagsReturnsCachedValueTest()
        {
            var first = typeof(HtmlElement).GetSupportedTags();
            var second = typeof(HtmlElement).GetSupportedTags();

            ReferenceEquals(first, second).Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for get supported tags returns hierarchy of tags.
        /// </summary>
        [TestMethod]
        public void GetSupportedTagsReturnsHierarchyOfTagsTest()
        {
            var actual = typeof(HtmlElement).GetSupportedTags();

            actual.Should().NotBeNull();
            actual.Count.Should().BeGreaterThan(1);
        }

        /// <summary>
        ///     Runs a test for get supported tags throws exception when type has no tags.
        /// </summary>
        [TestMethod]
        public void GetSupportedTagsThrowsExceptionWhenTypeHasNoTagsTest()
        {
            Action action = () => typeof(string).GetSupportedTags();

            action.ShouldThrow<InvalidOperationException>();
        }

        /// <summary>
        ///     Runs a test for get supported tags throws exception with null type.
        /// </summary>
        [TestMethod]
        public void GetSupportedTagsThrowsExceptionWithNullTypeTest()
        {
            var target = (Type)null;

            Action action = () => target.GetSupportedTags();

            action.ShouldThrow<ArgumentNullException>();
        }
    }
}