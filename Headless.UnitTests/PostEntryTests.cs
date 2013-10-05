namespace Headless.UnitTests
{
    using System;
    using System.Globalization;
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
        ///     Runs a test for can create with boolean.
        /// </summary>
        [TestMethod]
        public void CanCreateWithBooleanTest()
        {
            var name = Guid.NewGuid().ToString();
            const bool Value = true;

            var target = new PostEntry(name, Value);

            target.Name.Should().Be(name);
            target.Value.Should().Be(Value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        ///     Runs a test for can create with GUID.
        /// </summary>
        [TestMethod]
        public void CanCreateWithGuidTest()
        {
            var name = Guid.NewGuid().ToString();
            var value = Guid.NewGuid();

            var target = new PostEntry(name, value);

            target.Name.Should().Be(name);
            target.Value.Should().Be(value.ToString());
        }

        /// <summary>
        ///     Runs a test for can create with int16.
        /// </summary>
        [TestMethod]
        public void CanCreateWithInt16Test()
        {
            var name = Guid.NewGuid().ToString();
            const short Value = 234;

            var target = new PostEntry(name, Value);

            target.Name.Should().Be(name);
            target.Value.Should().Be(Value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        ///     Runs a test for can create with int32.
        /// </summary>
        [TestMethod]
        public void CanCreateWithInt32Test()
        {
            var name = Guid.NewGuid().ToString();
            var value = Environment.TickCount;

            var target = new PostEntry(name, value);

            target.Name.Should().Be(name);
            target.Value.Should().Be(value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        ///     Runs a test for can create with int64.
        /// </summary>
        [TestMethod]
        public void CanCreateWithInt64Test()
        {
            var name = Guid.NewGuid().ToString();
            const long Value = 234;

            var target = new PostEntry(name, Value);

            target.Name.Should().Be(name);
            target.Value.Should().Be(Value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        ///     Runs a test for can create with null object.
        /// </summary>
        [TestMethod]
        public void CanCreateWithNullObjectTest()
        {
            var name = Guid.NewGuid().ToString();

            var target = new PostEntry(name, (object)null);

            target.Name.Should().Be(name);
            target.Value.Should().BeNull();
        }

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
        ///     Runs a test for can create with object.
        /// </summary>
        [TestMethod]
        public void CanCreateWithObjectTest()
        {
            var name = Guid.NewGuid().ToString();
            var value = new Object();

            var target = new PostEntry(name, value);

            target.Name.Should().Be(name);
            target.Value.Should().Be(value.ToString());
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
        ///     Runs a test for throws exception when created with empty name and object value.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithEmptyNameAndObjectValueTest()
        {
            Action action = () => new PostEntry(string.Empty, new object());

            action.ShouldThrow<ArgumentException>();
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
        ///     Runs a test for throws exception when created with null name and object value.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithNullNameAndObjectValueTest()
        {
            Action action = () => new PostEntry(null, new object());

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