namespace Headless
{
    using System;
    using System.Diagnostics;
    using System.Dynamic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Xml;
    using System.Xml.XPath;
    using Headless.Activation;
    using Headless.Properties;

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
        public T CloneAs<T>() where T : IHtmlPage, new()
        {
            var page = new T();

            page.Initialize(
                _wrapperPage.Browser, 
                _wrapperPage.StatusCode, 
                _wrapperPage.StatusDescription, 
                _wrapperPage.Result, 
                _wrapperPage.Document as XmlDocument);

            return page;
        }

        /// <inheritdoc />
        public IHtmlElementFinder<T> Find<T>() where T : HtmlElement
        {
            return new DefaultHtmlElementFinder<T>(this);
        }

        /// <inheritdoc />
        public void Initialize(
            IBrowser browser, 
            HttpStatusCode statusCode, 
            string statusDescription, 
            HttpResult result, 
            XmlDocument document)
        {
            if (result == null)
            {
                throw new ArgumentNullException("result");
            }

            var location = result.Outcomes.Last().Location;

            _wrapperPage = new HtmlPageWrapper(location);

            _wrapperPage.Initialize(browser, statusCode, statusDescription, result, document);
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
                var failureMessage = string.Format(
                    CultureInfo.CurrentCulture, 
                    Resources.DynamicHtmlPage_DynamicElementNotFound, 
                    binder.Name);

                throw new HtmlElementNotFoundException(failureMessage);
            }

            return true;
        }

        /// <inheritdoc />
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            throw new InvalidOperationException(Resources.DynamicHtmlPage_MembersAreReadOnly);
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
            var finder = new DefaultHtmlElementFinder<HtmlElement>(this);

            var elementsById = finder.AllByAttribute("id", value).ToList();

            if (elementsById.Count == 1)
            {
                return elementsById[0];
            }

            if (elementsById.Count > 1)
            {
                var failureMessage = string.Format(
                    CultureInfo.CurrentCulture, 
                    Resources.HtmlElement_MultipleMatchesFoundForId, 
                    value);

                throw new InvalidHtmlElementMatchException(failureMessage);
            }

            var elementsByName = finder.AllByAttribute("name", value).ToList();

            if (elementsByName.Count == 1)
            {
                return elementsByName[0];
            }

            if (elementsByName.Count > 1)
            {
                var failureMessage = string.Format(
                    CultureInfo.CurrentCulture, 
                    Resources.HtmlElement_MultipleMatchesFoundForName, 
                    value);

                throw new InvalidHtmlElementMatchException(failureMessage);
            }

            var elementsBySensitiveText = finder.AllByText(value, false).ToList();

            if (elementsBySensitiveText.Count == 1)
            {
                return elementsBySensitiveText[0];
            }

            if (elementsBySensitiveText.Count > 1)
            {
                var failureMessage = string.Format(
                    CultureInfo.CurrentCulture, 
                    Resources.HtmlElement_MultipleMatchesFoundForText, 
                    value);

                throw new InvalidHtmlElementMatchException(failureMessage);
            }

            // The reason for running this as two seperate operations is that an insensitive search
            // may find multiple matches where the sensitive search would have returned the single expect element
            // This additional operation is just trying to be that little bit more helpful where the case sensitive match wasn't found
            var elementsByInsensitiveText = finder.AllByText(value, true).ToList();

            if (elementsByInsensitiveText.Count == 1)
            {
                return elementsByInsensitiveText[0];
            }

            if (elementsByInsensitiveText.Count > 1)
            {
                var failureMessage = string.Format(
                    CultureInfo.CurrentCulture, 
                    Resources.HtmlElement_MultipleMatchesFoundForText, 
                    value);

                throw new InvalidHtmlElementMatchException(failureMessage);
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
        public IHtmlElementFactory ElementFactory
        {
            get
            {
                return _wrapperPage.ElementFactory;
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