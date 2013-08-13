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
        void Initialize(Browser browser, HttpResponseMessage response);

        /// <summary>
        ///     Gets the owning browser.
        /// </summary>
        /// <value>
        ///     The owning browser.
        /// </value>
        Browser Browser
        {
            get;
        }

        /// <summary>
        /// Determines whether the specified location is valid for the page.
        /// </summary>
        /// <param name="location">
        /// The current location.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified location is valid for the page; otherwise, <c>false</c>.
        /// </returns>
        bool IsValidLocation(Uri location);

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