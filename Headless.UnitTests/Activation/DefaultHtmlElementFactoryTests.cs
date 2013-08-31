namespace Headless.UnitTests.Activation
{
    using System;
    using System.Xml.XPath;
    using FluentAssertions;
    using Headless.Activation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;

    /// <summary>
    ///     The <see cref="DefaultHtmlElementFactoryTests" />
    ///     class tests the <see cref="DefaultHtmlElementFactory" /> class.
    /// </summary>
    [TestClass]
    public class DefaultHtmlElementFactoryTests
    {
        /// <summary>
        ///     Runs a test for create returns best matching type.
        /// </summary>
        [TestMethod]
        public void CreateReturnsBestMatchingTypeTest()
        {
            var page = new HtmlPageStub("<input type='text' />");

            var target = new DefaultHtmlElementFactory();

            var actual = target.Create<HtmlElement>(page, page.Node);

            actual.Should().NotBeNull();
            actual.Should().BeOfType<HtmlInput>();
        }

        /// <summary>
        ///     Runs a test for create returns exact matching type.
        /// </summary>
        [TestMethod]
        public void CreateReturnsExactMatchingTypeTest()
        {
            var page = new HtmlPageStub("<input type='text' />");

            var target = new DefaultHtmlElementFactory();

            var actual = target.Create<HtmlInput>(page, page.Node);

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

            var target = new DefaultHtmlElementFactory();

            Action action = () => target.Create<HtmlButton>(page, node);

            action.ShouldThrow<InvalidOperationException>();
        }
    }
}