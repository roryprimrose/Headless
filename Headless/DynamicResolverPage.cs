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
        private Browser _browser;

        /// <summary>
        ///     The response.
        /// </summary>
        private HttpResponseMessage _response;

        /// <summary>
        ///     The result.
        /// </summary>
        private HttpResult _result;

        /// <summary>
        ///     Gets the appropriate page.
        /// </summary>
        /// <returns>An <see cref="IPage" /> value.</returns>
        public IPage GetAppropriatePage()
        {
            // Look at the content type header to determine the correct type of page to return
            var contentType = DetermineContentType();
            IPage page;

            if (contentType == ContentType.Html)
            {
                page = new DynamicHtmlPage();
            }
            else if (contentType == ContentType.Binary)
            {
                page = new BinaryPageWrapper(Location);
            }
            else
            {
                page = new TextPageWrapper(Location);
            }

            page.Initialize(_browser, _response, _result);

            return page;
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

            _browser = browser;
            _response = response;
            _result = result;
        }

        /// <inheritdoc />
        public bool IsOn(Uri location)
        {
            return true;
        }

        /// <summary>
        ///     Determines the type of the content.
        /// </summary>
        /// <returns>A <see cref="ContentType" /> value.</returns>
        private ContentType DetermineContentType()
        {
            var mediaType = _response.Content.Headers.ContentType.MediaType;

            if (mediaType.Equals("text/html", StringComparison.OrdinalIgnoreCase))
            {
                return ContentType.Html;
            }

            if (mediaType.Equals("text/xml", StringComparison.OrdinalIgnoreCase))
            {
                return ContentType.Html;
            }

            // TODO: Find out more binary content types here
            if (mediaType.IndexOf("image", StringComparison.InvariantCultureIgnoreCase) > -1)
            {
                return ContentType.Binary;
            }

            return ContentType.Text;
        }

        /// <inheritdoc />
        public Browser Browser
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

        /// <summary>
        ///     The <see cref="ContentType" />
        ///     enum identifies broad HTTP content types.
        /// </summary>
        private enum ContentType
        {
            /// <summary>
            ///     The document contains HTML/XML formatted text.
            /// </summary>
            Html = 0, 

            /// <summary>
            ///     The document contains text.
            /// </summary>
            Text, 

            /// <summary>
            ///     The document contains binary data.
            /// </summary>
            Binary
        }
    }
}