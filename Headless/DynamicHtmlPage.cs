namespace Headless
{
    using System;
    using System.Diagnostics;
    using System.Dynamic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
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

        /// <summary>
        ///     Initializes a new instance of the <see cref="DynamicHtmlPage" /> class.
        /// </summary>
        public DynamicHtmlPage()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicHtmlPage"/> class.
        /// </summary>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="html">
        /// The HTML.
        /// </param>
        /// <remarks>
        /// This constructor supports the usage of a dynamic HTML page generated from in-memory HTML rather than resulting
        ///     from a browser request.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="browser"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <paramref name="html"/> parameter is <c>null</c>, empty or only contains white
        ///     space.
        /// </exception>
        public DynamicHtmlPage(IBrowser browser, string html)
        {
            if (browser == null)
            {
                throw new ArgumentNullException("browser");
            }

            if (string.IsNullOrWhiteSpace(html))
            {
                throw new ArgumentException(Resources.HtmlPage_NoHtmlContentProvided, "html");
            }

            // Create a wrapper page using the MemoryHtmlPage
            var wrapperPage = new MemoryHtmlPage(browser, html);

            Initialize(wrapperPage);
        }

        /// <inheritdoc />
        public T CloneAs<T>() where T : IHtmlPage, new()
        {
            var page = new T();

            page.Initialize(_wrapperPage);

            return page;
        }

        /// <inheritdoc />
        public IHtmlElementFinder<T> Find<T>() where T : HtmlElement
        {
            return new DefaultHtmlElementFinder<T>(this);
        }

        /// <inheritdoc />
        public virtual IHtmlElementFinder<T> FindAncestor<T>() where T : HtmlElement
        {
            return new AncestorHtmlElementFinder<T>(this);
        }

        /// <inheritdoc />
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="page" /> parameter is <c>null</c>.
        /// </exception>
        public void Initialize(IHtmlPage page)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page");
            }

            var location = page.Result.Outcomes.Last().Location;

            _wrapperPage = new HtmlPageWrapper(location);

            _wrapperPage.Initialize(page);
        }

        /// <inheritdoc />
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="browser" /> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="response" /> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="result" /> parameter is <c>null</c>.
        /// </exception>
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

        /// <summary>
        /// Determines whether the the page is on the specified location.
        /// </summary>
        /// <param name="location">
        /// The current location.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified location is valid for the page; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="location"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// The <paramref name="location"/> parameter is a relative location.
        /// </exception>
        public bool IsOn(Uri location)
        {
            return IsOn(location, _wrapperPage.Browser.VerificationParts);
        }

        /// <inheritdoc />
        public bool IsOn(Uri location, UriComponents compareWith)
        {
            return _wrapperPage.IsOn(location, compareWith);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.BuildToString();
        }

        /// <inheritdoc />
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="binder" /> parameter is <c>null</c>.
        /// </exception>
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
        public IHtmlElementFactory ElementFactory
        {
            get
            {
                return _wrapperPage.ElementFactory;
            }
        }

        /// <inheritdoc />
        public string Html
        {
            get
            {
                return _wrapperPage.Html;
            }
        }

        /// <inheritdoc />
        public Uri Location
        {
            get
            {
                return _wrapperPage.Location;
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

        /// <inheritdoc />
        public Uri TargetLocation
        {
            [DebuggerStepThrough]
            get
            {
                return _wrapperPage.TargetLocation;
            }
        }

        /// <summary>
        ///     Gets the HTML document of the page.
        /// </summary>
        /// <value>
        ///     The HTML document of the page.
        /// </value>
        protected internal IXPathNavigable Document
        {
            [DebuggerStepThrough]
            get
            {
                return ((IHtmlPage)this).Document;
            }
        }

        /// <summary>
        ///     Gets the node.
        /// </summary>
        /// <value>
        ///     The node.
        /// </value>
        protected internal IXPathNavigable Node
        {
            [DebuggerStepThrough]
            get
            {
                return ((IHtmlPage)this).Node;
            }
        }

        /// <inheritdoc />
        IXPathNavigable IHtmlPage.Document
        {
            [DebuggerStepThrough]
            get
            {
                return ((IHtmlPage)_wrapperPage).Document;
            }
        }

        /// <inheritdoc />
        IXPathNavigable IHtmlPage.Node
        {
            [DebuggerStepThrough]
            get
            {
                return ((IHtmlPage)_wrapperPage).Node;
            }
        }
    }
}