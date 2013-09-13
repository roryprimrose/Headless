namespace Headless
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Http;
    using Headless.Properties;

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
            return IsOn(location, _browser.VerificationParts);
        }

        /// <inheritdoc />
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="location" /> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        ///     The <paramref name="location" /> parameter is a relative location.
        /// </exception>
        public virtual bool IsOn(Uri location, UriComponents compareWith)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location");
            }

            if (location.IsAbsoluteUri == false)
            {
                throw new ArgumentException(Resources.Uri_LocationMustBeAbsolute, "location");
            }

            const UriFormat CompareFormat = UriFormat.SafeUnescaped;

            var compareValue = string.Compare(
                Location.GetComponents(compareWith, CompareFormat), 
                location.GetComponents(compareWith, CompareFormat), 
                StringComparison.OrdinalIgnoreCase);

            if (compareValue == 0)
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
        public Uri Location
        {
            get
            {
                return _result.Outcomes[_result.Outcomes.Count - 1].Location;
            }
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

        /// <inheritdoc />
        public abstract Uri TargetLocation
        {
            get;
        }
    }
}