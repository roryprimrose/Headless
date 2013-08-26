namespace Headless.UnitTests
{
    using System;
    using System.IO;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="PostFileEntryTests" />
    ///     class tests the <see cref="PostFileEntry" /> class.
    /// </summary>
    [TestClass]
    public class PostFileEntryTests
    {
        /// <summary>
        ///     Runs a test for can create with null value.
        /// </summary>
        [TestMethod]
        public void CanCreateWithNullValueTest()
        {
            var name = Guid.NewGuid().ToString();

            var target = new PostFileEntry(name, null);

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

            var target = new PostFileEntry(name, value);

            target.Name.Should().Be(name);
            target.Value.Should().Be(value);
        }

        /// <summary>
        ///     Runs a test for is valid returns false when file does not exist.
        /// </summary>
        [TestMethod]
        public void IsValidReturnsFalseWhenFileDoesNotExistTest()
        {
            var name = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();

            var target = new PostFileEntry(name, value);

            target.IsValid.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for is valid returns false when value is empty.
        /// </summary>
        [TestMethod]
        public void IsValidReturnsFalseWhenValueIsEmptyTest()
        {
            var name = Guid.NewGuid().ToString();

            var target = new PostFileEntry(name, string.Empty);

            target.IsValid.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for is valid returns false when value is null.
        /// </summary>
        [TestMethod]
        public void IsValidReturnsFalseWhenValueIsNullTest()
        {
            var name = Guid.NewGuid().ToString();

            var target = new PostFileEntry(name, null);

            target.IsValid.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for is valid returns false when value is white space.
        /// </summary>
        [TestMethod]
        public void IsValidReturnsFalseWhenValueIsWhiteSpaceTest()
        {
            var name = Guid.NewGuid().ToString();

            var target = new PostFileEntry(name, "  ");

            target.IsValid.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for is valid returns true for valid file path value.
        /// </summary>
        [TestMethod]
        public void IsValidReturnsTrueForValidFilePathValueTest()
        {
            var name = Guid.NewGuid().ToString();
            var value = Path.GetTempFileName();

            try
            {
                var target = new PostFileEntry(name, value);

                target.IsValid.Should().BeTrue();
            }
            finally
            {
                File.Delete(value);
            }
        }

        /// <summary>
        ///     Runs a test for read content returns file data.
        /// </summary>
        [TestMethod]
        public void ReadContentReturnsFileDataTest()
        {
            var name = Guid.NewGuid().ToString();
            var value = Path.GetTempFileName();
            var expected = Guid.NewGuid().ToString();

            try
            {
                File.WriteAllText(value, expected);

                var target = new PostFileEntry(name, value);

                using (var stream = target.ReadContent())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var actual = reader.ReadToEnd();

                        actual.Should().Be(expected);
                    }
                }

                target.IsValid.Should().BeTrue();
            }
            finally
            {
                File.Delete(value);
            }
        }

        /// <summary>
        ///     Runs a test for throws exception when created with empty name.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithEmptyNameTest()
        {
            Action action = () => new PostFileEntry(string.Empty, Guid.NewGuid().ToString());

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for throws exception when created with null name.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithNullNameTest()
        {
            Action action = () => new PostFileEntry(null, Guid.NewGuid().ToString());

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for throws exception when created with white space name.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithWhiteSpaceNameTest()
        {
            Action action = () => new PostFileEntry("  ", Guid.NewGuid().ToString());

            action.ShouldThrow<ArgumentException>();
        }
    }
}