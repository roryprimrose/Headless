﻿namespace Headless
{
    using System;
    using System.Net;
    using System.Net.Http;
    using Headless.Activation;

    /// <summary>
    ///     The <see cref="IBrowser" />
    ///     interface defines the members for making browser requests.
    /// </summary>
    public interface IBrowser
    {
        /// <summary>
        /// Executes the specified request and returns a page.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="expectedStatusCode">
        /// The expected status code.
        /// </param>
        /// <param name="pageFactory">
        /// The page factory.
        /// </param>
        /// <returns>
        /// An <typeparamref name="T"/> value.
        /// </returns>
        T Execute<T>(HttpRequestMessage request, HttpStatusCode expectedStatusCode, IPageFactory pageFactory)
            where T : IPage, new();

        /// <summary>
        ///     Gets or sets the content type resolver.
        /// </summary>
        /// <value>
        ///     The content type resolver.
        /// </value>
        IPageContentTypeResolver ContentTypeResolver
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the location validator.
        /// </summary>
        /// <value>
        ///     The location validator.
        /// </value>
        ILocationValidator LocationValidator
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets the last page loaded by the browser.
        /// </summary>
        /// <value>
        ///     The last page loaded by the browser.
        /// </value>
        /// <remarks>Returns <c>null</c> before the first page has been loaded.</remarks>
        IPage Page
        {
            get;
        }

        /// <summary>
        ///     Gets or sets the timeout.
        /// </summary>
        /// <value>
        ///     The timeout.
        /// </value>
        TimeSpan Timeout
        {
            get;
            set;
        }
    }
}