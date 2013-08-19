namespace Headless
{
    using System;
    using System.Diagnostics;
    using System.Dynamic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Xml.XPath;

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
        public void Initialize(IBrowser browser, HttpResponseMessage response, HttpResult result)
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

            result = FindElement(binder.Name);

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
            var finder = new HtmlElementFinder<HtmlElement>(this);

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
        
        /// <inheritdoc />
        public IBrowser Browser
        {
            [DebuggerStepThrough]
            get
            {
                return _wrapperPage.Browser;
            }
        }

        /// <inheritdoc />
        public IXPathNavigable Document
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
        public IXPathNavigable Node
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