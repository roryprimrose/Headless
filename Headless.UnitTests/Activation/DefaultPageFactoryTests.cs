namespace Headless.UnitTests.Activation
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using FluentAssertions;
    using Headless.Activation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;

    /// <summary>
    ///     The <see cref="DefaultPageFactoryTests" />
    ///     class tests the <see cref="DefaultPageFactory" /> class.
    /// </summary>
    [TestClass]
    public class DefaultPageFactoryTests
    {
        /// <summary>
        ///     Runs a test for create initializes new page instance.
        /// </summary>
        [TestMethod]
        public void CreateInitializesNewPageInstanceTest()
        {
            var data = Guid.NewGuid().ToString();
            var response = new HttpResponseMessage
            {
                Content = new StringContent(data)
            };
            var result = new HttpResult(new ReadOnlyCollection<HttpOutcome>(new List<HttpOutcome>()));

            var browser = Substitute.For<IBrowser>();

            var target = new DefaultPageFactory();

            var actual = target.Create<PageWrapper>(browser, response, result);

            actual.Should().NotBeNull();
            actual.Browser.Should().Be(browser);
            actual.Result.Should().Be(result);
            actual.StatusCode.Should().Be(response.StatusCode);
            actual.StatusDescription.Should().Be(response.ReasonPhrase);
            actual.Content.Should().Be(data);
        }
    }
}