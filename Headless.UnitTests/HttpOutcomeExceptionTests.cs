namespace Headless.UnitTests
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="HttpOutcomeExceptionTests" />
    ///     class tests the <see cref="HttpOutcomeException" /> class.
    /// </summary>
    [TestClass]
    public class HttpOutcomeExceptionTests
    {
        /// <summary>
        ///     Runs a test for can be created with message and exception.
        /// </summary>
        [TestMethod]
        public void CanBeCreatedWithMessageAndExceptionTest()
        {
            var exceptionMessage = Guid.NewGuid().ToString();
            var innerExceptionMessage = Guid.NewGuid().ToString();

            var innerException = new TypeLoadException(innerExceptionMessage);
            var target = new HttpOutcomeException(exceptionMessage, innerException);

            target.Message.Should().Be(exceptionMessage);
            target.InnerException.Should().Be(innerException);
        }

        /// <summary>
        ///     Runs a test for can be serialized and deserialized.
        /// </summary>
        [TestMethod]
        public void CanBeSerializedAndDeserializedTest()
        {
            var exceptionMessage = Guid.NewGuid().ToString();
            var innerExceptionMessage = Guid.NewGuid().ToString();
            var innerException = new TypeLoadException(innerExceptionMessage);
            var target = new HttpOutcomeException(exceptionMessage, innerException);

            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();

                formatter.Serialize(ms, target);
                ms.Seek(0, SeekOrigin.Begin);

                var outputException = formatter.Deserialize(ms) as HttpOutcomeException;

                outputException.Should().NotBeNull();
                target.Message.Should().Be(exceptionMessage);
                target.InnerException.Should().Be(innerException);
            }
        }

        /// <summary>
        ///     Runs a test for message returns value when created with default constructor.
        /// </summary>
        [TestMethod]
        public void MessageReturnsValueWhenCreatedWithDefaultConstructorTest()
        {
            var target = new HttpOutcomeException();

            target.Message.Should().NotBeEmpty();
        }

        /// <summary>
        ///     Runs a test for message returns value when created with message.
        /// </summary>
        [TestMethod]
        public void MessageReturnsValueWhenCreatedWithMessageTest()
        {
            var exceptionMessage = Guid.NewGuid().ToString();

            var target = new HttpOutcomeException(exceptionMessage);

            target.Message.Should().Be(exceptionMessage);
        }
    }
}