namespace Headless
{
    using System;
    using System.Diagnostics;
    using System.Dynamic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using HtmlAgilityPack;

    /// <summary>
    ///     The <see cref="DynamicHtmlPage" />
    ///     provides the wrapper logic around a dynamic HTML page reference.
    /// </summary>
    public class DynamicHtmlPage : DynamicObject, IHtmlPage
    {
        /// <summary>
        ///     The wrapper page.
        /// </summary>
        private HtmlPageWrapper _wrapperPage;

        /// <inheritdoc />
        public void Initialize(Browser browser, HttpResponseMessage response)
        {
            var results = browser.GetLastResult<DynamicHtmlPage>();
            var location = results.Outcomes.Last().Location;

            _wrapperPage = new HtmlPageWrapper(location);

            _wrapperPage.Initialize(browser, response);
        }

        /// <inheritdoc />
        public bool IsValidLocation(Uri location)
        {
            return _wrapperPage.IsValidLocation(location);
        }

        /// <summary>
        ///     Provides a finding implementation for searching for child <see cref="HtmlElement" /> values.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="HtmlElement" /> to find.</typeparam>
        /// <returns>A <see cref="HtmlElementFinder{T}" /> value.</returns>
        private HtmlElementFinder<T> Find<T>() where T : HtmlElement
        {
            return new HtmlElementFinder<T>(_wrapperPage, Node);
        }

        /// <inheritdoc />
        public Browser Browser
        {
            [DebuggerStepThrough]
            get
            {
                return _wrapperPage.Browser;
            }
        }

        /// <inheritdoc />
        public HtmlDocument Document
        {
            [DebuggerStepThrough]
            get
            {
                return _wrapperPage.Document;
            }
        }

        /// <inheritdoc />
        public Uri Location
        {
            [DebuggerStepThrough]
            get
            {
                return _wrapperPage.Location;
            }
        }

        /// <inheritdoc />
        public HtmlNode Node
        {
            [DebuggerStepThrough]
            get
            {
                return _wrapperPage.Node;
            }
        }

        /// <inheritdoc />
        public HttpStatusCode StatusCode
        {
            [DebuggerStepThrough]
            get
            {
                return _wrapperPage.StatusCode;
            }
        }

        /// <inheritdoc />
        public string StatusDescription
        {
            [DebuggerStepThrough]
            get
            {
                return _wrapperPage.StatusDescription;
            }
        }
    }
}