namespace Headless.UnitTests
{
    using System;
    using System.Net;
    using System.Net.Http;

    /// <summary>
    ///     The <see cref="PageWrapper" />
    ///     class is used for internal testing.
    /// </summary>
    internal class PageWrapper : IPage
    {
        /// <inheritdoc />
        public void Initialize(IBrowser browser, HttpResponseMessage response, HttpResult result)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool IsOn(Uri location)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IBrowser Browser
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public Uri Location
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public HttpResult Result
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public HttpStatusCode StatusCode
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public string StatusDescription
        {
            get;
            private set;
        }
    }
}