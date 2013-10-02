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
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", 
        Justification = "This type is created dynamically.")]
    internal sealed class DynamicResolverPage : IPage
    {
        /// <summary>
        ///     The default media type used when the server does not send the media type header.
        /// </summary>
        private const string DefaultMediaType = "text/html";

        /// <summary>
        ///     The resolved page.
        /// </summary>
        private IPage _resolvedPage;

        /// <inheritdoc />
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="browser"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="response"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="result"/> parameter is <c>null</c>.
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

            // Look at the content type header to determine the correct type of page to return
            var mediaType = DetermineMediaType(response);

            var contentType = browser.ContentTypeResolver.DeterminePageType(mediaType);

            if (contentType == PageContentType.Html)
            {
                _resolvedPage = new DynamicHtmlPage();
            }
            else if (contentType == PageContentType.Binary)
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
        public bool IsOn(Uri location, UriComponents compareWith)
        {
            return ResolvedPage.IsOn(location, compareWith);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.BuildToString();
        }

        /// <summary>
        /// Determines the type of the media.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>A <see cref="string"/> value.</returns>
        private static string DetermineMediaType(HttpResponseMessage response)
        {
            if (response.Content == null)
            {
                return DefaultMediaType;
            }

            if (response.Content.Headers == null)
            {
                return DefaultMediaType;
            }

            if (response.Content.Headers.ContentType == null)
            {
                return DefaultMediaType;
            }

            if (string.IsNullOrWhiteSpace(response.Content.Headers.ContentType.MediaType))
            {
                return DefaultMediaType;
            }

            return response.Content.Headers.ContentType.MediaType;
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

        /// <inheritdoc />
        public Uri TargetLocation
        {
            [DebuggerStepThrough]
            get
            {
                return ResolvedPage.TargetLocation;
            }
        }
    }
}