namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Net;
    using System.Net.Http;
    using Headless.Activation;
    using Headless.Properties;

    /// <summary>
    ///     The <see cref="Browser" />
    ///     class provides a wrapper around a HTTP browsing session.
    /// </summary>
    public class Browser : IDisposable, IBrowser
    {
        /// <summary>
        ///     Stores the http client.
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        ///     Stores the http handler.
        /// </summary>
        private readonly HttpClientHandler _handler;

        /// <summary>
        ///     Stores the content type resolver.
        /// </summary>
        private IPageContentTypeResolver _contentTypeResolver;

        /// <summary>
        ///     Stores whether this instance has been disposed.
        /// </summary>
        private bool _disposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Browser" /> class.
        /// </summary>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", 
            Justification = "The handler is disposed by the client when it is disposed.")]
        public Browser()
        {
            _handler = new HttpClientHandler
            {
                AllowAutoRedirect = false
            };
            _client = new HttpClient(_handler);
            _contentTypeResolver = new DefaultPageContentTypeResolver();
        }

        /// <summary>
        ///     Clears the cookies.
        /// </summary>
        public void ClearCookies()
        {
            _handler.CookieContainer = new CookieContainer();
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        public T Execute<T>(HttpRequestMessage request, HttpStatusCode expectedStatusCode, IPageFactory pageFactory)
            where T : IPage, new()
        {
            var page = default(T);

            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (pageFactory == null)
            {
                throw new ArgumentNullException("pageFactory");
            }

            var currentResourceLocation = request.RequestUri;

            var outcomes = new List<HttpOutcome>();

            var stopwatch = Stopwatch.StartNew();

            var task = _client.SendAsync(request);

            Uri redirectLocation = null;
            bool requiresRedirect;

            using (var response = task.Result)
            {
                stopwatch.Stop();

                var outcome = new HttpOutcome(
                    currentResourceLocation, 
                    response.RequestMessage.Method, 
                    response.StatusCode, 
                    response.ReasonPhrase, 
                    stopwatch.Elapsed);

                outcomes.Add(outcome);

                requiresRedirect = IsRedirect(response);

                if (requiresRedirect)
                {
                    redirectLocation = response.Headers.Location;
                }
                else
                {
                    // This the final response
                    var result = new HttpResult(outcomes);

                    page = pageFactory.Create<T>(this, response, result);
                }
            }

            while (requiresRedirect)
            {
                // Get the redirect address
                Uri fullRedirectLocation;

                if (redirectLocation.IsAbsoluteUri)
                {
                    fullRedirectLocation = redirectLocation;
                }
                else
                {
                    fullRedirectLocation = new Uri(currentResourceLocation, redirectLocation);
                }

                currentResourceLocation = fullRedirectLocation;
                stopwatch = Stopwatch.StartNew();
                task = _client.GetAsync(currentResourceLocation);

                using (var response = task.Result)
                {
                    stopwatch.Stop();

                    var outcome = new HttpOutcome(
                        currentResourceLocation, 
                        response.RequestMessage.Method, 
                        response.StatusCode, 
                        response.ReasonPhrase, 
                        stopwatch.Elapsed);

                    outcomes.Add(outcome);

                    requiresRedirect = IsRedirect(response);

                    if (requiresRedirect)
                    {
                        redirectLocation = response.Headers.Location;
                    }
                    else
                    {
                        // This the final response
                        var result = new HttpResult(outcomes);

                        page = pageFactory.Create<T>(this, response, result);
                    }
                }
            }

            var lastOutcome = outcomes[outcomes.Count - 1];

            if (lastOutcome.StatusCode != expectedStatusCode)
            {
                var message = string.Format(
                    CultureInfo.CurrentCulture, 
                    Resources.Browser_InvalidResponseStatus, 
                    expectedStatusCode, 
                    lastOutcome.StatusCode);

                throw new HttpOutcomeException(message);
            }

            // Validate that the final address matches the page
            if (page.IsOn(currentResourceLocation) == false)
            {
                // We have been requested to go to a location that doesn't match the requested page
                var message = string.Format(
                    CultureInfo.CurrentCulture, 
                    "The url requested is {0} which does not match the location of {1} defined by page {2}.", 
                    currentResourceLocation, 
                    page.TargetLocation, 
                    page.GetType().FullName);

                throw new InvalidOperationException(message);
            }

            return page;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed == false)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    _client.Dispose();
                }

                // There are no unmanaged resources to release, but
                // if we add them, they need to be released here.
            }

            _disposed = true;
        }

        /// <summary>
        /// Determines whether the specified response is redirect.
        /// </summary>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified response is redirect; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsRedirect(HttpResponseMessage response)
        {
            if (response.Headers.Location == null)
            {
                return false;
            }

            if (response.StatusCode == HttpStatusCode.Ambiguous)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.Moved)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.MovedPermanently)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.MultipleChoices)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.Found)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.Redirect)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.RedirectKeepVerb)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.RedirectMethod)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.SeeOther)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.TemporaryRedirect)
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public virtual IPageContentTypeResolver ContentTypeResolver
        {
            get
            {
                return _contentTypeResolver;
            }

            set
            {
                _contentTypeResolver = value;
            }
        }

        /// <summary>
        ///     Gets the cookies for the browser session.
        /// </summary>
        /// <value>
        ///     The cookies for the browser session.
        /// </value>
        public CookieContainer Cookies
        {
            [DebuggerStepThrough]
            get
            {
                return _handler.CookieContainer;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether cookies are used in the browsing session.
        /// </summary>
        /// <value>
        ///     <c>true</c> if cookies are used in the browsing session; otherwise, <c>false</c>.
        /// </value>
        public bool UseCookies
        {
            [DebuggerStepThrough]
            get
            {
                return _handler.UseCookies;
            }

            [DebuggerStepThrough]
            set
            {
                _handler.UseCookies = value;
            }
        }
    }
}