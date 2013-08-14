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
        private Browser _browser;

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

            _browser = browser;
            _statusCode = response.StatusCode;
            _statusDescription = response.ReasonPhrase;
            _result = result;

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

            if (Location.IsBaseOf(location))
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
        internal abstract void SetContent(HttpContent content);

        /// <inheritdoc />
        public Browser Browser
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