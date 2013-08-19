namespace Headless
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using System.Net.Http;
    using Headless.Activation;

    /// <summary>
    ///     The <see cref="DynamicResolverPage" />
    ///     class is used internally by <see cref="DefaultPageFactory" />
    ///     to create a page for dynamic requests where the actual type of page is determined by the response rather than
    ///     initial request.
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "This type is created dynamically.")]
    internal class DynamicResolverPage : IPage
    {
        /// <summary>
        ///     The resolved page.
        /// </summary>
        private IPage _resolvedPage;

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

            // Look at the content type header to determine the correct type of page to return
            var contentType = DetermineContentType(response);

            if (contentType == ContentType.Html)
            {
                _resolvedPage = new DynamicHtmlPage();
            }
            else if (contentType == ContentType.Binary)
            {
                _resolvedPage = new BinaryPageWrapper();
            }
            else
            {
                _resolvedPage = new TextPageWrapper();
            }

            ResolvedPage.Initialize(browser, response, result);
        }

        /// <inheritdoc />
        public bool IsOn(Uri location)
        {
            // There is no verification of dynamic page locations because there is no model to define where the current location should be
            return true;
        }

        /// <summary>
        /// Determines the type of the content.
        /// </summary>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <returns>
        /// A <see cref="ContentType"/> value.
        /// </returns>
        private static ContentType DetermineContentType(HttpResponseMessage response)
        {
            var mediaType = response.Content.Headers.ContentType.MediaType;

            if (mediaType.Equals("text/html", StringComparison.OrdinalIgnoreCase))
            {
                return ContentType.Html;
            }

            if (mediaType.Equals("text/xml", StringComparison.OrdinalIgnoreCase))
            {
                return ContentType.Html;
            }

            // TODO: Find out more binary content types here
            if (mediaType.IndexOf("image", StringComparison.OrdinalIgnoreCase) > -1)
            {
                return ContentType.Binary;
            }

            return ContentType.Text;
        }

        /// <inheritdoc />
        public IBrowser Browser
        {
            [DebuggerStepThrough]
            get
            {
                return ResolvedPage.Browser;
            }
        }

        /// <inheritdoc />
        public Uri Location
        {
            [DebuggerStepThrough]
            get
            {
                return ResolvedPage.Location;
            }
        }

        /// <summary>
        ///     Gets the resolved page.
        /// </summary>
        /// <value>
        ///     The resolved page.
        /// </value>
        public IPage ResolvedPage
        {
            get
            {
                return _resolvedPage;
            }
        }

        /// <inheritdoc />
        public HttpResult Result
        {
            [DebuggerStepThrough]
            get
            {
                return ResolvedPage.Result;
            }
        }

        /// <inheritdoc />
        public HttpStatusCode StatusCode
        {
            [DebuggerStepThrough]
            get
            {
                return ResolvedPage.StatusCode;
            }
        }

        /// <inheritdoc />
        public string StatusDescription
        {
            [DebuggerStepThrough]
            get
            {
                return ResolvedPage.StatusDescription;
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