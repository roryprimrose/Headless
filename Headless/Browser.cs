﻿namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Net;
    using System.Net.Http;

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
        /// Goes to.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <param name="locationToMatch">
        /// The location to match.
        /// </param>
        /// <param name="expectedOutcome">
        /// The expected outcome.
        /// </param>
        /// <returns>
        /// A <see cref="Page"/> value.
        /// </returns>
        internal T GoTo<T>(Uri locationToMatch, HttpStatusCode expectedOutcome) where T : IPage, new()
        {
            var page = new T();

            Uri currentResourceLocation;

            if (locationToMatch == null)
            {
                currentResourceLocation = page.Location;
            }
            else
            {
                currentResourceLocation = locationToMatch;
            }

            if (currentResourceLocation == null)
            {
                throw new InvalidOperationException("No location has been specified for the browser to request.");
            }

            var outcomes = new List<HttpOutcome>();

            var stopwatch = Stopwatch.StartNew();

            var task = _client.GetAsync(currentResourceLocation);
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
                task = _client.GetAsync(currentResourceLocation);
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
                    "StatusCode expected was {0} but {1} was returned.", 
                    expectedOutcome, 
                    outcome.StatusCode);

                throw new HttpResultException(message);
            }

            var result = new HttpResult(outcomes);

            page.Initialize(this, response, result);

            // Validate that the final address matches the page
            if (page.IsOn(outcome.Location) == false)
            {
                // We have been requested to go to a location that doesn't match the requested page
                var message = string.Format(
                    CultureInfo.CurrentCulture, 
                    "The url requested is {0} which does not match the location of {1} defined by page {2}.", 
                    locationToMatch, 
                    page.Location, 
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

        /// <summary>
        ///     Gets the cookies.
        /// </summary>
        /// <value>
        ///     The cookies.
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