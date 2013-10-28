namespace Headless.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Net.Http;
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
        ///     Runs a test for can create with result and exception.
        /// </summary>
        [TestMethod]
        public void CanCreateWithResultAndExceptionTest()
        {
            var outcomes = new List<HttpOutcome>
            {
                new HttpOutcome(
                    new Uri("http://www.google.com"),
                    HttpMethod.Get,
                    HttpStatusCode.Redirect,
                    "Redirect to HTTPS",
                    TimeSpan.FromMilliseconds(50)),
                new HttpOutcome(
                    new Uri("https://google.com"),
                    HttpMethod.Get,
                    HttpStatusCode.OK,
                    "OK",
                    TimeSpan.FromMilliseconds(234))
            };
            var result = new HttpResult(outcomes);
            var ex = new TimeoutException();

            var target = new HttpOutcomeException(result, ex);

            Trace.WriteLine(target.Message);

            target.Message.Should().Contain("http://www.google.com");
            target.Message.Should().Contain("https://google.com");
            target.Message.Should().Contain(ex.Message);
        }

        /// <summary>
        ///     Runs a test for can create with results and expected status code.
        /// </summary>
        [TestMethod]
        public void CanCreateWithResultAndExpectedStatusCodeTest()
        {
            var outcomes = new List<HttpOutcome>
            {
                new HttpOutcome(
                    new Uri("http://www.google.com"),
                    HttpMethod.Get,
                    HttpStatusCode.Redirect,
                    "Redirect to HTTPS",
                    TimeSpan.FromMilliseconds(50)),
                new HttpOutcome(
                    new Uri("https://google.com"),
                    HttpMethod.Get,
                    HttpStatusCode.OK,
                    "OK",
                    TimeSpan.FromMilliseconds(234))
            };
            var result = new HttpResult(outcomes);

            var target = new HttpOutcomeException(result, HttpStatusCode.Forbidden);

            Trace.WriteLine(target.Message);

            target.Message.Should().Contain("http://www.google.com");
            target.Message.Should().Contain("https://google.com");
        }

        /// <summary>
        ///     Runs a test for can serialize and deserialize result with location.
        /// </summary>
        [TestMethod]
        public void CanSerializeAndDeserializeResultWithLocationTest()
        {
            var outcomes = new List<HttpOutcome>
            {
                new HttpOutcome(
                    new Uri("http://www.google.com"),
                    HttpMethod.Get,
                    HttpStatusCode.Redirect,
                    "Redirect to HTTPS",
                    TimeSpan.FromMilliseconds(50)),
                new HttpOutcome(
                    new Uri("https://google.com"),
                    HttpMethod.Get,
                    HttpStatusCode.OK,
                    "OK",
                    TimeSpan.FromMilliseconds(234))
            };
            var result = new HttpResult(outcomes);

            var target = new HttpOutcomeException(result, new Uri("https://www.google.com"));

            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();

                formatter.Serialize(ms, target);
                ms.Seek(0, SeekOrigin.Begin);

                var outputException = formatter.Deserialize(ms) as HttpOutcomeException;

                Trace.WriteLine(outputException.Message);

                outputException.Should().NotBeNull();
                target.Message.Should().Be(outputException.Message);
                target.Result.ShouldBeEquivalentTo(result);
            }
        }

        /// <summary>
        ///     Runs a test for can serialize and deserialize result with status code.
        /// </summary>
        [TestMethod]
        public void CanSerializeAndDeserializeResultWithStatusCodeTest()
        {
            var outcomes = new List<HttpOutcome>
            {
                new HttpOutcome(
                    new Uri("http://www.google.com"),
                    HttpMethod.Get,
                    HttpStatusCode.Redirect,
                    "Redirect to HTTPS",
                    TimeSpan.FromMilliseconds(50)),
                new HttpOutcome(
                    new Uri("https://google.com"),
                    HttpMethod.Get,
                    HttpStatusCode.OK,
                    "OK",
                    TimeSpan.FromMilliseconds(234))
            };
            var result = new HttpResult(outcomes);

            var target = new HttpOutcomeException(result, HttpStatusCode.Forbidden);

            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();

                formatter.Serialize(ms, target);
                ms.Seek(0, SeekOrigin.Begin);

                var outputException = formatter.Deserialize(ms) as HttpOutcomeException;

                Trace.WriteLine(outputException.Message);

                outputException.Should().NotBeNull();
                target.Message.Should().Be(outputException.Message);
                target.Result.ShouldBeEquivalentTo(result);
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