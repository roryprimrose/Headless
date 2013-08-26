namespace Headless.UnitTests
{
    using System;
    using System.IO;
    using System.Text;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;

    /// <summary>
    ///     The <see cref="PostFileStreamEntryTests" />
    ///     class tests the <see cref="PostFileStreamEntry" /> class.
    /// </summary>
    [TestClass]
    public class PostFileStreamEntryTests
    {
        /// <summary>
        ///     Runs a test for can create with null value.
        /// </summary>
        [TestMethod]
        public void CanCreateWithNullValueTest()
        {
            var name = Guid.NewGuid().ToString();

            var stream = Substitute.For<Stream>();

            using (var target = new PostFileStreamEntry(name, null, stream))
            {
                target.Name.Should().Be(name);
                target.Value.Should().BeNull();

                var actual = target.ReadContent();

                actual.Should().BeSameAs(stream);
            }
        }

        /// <summary>
        ///     Runs a test for can create with value data.
        /// </summary>
        [TestMethod]
        public void CanCreateWithValueDataTest()
        {
            var name = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();

            var stream = Substitute.For<Stream>();

            using (var target = new PostFileStreamEntry(name, value, stream))
            {
                target.Name.Should().Be(name);
                target.Value.Should().Be(value);

                var actual = target.ReadContent();

                actual.Should().BeSameAs(stream);
            }
        }

        /// <summary>
        ///     Runs a test for dispose cleans up stream.
        /// </summary>
        [TestMethod]
        public void DisposeCleansUpStreamTest()
        {
            var name = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();

            var stream = Substitute.For<Stream>();

            using (new PostFileStreamEntry(name, value, stream))
            {
            }

            stream.Received().Dispose();
        }

        /// <summary>
        ///     Runs a test for dispose does not throw exception when stream dispose throws object disposed exception.
        /// </summary>
        [TestMethod]
        public void DisposeDoesNotThrowExceptionWhenStreamDisposeThrowsObjectDisposedExceptionTest()
        {
            var name = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();

            var stream = Substitute.For<Stream>();

            stream.When(x => x.Dispose()).Do(x => { throw new ObjectDisposedException("stream"); });

            using (new PostFileStreamEntry(name, value, stream))
            {
            }

            stream.Received().Dispose();
        }

        /// <summary>
        ///     Runs a test for dispose throw exception when stream dispose throws unexpected exception.
        /// </summary>
        [TestMethod]
        public void DisposeThrowExceptionWhenStreamDisposeThrowsUnexpectedExceptionTest()
        {
            var name = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();

            var stream = Substitute.For<Stream>();

            stream.When(x => x.Dispose()).Do(x => { throw new TimeoutException(); });

            var target = new PostFileStreamEntry(name, value, stream);

            Action action = target.Dispose;

            action.ShouldThrow<TimeoutException>();
        }

        /// <summary>
        ///     Runs a test for is valid returns false when value is empty.
        /// </summary>
        [TestMethod]
        public void IsValidReturnsFalseWhenValueIsEmptyTest()
        {
            var name = Guid.NewGuid().ToString();

            var stream = Substitute.For<Stream>();

            using (var target = new PostFileStreamEntry(name, string.Empty, stream))
            {
                target.IsValid.Should().BeFalse();
            }
        }

        /// <summary>
        ///     Runs a test for is valid returns false when value is null.
        /// </summary>
        [TestMethod]
        public void IsValidReturnsFalseWhenValueIsNullTest()
        {
            var name = Guid.NewGuid().ToString();

            var stream = Substitute.For<Stream>();

            using (var target = new PostFileStreamEntry(name, null, stream))
            {
                target.IsValid.Should().BeFalse();
            }
        }

        /// <summary>
        ///     Runs a test for is valid returns false when value is white space.
        /// </summary>
        [TestMethod]
        public void IsValidReturnsFalseWhenValueIsWhiteSpaceTest()
        {
            var name = Guid.NewGuid().ToString();

            var stream = Substitute.For<Stream>();

            using (var target = new PostFileStreamEntry(name, "  ", stream))
            {
                target.IsValid.Should().BeFalse();
            }
        }

        /// <summary>
        ///     Runs a test for is valid returns true when value provided.
        /// </summary>
        [TestMethod]
        public void IsValidReturnsTrueWhenValueProvidedTest()
        {
            var name = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();

            var stream = Substitute.For<Stream>();

            using (var target = new PostFileStreamEntry(name, value, stream))
            {
                target.IsValid.Should().BeTrue();
            }
        }

        /// <summary>
        ///     Runs a test for read content returns file data.
        /// </summary>
        [TestMethod]
        public void ReadContentReturnsFileDataTest()
        {
            var name = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();
            var expected = Guid.NewGuid().ToString();

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream, Encoding.UTF8, 1024, true))
                {
                    writer.Write(expected);
                }

                stream.Position = 0;

                using (var target = new PostFileStreamEntry(name, value, stream))
                {
                    using (var contentStream = target.ReadContent())
                    {
                        using (var reader = new StreamReader(contentStream))
                        {
                            var actual = reader.ReadToEnd();

                            actual.Should().Be(expected);
                        }
                    }

                    target.IsValid.Should().BeTrue();
                }
            }
        }

        /// <summary>
        ///     Runs a test for throws exception when created with empty name.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithEmptyNameTest()
        {
            var stream = Substitute.For<Stream>();

            Action action = () =>
            {
                using (new PostFileStreamEntry(string.Empty, Guid.NewGuid().ToString(), stream))
                {
                }
            };

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for throws exception when created with null name.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithNullNameTest()
        {
            var stream = Substitute.For<Stream>();

            Action action = () =>
            {
                using (new PostFileStreamEntry(null, Guid.NewGuid().ToString(), stream))
                {
                }
            };

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for throws exception when created with null stream.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithNullStreamTest()
        {
            Action action = () =>
            {
                using (new PostFileStreamEntry(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), null))
                {
                }
            };

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for throws exception when created with white space name.
        /// </summary>
        [TestMethod]
        public void ThrowsExceptionWhenCreatedWithWhiteSpaceNameTest()
        {
            var stream = Substitute.For<Stream>();

            Action action = () =>
            {
                using (new PostFileStreamEntry("  ", Guid.NewGuid().ToString(), stream))
                {
                }
            };

            action.ShouldThrow<ArgumentException>();
        }
    }
}