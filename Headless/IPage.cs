namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text.RegularExpressions;

    /// <summary>
    ///     The <see cref="IPage" />
    ///     interface defines the structure of a page returned from a <see cref="Browser" />.
    /// </summary>
    public interface IPage
    {
        /// <summary>
        /// Initializes the page using the specified browser and response.
        /// </summary>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <param name="result">
        /// The HTTP result.
        /// </param>
        void Initialize(IBrowser browser, HttpResponseMessage response, HttpResult result);

        /// <summary>
        /// Determines whether the the page is on the specified location.
        /// </summary>
        /// <param name="location">
        /// The current location.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified location is valid for the page; otherwise, <c>false</c>.
        /// </returns>
        bool IsOn(Uri location);

        /// <summary>
        ///     Gets the owning browser.
        /// </summary>
        /// <value>
        ///     The owning browser.
        /// </value>
        IBrowser Browser
        {
            get;
        }

        /// <summary>
        ///     Gets the current location of the page.
        /// </summary>
        /// <value>
        ///     The current location of the page.
        /// </value>
        Uri Location
        {
            get;
        }

        /// <summary>
        ///     Gets the location expressions for validating the location of the page.
        /// </summary>
        /// <value>
        ///     The location expressions for validating the location of the page.
        /// </value>
        IEnumerable<Regex> LocationExpressions
        {
            get;
        }

        /// <summary>
        ///     Gets the HTTP result for the page.
        /// </summary>
        /// <value>
        ///     The HTTP result for the page.
        /// </value>
        HttpResult Result
        {
            get;
        }

        /// <summary>
        ///     Gets the final status code for the page.
        /// </summary>
        /// <value>
        ///     The final status code for the page.
        /// </value>
        HttpStatusCode StatusCode
        {
            get;
        }

        /// <summary>
        ///     Gets the status description.
        /// </summary>
        /// <value>
        ///     The status description.
        /// </value>
        string StatusDescription
        {
            get;
        }

        /// <summary>
        ///     Gets the location the browser should navigate to in order to load this page.
        /// </summary>
        /// <value>
        ///     The location the browser should navigate to in order to load this page.
        /// </value>
        /// <remarks>
        ///     The <see cref="IBrowser" /> may also use this location for validation that the final response matches this
        ///     location. The actual implementation of this logic is determined by the
        ///     <see cref="IBrowser.LocationValidator" />.
        /// </remarks>
        Uri TargetLocation
        {
            get;
        }
    }
}