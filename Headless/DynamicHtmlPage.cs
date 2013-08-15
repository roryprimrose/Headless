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
        public HtmlElementFinder<T> Find<T>() where T : HtmlElement
        {
            return new HtmlElementFinder<T>(this);
        }

        /// <inheritdoc />
        public void Initialize(Browser browser, HttpResponseMessage response, HttpResult result)
        {
            if (browser == null)
            {
                throw new ArgumentNullException("browser");
            }

            if (response == null)
            {
                throw new ArgumentNullException("response");
            }

            if (result == null)
            {
                throw new ArgumentNullException("result");
            }

            var location = result.Outcomes.Last().Location;

            _wrapperPage = new HtmlPageWrapper(location);

            _wrapperPage.Initialize(browser, response, result);
        }

        /// <inheritdoc />
        public bool IsOn(Uri location)
        {
            // There is no verification of dynamic page locations because there is no model to define where the current location should be
            return true;
        }

        /// <inheritdoc />
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (binder == null)
            {
                throw new ArgumentNullException("binder");
            }

            result = ResolveConcreteElement(binder.Name);

            if (result == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Finds the element.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// A <see cref="HtmlElement"/> value.
        /// </returns>
        /// <exception cref="System.NotImplementedException">
        /// Need to support radio buttons here that use the same name
        /// </exception>
        /// <exception cref="Headless.HtmlElementNotFoundException">
        /// No html element was found by id, name or text for the value ' +
        ///     value + '.
        /// </exception>
        private HtmlElement FindElement(string value)
        {
            var finder = new HtmlElementFinder<DynamicHtmlElement>(this);

            var elementsById = finder.ByAttribute("id", value).ToList();

            if (elementsById.Count == 1)
            {
                return elementsById[0];
            }

            var elementsByName = finder.ByAttribute("name", value).ToList();

            if (elementsByName.Count == 1)
            {
                return elementsByName[0];
            }

            if (elementsByName.Count > 1)
            {
                throw new NotImplementedException("Need to support radio buttons here that use the same name");
            }

            var elementsByText = finder.ByText(value).ToList();

            if (elementsByText.Count == 1)
            {
                return elementsByText[0];
            }

            return null;
        }

        /// <summary>
        /// Resolves the concrete element.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// A <see cref="HtmlElement"/> value.
        /// </returns>
        private HtmlElement ResolveConcreteElement(string value)
        {
            var element = FindElement(value);

            if (element == null)
            {
                return null;
            }

            // Convert this dynamic element into the correct HtmlElement
            // TODO: Figure out a cleaner way of doing this that will support multiple types per tag name (for input tags with different type attributes)
            if (element.TagName == "a")
            {
                return new HtmlLink(element.Page, element.Node);
            }

            return null;
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
        public HttpResult Result
        {
            [DebuggerStepThrough]
            get
            {
                return _wrapperPage.Result;
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