namespace Headless.UnitTests.Activation
{
    using System;
    using System.Xml.XPath;
    using FluentAssertions;
    using Headless.Activation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;

    /// <summary>
    ///     The <see cref="HtmlElementFactoryTests" />
    ///     class tests the <see cref="HtmlElementFactory" /> class.
    /// </summary>
    [TestClass]
    public class HtmlElementFactoryTests
    {
        /// <summary>
        ///     Runs a test for create returns best matching type.
        /// </summary>
        [TestMethod]
        public void CreateReturnsBestMatchingTypeTest()
        {
            var page = Substitute.For<IHtmlPage>();
            var node = Substitute.For<IXPathNavigable>();
            var navigator = Substitute.For<XPathNavigator>();

            node.CreateNavigator().Returns(navigator);
            navigator.Name.Returns("input");
            navigator.GetAttribute("type", string.Empty).Returns("text");

            var actual = HtmlElementFactory.Create<HtmlElement>(page, node);

            actual.Should().NotBeNull();
            actual.Should().BeOfType<HtmlTextElement>();
        }

        /// <summary>
        ///     Runs a test for create returns exact matching type.
        /// </summary>
        [TestMethod]
        public void CreateReturnsExactMatchingTypeTest()
        {
            var page = Substitute.For<IHtmlPage>();
            var node = Substitute.For<IXPathNavigable>();
            var navigator = Substitute.For<XPathNavigator>();

            node.CreateNavigator().Returns(navigator);
            navigator.Name.Returns("input");
            navigator.GetAttribute("type", string.Empty).Returns("text");

            var actual = HtmlElementFactory.Create<HtmlTextElement>(page, node);

            actual.Should().NotBeNull();
        }

        /// <summary>
        ///     Runs a test for create throws exception when requested type is not valid.
        /// </summary>
        [TestMethod]
        public void CreateThrowsExceptionWhenRequestedTypeIsNotValidTest()
        {
            var page = Substitute.For<IHtmlPage>();
            var node = Substitute.For<IXPathNavigable>();
            var navigator = Substitute.For<XPathNavigator>();

            node.CreateNavigator().Returns(navigator);
            navigator.Name.Returns("input");
            navigator.GetAttribute("type", string.Empty).Returns("text");

            Action action = () => HtmlElementFactory.Create<HtmlButton>(page, node);

            action.ShouldThrow<InvalidOperationException>();
        }
    }
}