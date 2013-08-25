namespace Headless.UnitTests
{
    using System;
    using System.Collections.Generic;
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
            var expected = new PageWrapper();

            var target = Substitute.For<IBrowser>();

            target.Execute<PageWrapper>(
                Arg.Is<HttpRequestMessage>(x => x.RequestUri == location && x.Method == HttpMethod.Get), 
                HttpStatusCode.OK, 
                Arg.Any<IPageFactory>()).Returns(expected);

            var actual = target.GoTo<PageWrapper>(location);

            actual.Should().BeSameAs(expected);
        }

        /// <summary>
        ///     Runs a test for go to static navigates to page location.
        /// </summary>
        [TestMethod]
        public void GoToStaticNavigatesToPageLocationTest()
        {
            var expected = new PageWrapper();

            var target = Substitute.For<IBrowser>();

            target.Execute<PageWrapper>(
                Arg.Is<HttpRequestMessage>(x => x.RequestUri == expected.Location && x.Method == HttpMethod.Get), 
                HttpStatusCode.OK, 
                Arg.Any<IPageFactory>()).Returns(expected);

            var actual = target.GoTo<PageWrapper>();

            actual.Should().BeSameAs(expected);
        }

        /// <summary>
        ///     Runs a test for go to static with status navigates to page location.
        /// </summary>
        [TestMethod]
        public void GoToStaticWithStatusNavigatesToPageLocationTest()
        {
            var expected = new PageWrapper();

            var target = Substitute.For<IBrowser>();

            target.Execute<PageWrapper>(
                Arg.Is<HttpRequestMessage>(x => x.RequestUri == expected.Location && x.Method == HttpMethod.Get), 
                HttpStatusCode.NotFound, 
                Arg.Any<IPageFactory>()).Returns(expected);

            var actual = target.GoTo<PageWrapper>(HttpStatusCode.NotFound);

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

            Action action = () => target.GoTo<PageWrapper>(location, HttpStatusCode.OK, null);

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for go to static location navigates to specific location.
        /// </summary>
        [TestMethod]
        public void PostToStaticLocationNavigatesToSpecificLocationTest()
        {
            var location = new Uri("http://www.somwhere.com");
            var expected = new PageWrapper();
            IList<PostEntry> parameters = new List<PostEntry>();

            var target = Substitute.For<IBrowser>();

            target.Execute<PageWrapper>(
                Arg.Is<HttpRequestMessage>(x => x.RequestUri == location && x.Method == HttpMethod.Post), 
                HttpStatusCode.OK, 
                Arg.Any<IPageFactory>()).Returns(expected);

            var actual = target.PostTo<PageWrapper>(parameters, location);

            actual.Should().BeSameAs(expected);
        }

        /// <summary>
        ///     Runs a test for go to static navigates to page location.
        /// </summary>
        [TestMethod]
        public void PostToStaticNavigatesToPageLocationTest()
        {
            IList<PostEntry> parameters = new List<PostEntry>();
            var expected = new PageWrapper();

            var target = Substitute.For<IBrowser>();

            target.Execute<PageWrapper>(
                Arg.Is<HttpRequestMessage>(x => x.RequestUri == expected.Location && x.Method == HttpMethod.Post), 
                HttpStatusCode.OK, 
                Arg.Any<IPageFactory>()).Returns(expected);

            var actual = target.PostTo<PageWrapper>(parameters);

            actual.Should().BeSameAs(expected);
        }

        /// <summary>
        ///     Runs a test for go to static with status navigates to page location.
        /// </summary>
        [TestMethod]
        public void PostToStaticWithStatusNavigatesToPageLocationTest()
        {
            IList<PostEntry> parameters = new List<PostEntry>();
            var expected = new PageWrapper();

            var target = Substitute.For<IBrowser>();

            target.Execute<PageWrapper>(
                Arg.Is<HttpRequestMessage>(x => x.RequestUri == expected.Location && x.Method == HttpMethod.Post), 
                HttpStatusCode.NotFound, 
                Arg.Any<IPageFactory>()).Returns(expected);

            var actual = target.PostTo<PageWrapper>(parameters, HttpStatusCode.NotFound);

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

            Action action = () => target.PostTo<PageWrapper>(parameters, location, HttpStatusCode.OK, null);

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

            Action action = () => target.PostTo<PageWrapper>(null, location, HttpStatusCode.OK, pageFactory);

            action.ShouldThrow<ArgumentNullException>();
        }
    }
}