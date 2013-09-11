namespace Headless
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Http;

    /// <summary>
    ///     The <see cref="Page" />
    ///     class provides the base page logic for interacting with a browser response.
    /// </summary>
    public abstract class Page : IPage
    {
        /// <summary>
        ///     Stores the reference to the owning browser.
        /// </summary>
        private IBrowser _browser;

        /// <summary>
        ///     The HTTP result.
        /// </summary>
        private HttpResult _result;

        /// <summary>
        ///     Stores the status code.
        /// </summary>
        private HttpStatusCode _statusCode;

        /// <summary>
        ///     Stores the status description.
        /// </summary>
        private string _statusDescription;

        /// <inheritdoc />
        public virtual void Initialize(IBrowser browser, HttpResponseMessage response, HttpResult result)
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

            Initialize(browser, response.StatusCode, response.ReasonPhrase, result);

            SetContent(response.Content);
        }

        /// <inheritdoc />
        public virtual bool IsOn(Uri location)
        {
            if (location == null)
            {
                return false;
            }

            if (string.Equals(location.ToString(), Location.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            // Make the addresses lower-case because Uri.IsBaseOf is case sensitive
            var pageLocation = new Uri(Location.ToString().ToUpperInvariant());
            var testLocation = new Uri(location.ToString().ToUpperInvariant());

            if (pageLocation.IsBaseOf(testLocation))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the content of the string.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        protected internal abstract void SetContent(HttpContent content);

        /// <summary>
        /// Initializes the page with the specified values.
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
        protected virtual void Initialize(
            IBrowser browser, 
            HttpStatusCode statusCode, 
            string statusDescription, 
            HttpResult result)
        {
            _browser = browser;
            _statusCode = statusCode;
            _statusDescription = statusDescription;
            _result = result;
        }

        /// <inheritdoc />
        public IBrowser Browser
        {
            [DebuggerStepThrough]
            get
            {
                return _browser;
            }
        }

        /// <inheritdoc />
        public abstract Uri Location
        {
            get;
        }

        /// <inheritdoc />
        public HttpResult Result
        {
            [DebuggerStepThrough]
            get
            {
                return _result;
            }
        }

        /// <inheritdoc />
        public HttpStatusCode StatusCode
        {
            [DebuggerStepThrough]
            get
            {
                return _statusCode;
            }
        }

        /// <inheritdoc />
        public string StatusDescription
        {
            [DebuggerStepThrough]
            get
            {
                return _statusDescription;
            }
        }
    }
}