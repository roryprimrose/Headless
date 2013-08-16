namespace Headless
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net;
    using System.Net.Http;

    /// <summary>
    ///     The <see cref="DynamicResolverPage" />
    ///     class is used to determine the correct page to return for a dynamic request.
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "This type is created dynamically.")]
    internal class DynamicResolverPage : IPage
    {
        /// <summary>
        ///     The browser.
        /// </summary>
        private IBrowser _browser;

        /// <summary>
        ///     The response.
        /// </summary>
        private HttpResponseMessage _response;

        /// <summary>
        ///     The result.
        /// </summary>
        private HttpResult _result;

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

            _browser = browser;
            _response = response;
            _result = result;
        }

        /// <inheritdoc />
        public bool IsOn(Uri location)
        {
            return true;
        }

        /// <inheritdoc />
        public IBrowser Browser
        {
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
                return _result.Outcomes.Last().Location;
            }
        }

        /// <inheritdoc />
        public HttpResult Result
        {
            get
            {
                return _result;
            }
        }

        /// <inheritdoc />
        public HttpStatusCode StatusCode
        {
            get
            {
                return _response.StatusCode;
            }
        }

        /// <inheritdoc />
        public string StatusDescription
        {
            get
            {
                return _response.ReasonPhrase;
            }
        }
    }
}