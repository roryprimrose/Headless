namespace Headless
{
    using System;
    using System.Net;
    using System.Net.Http;

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
        ///     Gets the location of the page.
        /// </summary>
        /// <value>
        ///     The location of the page.
        /// </value>
        Uri Location
        {
            get;
        }

        /// <summary>
        ///     Gets the HTTP result.
        /// </summary>
        /// <value>
        ///     The HTTP result.
        /// </value>
        HttpResult Result
        {
            get;
        }

        /// <summary>
        ///     Gets the status code.
        /// </summary>
        /// <value>
        ///     The status code.
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
    }
}