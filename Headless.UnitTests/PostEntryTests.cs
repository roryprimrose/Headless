namespace Headless.UnitTests
{
    using System;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="PostEntryTests" />
    ///     class tests the <see cref="PostEntry" /> class.
    /// </summary>
    [TestClass]
    public class PostEntryTests
    {
        /// <summary>
        ///     Runs a test for can create with null value.
        /// </summary>
        [TestMethod]
        public void CanCreateWithNullValueTest()
        {
            var name = Guid.NewGuid().ToString();

            var target = new PostEntry(name, null);

            target.Name.Should().Be(name);
            target.Value.Should().BeNull();
        }

        /// <summary>
        ///     Runs a test for can create with value data.
        /// </summary>
        [TestMethod]
        public void CanCreateWithValueDataTest()
        {
            var name = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();

            var target = new PostEntry(name, value);

            target.Name.Should().Be(name);
            target.Value.Should().Be(value);
        }

        /// <summary>
        ///     Runs a test for throws exception when created with empty name.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithEmptyNameTest()
        {
            Action action = () => new PostEntry(string.Empty, Guid.NewGuid().ToString());

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for throws exception when created with null name.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithNullNameTest()
        {
            Action action = () => new PostEntry(null, Guid.NewGuid().ToString());

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for throws exception when created with white space name.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithWhiteSpaceNameTest()
        {
            Action action = () => new PostEntry("  ", Guid.NewGuid().ToString());

            action.ShouldThrow<ArgumentException>();
        }
    }
}