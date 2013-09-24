namespace Headless.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Xml;
    using System.Xml.XPath;
    using Headless.Activation;

    /// <summary>
    ///     The <see cref="MemoryHtmlPage" />
    ///     class is used to test <see cref="HtmlPage" />
    ///     using string HTML as the source data.
    /// </summary>
    internal class MemoryHtmlPage : DynamicHtmlPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryHtmlPage"/> class.
        /// </summary>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="html">
        /// The HTML.
        /// </param>
        public MemoryHtmlPage(IBrowser browser, string html)
        {
            IEnumerable<HttpOutcome> outcomes = new List<HttpOutcome>
            {
                new HttpOutcome(
                    Config.BaseWebAddress, 
                    HttpMethod.Get, 
                    HttpStatusCode.OK, 
                    "OK", 
                    TimeSpan.FromMilliseconds(5))
            };
            var result = new HttpResult(outcomes);

            var document = new XmlDocument();

            document.LoadXml(html);

            var wrapper = new PageWrapper(browser, HttpStatusCode.OK, "OK", result, document);

            Initialize(wrapper);
        }

        /// <summary>
        ///     The <see cref="PageWrapper" />
        ///     class is used to provide an easy way to initialize a page object from an instance of this class.
        /// </summary>
        private class PageWrapper : IHtmlPage
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="PageWrapper"/> class.
            /// </summary>
            /// <param name="browser">
            /// The browser.
            /// </param>
            /// <param name="statusCode">
            /// The status code.
            /// </param>
            /// <param name="statusDescription">
            /// The status description.
            /// </param>
            /// <param name="result">
            /// The result.
            /// </param>
            /// <param name="document">
            /// The document.
            /// </param>
            public PageWrapper(
                IBrowser browser, 
                HttpStatusCode statusCode, 
                string statusDescription, 
                HttpResult result, 
                IXPathNavigable document)
            {
                Browser = browser;
                StatusCode = statusCode;
                StatusDescription = statusDescription;
                Result = result;
                Document = document;
            }

            /// <inheritdoc />
            public T CloneAs<T>() where T : IHtmlPage, new()
            {
                throw new NotImplementedException();
            }

            /// <inheritdoc />
            public IHtmlElementFinder<T> Find<T>() where T : HtmlElement
            {
                throw new NotImplementedException();
            }

            /// <inheritdoc />
            public void Initialize(IBrowser browser, HttpResponseMessage response, HttpResult result)
            {
                throw new NotImplementedException();
            }

            /// <inheritdoc />
            public void Initialize(IHtmlPage page)
            {
                throw new NotImplementedException();
            }

            /// <inheritdoc />
            public bool IsOn(Uri location, UriComponents compareWith)
            {
                throw new NotImplementedException();
            }

            /// <inheritdoc />
            public IBrowser Browser
            {
                get;
                private set;
            }

            /// <inheritdoc />
            public IXPathNavigable Document
            {
                get;
                private set;
            }

            /// <inheritdoc />
            public IHtmlElementFactory ElementFactory
            {
                get;
                private set;
            }

            /// <summary>
            /// Gets the html.
            /// </summary>
            public string Html
            {
                get;
                private set;
            }

            /// <inheritdoc />
            public Uri Location
            {
                get;
                private set;
            }

            /// <inheritdoc />
            public IXPathNavigable Node
            {
                get;
                private set;
            }

            /// <inheritdoc />
            public HttpResult Result
            {
                get;
                private set;
            }

            /// <inheritdoc />
            public HttpStatusCode StatusCode
            {
                get;
                private set;
            }

            /// <inheritdoc />
            public string StatusDescription
            {
                get;
                private set;
            }

            /// <inheritdoc />
            public Uri TargetLocation
            {
                get;
                private set;
            }
        }
    }
}