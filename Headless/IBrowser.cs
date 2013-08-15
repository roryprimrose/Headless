namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    /// <summary>
    ///     The <see cref="IBrowser" />
    ///     interface defines the members for making browser requests.
    /// </summary>
    public interface IBrowser
    {
        /// <summary>
        /// Executes a GET request to the specified location.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <param name="location">
        /// The location to request.
        /// </param>
        /// <param name="expectedStatusCode">
        /// The expected status code.
        /// </param>
        /// <returns>
        /// A <see cref="IPage"/> value.
        /// </returns>
        T GoTo<T>(Uri location, HttpStatusCode expectedStatusCode) where T : IPage, new();

        /// <summary>
        /// Executes a POST request to the specified location.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <param name="location">
        /// The location to request.
        /// </param>
        /// <param name="expectedStatusCode">
        /// The expected status code.
        /// </param>
        /// <param name="parameters">
        /// The POST parameters.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        T PostTo<T>(Uri location, HttpStatusCode expectedStatusCode, IDictionary<string, string> parameters)
            where T : IPage, new();
    }
}