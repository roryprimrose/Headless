namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;

    /// <summary>
    ///     The <see cref="IBrowser" />
    ///     interface defines the members for making browser requests.
    /// </summary>
    public interface IBrowser
    {
        /// <summary>
        /// Executes a GET request to the specified location.
        /// </summary>
        /// <param name="location">
        /// The location to request.
        /// </param>
        /// <param name="expectedStatusCode">
        /// The expected status code.
        /// </param>
        /// <param name="pageFactory">
        /// The page factory.
        /// </param>
        /// <returns>
        /// An <see cref="IPage"/> value.
        /// </returns>
        IPage BrowseTo(
            Uri location, 
            HttpStatusCode expectedStatusCode, 
            Func<IBrowser, HttpResponseMessage, HttpResult, IPage> pageFactory);

        /// <summary>
        /// Executes a POST request to the specified location.
        /// </summary>
        /// <param name="parameters">
        /// The POST parameters.
        /// </param>
        /// <param name="location">
        /// The location to request.
        /// </param>
        /// <param name="expectedStatusCode">
        /// The expected status code.
        /// </param>
        /// <param name="pageFactory">
        /// The page factory.
        /// </param>
        /// <returns>
        /// An <see cref="IPage"/> value.
        /// </returns>
        IPage PostTo(
            IDictionary<string, string> parameters, 
            Uri location, 
            HttpStatusCode expectedStatusCode, 
            Func<IBrowser, HttpResponseMessage, HttpResult, IPage> pageFactory);
    }
}