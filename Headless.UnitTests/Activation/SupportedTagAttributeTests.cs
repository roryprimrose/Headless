namespace Headless.UnitTests.Activation
{
    using System;
    using FluentAssertions;
    using Headless.Activation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="SupportedTagAttributeTests" />
    ///     class tests the <see cref="SupportedTagAttribute" /> class.
    /// </summary>
    [TestClass]
    public class SupportedTagAttributeTests
    {
        /// <summary>
        ///     Runs a test for has attribute filter returns false if attribute name is empty.
        /// </summary>
        [TestMethod]
        public void HasAttributeFilterReturnsFalseIfAttributeNameIsEmptyTest()
        {
            var tagName = Guid.NewGuid().ToString();
            var attributeValue = Guid.NewGuid().ToString();

            var target = new SupportedTagAttribute(tagName, string.Empty, attributeValue);

            var actual = target.HasAttributeFilter;

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for has attribute filter returns false if attribute name is null.
        /// </summary>
        [TestMethod]
        public void HasAttributeFilterReturnsFalseIfAttributeNameIsNullTest()
        {
            var tagName = Guid.NewGuid().ToString();
            var attributeValue = Guid.NewGuid().ToString();

            var target = new SupportedTagAttribute(tagName, null, attributeValue);

            var actual = target.HasAttributeFilter;

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for has attribute filter returns false if attribute name is white space.
        /// </summary>
        [TestMethod]
        public void HasAttributeFilterReturnsFalseIfAttributeNameIsWhiteSpaceTest()
        {
            var tagName = Guid.NewGuid().ToString();
            var attributeValue = Guid.NewGuid().ToString();

            var target = new SupportedTagAttribute(tagName, "  ", attributeValue);

            var actual = target.HasAttributeFilter;

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for has attribute filter returns false if attribute value is empty.
        /// </summary>
        [TestMethod]
        public void HasAttributeFilterReturnsFalseIfAttributeValueIsEmptyTest()
        {
            var tagValue = Guid.NewGuid().ToString();
            var attributeName = Guid.NewGuid().ToString();

            var target = new SupportedTagAttribute(tagValue, attributeName, string.Empty);

            var actual = target.HasAttributeFilter;

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for has attribute filter returns false if attribute value is null.
        /// </summary>
        [TestMethod]
        public void HasAttributeFilterReturnsFalseIfAttributeValueIsNullTest()
        {
            var tagValue = Guid.NewGuid().ToString();
            var attributeName = Guid.NewGuid().ToString();

            var target = new SupportedTagAttribute(tagValue, attributeName, null);

            var actual = target.HasAttributeFilter;

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for has attribute filter returns false if attribute value is white space.
        /// </summary>
        [TestMethod]
        public void HasAttributeFilterReturnsFalseIfAttributeValueIsWhiteSpaceTest()
        {
            var tagValue = Guid.NewGuid().ToString();
            var attributeName = Guid.NewGuid().ToString();

            var target = new SupportedTagAttribute(tagValue, attributeName, "  ");

            var actual = target.HasAttributeFilter;

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for has attribute filter returns true if attribute is defined.
        /// </summary>
        [TestMethod]
        public void HasAttributeFilterReturnsTrueIfAttributeIsDefinedTest()
        {
            var tagName = Guid.NewGuid().ToString();
            var attributeName = Guid.NewGuid().ToString();
            var attributeValue = Guid.NewGuid().ToString();

            var target = new SupportedTagAttribute(tagName, attributeName, attributeValue);

            var actual = target.HasAttributeFilter;

            actual.Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for returns constructor values.
        /// </summary>
        [TestMethod]
        public void ReturnsConstructorValuesTest()
        {
            var tagName = Guid.NewGuid().ToString();
            var attributeName = Guid.NewGuid().ToString();
            var attributeValue = Guid.NewGuid().ToString();

            var target = new SupportedTagAttribute(tagName, attributeName, attributeValue);

            target.TagName.Should().Be(tagName);
            target.AttributeName.Should().Be(attributeName);
            target.AttributeValue.Should().Be(attributeValue);
        }

        /// <summary>
        ///     Runs a test for to string returns tag representation.
        /// </summary>
        [TestMethod]
        public void ToStringReturnsTagRepresentationTest()
        {
            var target = new SupportedTagAttribute("test");

            var actual = target.ToString();

            actual.Should().Be("<test />");
        }

        /// <summary>
        ///     Runs a test for to string returns tag representation with attribute.
        /// </summary>
        [TestMethod]
        public void ToStringReturnsTagRepresentationWithAttributeTest()
        {
            var target = new SupportedTagAttribute("test", "name", "Value");

            var actual = target.ToString();

            actual.Should().Be("<test name=\"Value\" />");
        }
    }
}