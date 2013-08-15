namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Headless.Properties;

    /// <summary>
    ///     The <see cref="Browser" />
    ///     class provides a wrapper around a HTTP browsing session.
    /// </summary>
    public class Browser : IDisposable
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

        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <param name="location">
        /// The specific location to request rather than that identified by the page.
        /// </param>
        /// <param name="expectedOutcome">
        /// The expected outcome.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// No location has been specified for the browser to request.
        ///     or
        /// </exception>
        /// <exception cref="HttpOutcomeException">
        /// An unexpected HTTP outcome was encountered.
        /// </exception>
        internal T ExecuteAction<T>(
            Uri location, 
            HttpStatusCode expectedOutcome, 
            Func<Uri, Task<HttpResponseMessage>> action) where T : IPage, new()
        {
            var page = new T();

            if (location == null)
            {
                location = page.Location;
            }

            var currentResourceLocation = location;

            if (currentResourceLocation == null)
            {
                throw new InvalidOperationException("No location has been specified for the browser to request.");
            }

            var outcomes = new List<HttpOutcome>();

            var stopwatch = Stopwatch.StartNew();

            var task = action(currentResourceLocation);
            var response = task.Result;

            stopwatch.Stop();

            var outcome = new HttpOutcome(
                currentResourceLocation, 
                response.StatusCode, 
                response.ReasonPhrase, 
                stopwatch.Elapsed);

            outcomes.Add(outcome);

            while (IsRedirect(response))
            {
                // Get the redirect address
                var newLocation = response.Headers.Location;
                Uri redirectTo;

                if (newLocation.IsAbsoluteUri)
                {
                    redirectTo = newLocation;
                }
                else
                {
                    redirectTo = new Uri(currentResourceLocation, newLocation);
                }

                currentResourceLocation = redirectTo;
                stopwatch = Stopwatch.StartNew();
                task = action(currentResourceLocation);
                response = task.Result;

                stopwatch.Stop();

                outcome = new HttpOutcome(
                    currentResourceLocation, 
                    response.StatusCode, 
                    response.ReasonPhrase, 
                    stopwatch.Elapsed);

                outcomes.Add(outcome);
            }

            if (outcome.StatusCode != expectedOutcome)
            {
                var message = string.Format(
                    CultureInfo.CurrentCulture, 
                    Resources.Browser_InvalidResponseStatus, 
                    expectedOutcome, 
                    outcome.StatusCode);

                throw new HttpOutcomeException(message);
            }

            var result = new HttpResult(outcomes);

            page.Initialize(this, response, result);

            // Validate that the final address matches the page
            if (page.IsOn(currentResourceLocation) == false)
            {
                // We have been requested to go to a location that doesn't match the requested page
                var message = string.Format(
                    CultureInfo.CurrentCulture, 
                    "The url requested is {0} which does not match the location of {1} defined by page {2}.",
                    currentResourceLocation, 
                    page.Location, 
                    page.GetType().FullName);

                throw new InvalidOperationException(message);
            }

            return page;
        }

        /// <summary>
        /// Goes to.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <param name="location">
        /// The specific location to request rather than that identified by the page.
        /// </param>
        /// <param name="expectedOutcome">
        /// The expected outcome.
        /// </param>
        /// <returns>
        /// A <see cref="Page"/> value.
        /// </returns>
        internal T GoTo<T>(Uri location, HttpStatusCode expectedOutcome) where T : IPage, new()
        {
            return ExecuteAction<T>(location, expectedOutcome, x => _client.GetAsync(x));
        }

        /// <summary>
        /// Posts to.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <param name="location">
        /// The specific location to request rather than that identified by the page.
        /// </param>
        /// <param name="expectedOutcome">
        /// The expected outcome.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        internal T PostTo<T>(
            Uri location, 
            HttpStatusCode expectedOutcome, 
            IDictionary<string, string> parameters) where T : IPage, new()
        {
            return ExecuteAction<T>(
                location, 
                expectedOutcome, 
                x => _client.PostAsync(x, new FormUrlEncodedContent(parameters)));
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