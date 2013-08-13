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
    public abstract class Page
    {
        /// <summary>
        ///     Stores the reference to the owning browser.
        /// </summary>
        private Browser _browser;

        /// <summary>
        ///     Stores the status code.
        /// </summary>
        private HttpStatusCode _statusCode;

        /// <summary>
        ///     Stores the status description.
        /// </summary>
        private string _statusDescription;

        /// <summary>
        /// Sets the browser.
        /// </summary>
        /// <param name="browser">
        /// The browser.
        /// </param>
        internal void SetBrowser(Browser browser)
        {
            _browser = browser;
        }

        /// <summary>
        /// Sets the content of the string.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        internal abstract void SetContent(HttpContent content);

        /// <summary>
        /// Sets the state.
        /// </summary>
        /// <param name="statusCode">
        /// The status code.
        /// </param>
        /// <param name="statusDescription">
        /// The status description.
        /// </param>
        internal void SetStatus(HttpStatusCode statusCode, string statusDescription)
        {
            _statusCode = statusCode;
            _statusDescription = statusDescription;
        }

        /// <summary>
        /// Determines whether the specified location is valid for the page.
        /// </summary>
        /// <param name="location">
        /// The current location.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified location is valid for the page; otherwise, <c>false</c>.
        /// </returns>
        protected internal virtual bool IsValidLocation(Uri location)
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
        ///     Gets the location of the page.
        /// </summary>
        /// <value>
        ///     The location of the page.
        /// </value>
        public abstract Uri Location
        {
            get;
        }

        /// <summary>
        ///     Gets the status code.
        /// </summary>
        /// <value>
        ///     The status code.
        /// </value>
        public HttpStatusCode StatusCode
        {
            [DebuggerStepThrough]
            get
            {
                return _statusCode;
            }
        }

        /// <summary>
        ///     Gets the status description.
        /// </summary>
        /// <value>
        ///     The status description.
        /// </value>
        public string StatusDescription
        {
            [DebuggerStepThrough]
            get
            {
                return _statusDescription;
            }
        }

        /// <summary>
        ///     Stores the reference to the owning browser.
        /// </summary>
        internal Browser Browser
        {
            [DebuggerStepThrough]
            get
            {
                return _browser;
            }
        }
    }
}