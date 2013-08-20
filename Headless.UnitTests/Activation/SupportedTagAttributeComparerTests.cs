namespace Headless.UnitTests.Activation
{
    using System;
    using FluentAssertions;
    using Headless.Activation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="SupportedTagAttributeComparerTests" />
    ///     class tests the <see cref="SupportedTagAttributeComparer" /> class.
    /// </summary>
    [TestClass]
    public class SupportedTagAttributeComparerTests
    {
        /// <summary>
        ///     Runs a test for equals returns false when attribute filter does not match.
        /// </summary>
        [TestMethod]
        public void EqualsReturnsFalseWhenAttributeFilterDoesNotMatchTest()
        {
            var first = new SupportedTagAttribute(Guid.NewGuid().ToString());
            var second = new SupportedTagAttribute(first.TagName, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            var target = new SupportedTagAttributeComparer();

            var actual = target.Equals(first, second);

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for equals returns false when attribute name does not match.
        /// </summary>
        [TestMethod]
        public void EqualsReturnsFalseWhenAttributeNameDoesNotMatchTest()
        {
            var first = new SupportedTagAttribute(
                Guid.NewGuid().ToString(), 
                Guid.NewGuid().ToString(), 
                Guid.NewGuid().ToString());
            var second = new SupportedTagAttribute(first.TagName, Guid.NewGuid().ToString(), first.AttributeValue);

            var target = new SupportedTagAttributeComparer();

            var actual = target.Equals(first, second);

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for equals returns false when attribute value does not match.
        /// </summary>
        [TestMethod]
        public void EqualsReturnsFalseWhenAttributeValueDoesNotMatchTest()
        {
            var first = new SupportedTagAttribute(
                Guid.NewGuid().ToString(), 
                Guid.NewGuid().ToString(), 
                Guid.NewGuid().ToString());
            var second = new SupportedTagAttribute(first.TagName, first.AttributeName, Guid.NewGuid().ToString());

            var target = new SupportedTagAttributeComparer();

            var actual = target.Equals(first, second);

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for equals returns false when first is null.
        /// </summary>
        [TestMethod]
        public void EqualsReturnsFalseWhenFirstIsNullTest()
        {
            var tag = new SupportedTagAttribute(Guid.NewGuid().ToString());

            var target = new SupportedTagAttributeComparer();

            var actual = target.Equals(null, tag);

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for equals returns false when second is null.
        /// </summary>
        [TestMethod]
        public void EqualsReturnsFalseWhenSecondIsNullTest()
        {
            var tag = new SupportedTagAttribute(Guid.NewGuid().ToString());

            var target = new SupportedTagAttributeComparer();

            var actual = target.Equals(tag, null);

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for equals returns false when tags do not match.
        /// </summary>
        [TestMethod]
        public void EqualsReturnsFalseWhenTagsDoNotMatchTest()
        {
            var first = new SupportedTagAttribute(Guid.NewGuid().ToString());
            var second = new SupportedTagAttribute(Guid.NewGuid().ToString());

            var target = new SupportedTagAttributeComparer();

            var actual = target.Equals(first, second);

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for equals returns true when tag and attribute matches.
        /// </summary>
        [TestMethod]
        public void EqualsReturnsTrueWhenTagAndAttributeMatchesTest()
        {
            var first = new SupportedTagAttribute(
                Guid.NewGuid().ToString(), 
                Guid.NewGuid().ToString(), 
                Guid.NewGuid().ToString());
            var second = new SupportedTagAttribute(first.TagName, first.AttributeName, first.AttributeValue);

            var target = new SupportedTagAttributeComparer();

            var actual = target.Equals(first, second);

            actual.Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for equals returns true when tags match without attributes.
        /// </summary>
        [TestMethod]
        public void EqualsReturnsTrueWhenTagsMatchWithoutAttributesTest()
        {
            var first = new SupportedTagAttribute(Guid.NewGuid().ToString());
            var second = new SupportedTagAttribute(first.TagName);

            var target = new SupportedTagAttributeComparer();

            var actual = target.Equals(first, second);

            actual.Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for get hash code returns different value for mismatched attribute name.
        /// </summary>
        [TestMethod]
        public void GetHashCodeReturnsDifferentValueForMismatchedAttributeNameTest()
        {
            var first = new SupportedTagAttribute(
                Guid.NewGuid().ToString(), 
                Guid.NewGuid().ToString(), 
                Guid.NewGuid().ToString());
            var second = new SupportedTagAttribute(first.TagName, Guid.NewGuid().ToString(), first.AttributeValue);

            var target = new SupportedTagAttributeComparer();

            var firstCode = target.GetHashCode(first);
            var secondCode = target.GetHashCode(second);

            firstCode.Should().NotBe(secondCode);
        }

        /// <summary>
        ///     Runs a test for get hash code returns different value for mismatched attribute value.
        /// </summary>
        [TestMethod]
        public void GetHashCodeReturnsDifferentValueForMismatchedAttributeValueTest()
        {
            var first = new SupportedTagAttribute(
                Guid.NewGuid().ToString(), 
                Guid.NewGuid().ToString(), 
                Guid.NewGuid().ToString());
            var second = new SupportedTagAttribute(first.TagName, first.AttributeName, Guid.NewGuid().ToString());

            var target = new SupportedTagAttributeComparer();

            var firstCode = target.GetHashCode(first);
            var secondCode = target.GetHashCode(second);

            firstCode.Should().NotBe(secondCode);
        }

        /// <summary>
        ///     Runs a test for get hash code returns different value for mismatched tag.
        /// </summary>
        [TestMethod]
        public void GetHashCodeReturnsDifferentValueForMismatchedTagTest()
        {
            var first = new SupportedTagAttribute(Guid.NewGuid().ToString());
            var second = new SupportedTagAttribute(Guid.NewGuid().ToString());

            var target = new SupportedTagAttributeComparer();

            var firstCode = target.GetHashCode(first);
            var secondCode = target.GetHashCode(second);

            firstCode.Should().NotBe(secondCode);
        }

        /// <summary>
        ///     Runs a test for get hash code returns same value for matching tag and attribute.
        /// </summary>
        [TestMethod]
        public void GetHashCodeReturnsSameValueForMatchingTagAndAttributeTest()
        {
            var first = new SupportedTagAttribute(
                Guid.NewGuid().ToString(), 
                Guid.NewGuid().ToString(), 
                Guid.NewGuid().ToString());
            var second = new SupportedTagAttribute(first.TagName, first.AttributeName, first.AttributeValue);

            var target = new SupportedTagAttributeComparer();

            var firstCode = target.GetHashCode(first);
            var secondCode = target.GetHashCode(second);

            firstCode.Should().Be(secondCode);
        }

        /// <summary>
        ///     Runs a test for get hash code returns same value for matching tag.
        /// </summary>
        [TestMethod]
        public void GetHashCodeReturnsSameValueForMatchingTagTest()
        {
            var first = new SupportedTagAttribute(Guid.NewGuid().ToString());
            var second = new SupportedTagAttribute(first.TagName);

            var target = new SupportedTagAttributeComparer();

            var firstCode = target.GetHashCode(first);
            var secondCode = target.GetHashCode(second);

            firstCode.Should().Be(secondCode);
        }

        /// <summary>
        ///     Runs a test for get hash code throws exception with null object.
        /// </summary>
        [TestMethod]
        public void GetHashCodeThrowsExceptionWithNullObjectTest()
        {
            var target = new SupportedTagAttributeComparer();

            Action action = () => target.GetHashCode(null);

            action.ShouldThrow<ArgumentNullException>();
        }
    }
}