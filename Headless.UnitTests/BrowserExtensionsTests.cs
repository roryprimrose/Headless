namespace Headless.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using FluentAssertions;
    using Headless.Activation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;

    /// <summary>
    ///     The <see cref="BrowserExtensionsTests" />
    ///     class tests the <see cref="BrowserExtensions" /> class.
    /// </summary>
    [TestClass]
    public class BrowserExtensionsTests
    {
        /// <summary>
        ///     Runs a test for go to static location navigates to specific location.
        /// </summary>
        [TestMethod]
        public void GoToStaticLocationNavigatesToSpecificLocationTest()
        {
            var location = new Uri("http://www.somwhere.com");
            var expected = new TextPageWrapper();

            var target = Substitute.For<IBrowser>();

            target.Execute<TextPageWrapper>(
                Arg.Is<HttpRequestMessage>(x => x.RequestUri == location && x.Method == HttpMethod.Get), 
                HttpStatusCode.OK, 
                Arg.Any<IPageFactory>()).Returns(expected);

            var actual = target.GoTo<TextPageWrapper>(location);

            actual.Should().BeSameAs(expected);
        }

        /// <summary>
        ///     Runs a test for go to static navigates to page location.
        /// </summary>
        [TestMethod]
        public void GoToStaticNavigatesToPageLocationTest()
        {
            var expected = new TextPageWrapper();

            var target = Substitute.For<IBrowser>();

            target.Execute<TextPageWrapper>(
                Arg.Is<HttpRequestMessage>(x => x.RequestUri == expected.TargetLocation && x.Method == HttpMethod.Get), 
                HttpStatusCode.OK, 
                Arg.Any<IPageFactory>()).Returns(expected);

            var actual = target.GoTo<TextPageWrapper>();

            actual.Should().BeSameAs(expected);
        }

        /// <summary>
        ///     Runs a test for go to static with status navigates to page location.
        /// </summary>
        [TestMethod]
        public void GoToStaticWithStatusNavigatesToPageLocationTest()
        {
            var expected = new TextPageWrapper();

            var target = Substitute.For<IBrowser>();

            target.Execute<TextPageWrapper>(
                Arg.Is<HttpRequestMessage>(x => x.RequestUri == expected.TargetLocation && x.Method == HttpMethod.Get), 
                HttpStatusCode.NotFound, 
                Arg.Any<IPageFactory>()).Returns(expected);

            var actual = target.GoTo<TextPageWrapper>(HttpStatusCode.NotFound);

            actual.Should().BeSameAs(expected);
        }

        /// <summary>
        ///     Runs a test for go to throws exception with null browser.
        /// </summary>
        [TestMethod]
        public void GoToThrowsExceptionWithNullBrowserTest()
        {
            IBrowser target = null;
            var location = new Uri("https://google.com");

            Action action = () => target.GoTo(location);

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for go to throws exception with null location.
        /// </summary>
        [TestMethod]
        public void GoToThrowsExceptionWithNullLocationTest()
        {
            var target = Substitute.For<IBrowser>();

            Action action = () => target.GoTo(null);

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for go to throws exception with null page factory.
        /// </summary>
        [TestMethod]
        public void GoToThrowsExceptionWithNullPageFactoryTest()
        {
            var location = new Uri("https://google.com");

            var target = Substitute.For<IBrowser>();

            Action action = () => target.GoTo<TextPageWrapper>(location, HttpStatusCode.OK, null);

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for go to static location navigates to specific location.
        /// </summary>
        [TestMethod]
        public void PostToStaticLocationNavigatesToSpecificLocationTest()
        {
            var location = new Uri("http://www.somwhere.com");
            var expected = new TextPageWrapper();
            IList<PostEntry> parameters = new List<PostEntry>();

            var target = Substitute.For<IBrowser>();

            target.Execute<TextPageWrapper>(
                Arg.Is<HttpRequestMessage>(x => x.RequestUri == location && x.Method == HttpMethod.Post), 
                HttpStatusCode.OK, 
                Arg.Any<IPageFactory>()).Returns(expected);

            var actual = target.PostTo<TextPageWrapper>(parameters, location);

            actual.Should().BeSameAs(expected);
        }

        /// <summary>
        ///     Runs a test for go to static navigates to page location.
        /// </summary>
        [TestMethod]
        public void PostToStaticNavigatesToPageLocationTest()
        {
            IList<PostEntry> parameters = new List<PostEntry>
            {
                new PostEntry("test", "value")
            };
            var expected = new TextPageWrapper();

            var target = Substitute.For<IBrowser>();

            target.Execute<TextPageWrapper>(
                Arg.Is<HttpRequestMessage>(x => x.RequestUri == expected.TargetLocation && x.Method == HttpMethod.Post), 
                HttpStatusCode.OK, 
                Arg.Any<IPageFactory>()).Returns(expected);

            var actual = target.PostTo<TextPageWrapper>(parameters);

            actual.Should().BeSameAs(expected);
        }

        /// <summary>
        ///     Runs a test for post to static with file entries navigates to page location.
        /// </summary>
        [TestMethod]
        public void PostToStaticWithFileEntriesNavigatesToPageLocationTest()
        {
            var tempFile = Path.GetTempFileName();

            try
            {
                File.WriteAllText(tempFile, Guid.NewGuid().ToString());

                IList<PostEntry> parameters = new List<PostEntry>
                {
                    new PostEntry("test", "value"), 
                    new PostFileEntry("files", tempFile), 
                    new PostFileStreamEntry("files", "dynamicFile.txt", new MemoryStream()),
                    new PostFileEntry("files", null)
                };
                var expected = new TextPageWrapper();

                var target = Substitute.For<IBrowser>();

                target.Execute<TextPageWrapper>(
                    Arg.Is<HttpRequestMessage>(x => x.RequestUri == expected.TargetLocation && x.Method == HttpMethod.Post), 
                    HttpStatusCode.OK, 
                    Arg.Any<IPageFactory>()).Returns(expected);

                var actual = target.PostTo<TextPageWrapper>(parameters);

                actual.Should().BeSameAs(expected);
            }
            finally
            {
                File.Delete(tempFile);
            }
        }

        /// <summary>
        ///     Runs a test for go to static with status navigates to page location.
        /// </summary>
        [TestMethod]
        public void PostToStaticWithStatusNavigatesToPageLocationTest()
        {
            IList<PostEntry> parameters = new List<PostEntry>();
            var expected = new TextPageWrapper();

            var target = Substitute.For<IBrowser>();

            target.Execute<TextPageWrapper>(
                Arg.Is<HttpRequestMessage>(x => x.RequestUri == expected.TargetLocation && x.Method == HttpMethod.Post), 
                HttpStatusCode.NotFound, 
                Arg.Any<IPageFactory>()).Returns(expected);

            var actual = target.PostTo<TextPageWrapper>(parameters, HttpStatusCode.NotFound);

            actual.Should().BeSameAs(expected);
        }

        /// <summary>
        ///     Runs a test for go to throws exception with null browser.
        /// </summary>
        [TestMethod]
        public void PostToThrowsExceptionWithNullBrowserTest()
        {
            IList<PostEntry> parameters = new List<PostEntry>();
            IBrowser target = null;

            var location = new Uri("https://google.com");

            Action action = () => target.PostTo(parameters, location);

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for go to throws exception with null location.
        /// </summary>
        [TestMethod]
        public void PostToThrowsExceptionWithNullLocationTest()
        {
            IList<PostEntry> parameters = new List<PostEntry>();

            var target = Substitute.For<IBrowser>();

            Action action = () => target.PostTo(parameters, null);

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for go to throws exception with null page factory.
        /// </summary>
        [TestMethod]
        public void PostToThrowsExceptionWithNullPageFactoryTest()
        {
            IList<PostEntry> parameters = new List<PostEntry>();
            var location = new Uri("https://google.com");

            var target = Substitute.For<IBrowser>();

            Action action = () => target.PostTo<TextPageWrapper>(parameters, location, HttpStatusCode.OK, null);

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for post to throws exception with null parameters.
        /// </summary>
        [TestMethod]
        public void PostToThrowsExceptionWithNullParametersTest()
        {
            var location = new Uri("https://google.com");

            var pageFactory = Substitute.For<IPageFactory>();
            var target = Substitute.For<IBrowser>();

            Action action = () => target.PostTo<TextPageWrapper>(null, location, HttpStatusCode.OK, pageFactory);

            action.ShouldThrow<ArgumentNullException>();
        }
    }
}