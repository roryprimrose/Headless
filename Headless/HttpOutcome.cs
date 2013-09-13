namespace Headless
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Net.Http;
    using Headless.Properties;

    /// <summary>
    ///     The <see cref="HttpOutcome" />
    ///     class is used to identify the outcome of a HTTP request.
    /// </summary>
    public class HttpOutcome
    {
        /// <summary>
        ///     The location.
        /// </summary>
        private readonly Uri _location;

        /// <summary>
        ///     The request method.
        /// </summary>
        private readonly HttpMethod _method;

        /// <summary>
        ///     The response message.
        /// </summary>
        private readonly string _responseMessage;

        /// <summary>
        ///     The response time.
        /// </summary>
        private readonly TimeSpan _responseTime;

        /// <summary>
        ///     The status code.
        /// </summary>
        private readonly HttpStatusCode _statusCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpOutcome"/> class.
        /// </summary>
        /// <param name="location">
        /// The location.
        /// </param>
        /// <param name="method">
        /// The request method.
        /// </param>
        /// <param name="statusCode">
        /// The status code.
        /// </param>
        /// <param name="responseMessage">
        /// The response message.
        /// </param>
        /// <param name="responseTime">
        /// The response time.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="location"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// The <paramref name="location"/> parameter is a relative location.
        /// </exception>
        public HttpOutcome(
            Uri location, 
            HttpMethod method, 
            HttpStatusCode statusCode, 
            string responseMessage, 
            TimeSpan responseTime)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location");
            }
            
            if (location.IsAbsoluteUri == false)
            {
                throw new ArgumentException(Resources.Uri_LocationMustBeAbsolute, "location");
            }

            _location = location;
            _method = method;
            _statusCode = statusCode;
            _responseMessage = responseMessage;
            _responseTime = responseTime;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            const string Layout = "{0} ({1}) returned {2} ({3} - {4}) in {5} milliseconds";

            return string.Format(
                CultureInfo.CurrentCulture, 
                Layout, 
                Location, 
                Method, 
                ResponseMessage, 
                (int)StatusCode, 
                StatusCode, 
                ResponseTime.TotalMilliseconds);
        }

        /// <summary>
        ///     Gets the location.
        /// </summary>
        /// <value>
        ///     The location.
        /// </value>
        public Uri Location
        {
            get
            {
                return _location;
            }
        }

        /// <summary>
        ///     Gets the requet method.
        /// </summary>
        /// <value>
        ///     The request method.
        /// </value>
        public HttpMethod Method
        {
            get
            {
                return _method;
            }
        }

        /// <summary>
        ///     Gets the response message.
        /// </summary>
        /// <value>
        ///     The response message.
        /// </value>
        public string ResponseMessage
        {
            get
            {
                return _responseMessage;
            }
        }

        /// <summary>
        ///     Gets the response time.
        /// </summary>
        /// <value>
        ///     The response time.
        /// </value>
        public TimeSpan ResponseTime
        {
            get
            {
                return _responseTime;
            }
        }

        /// <summary>
        ///     Gets the status code.
        /// </summary>
        /// <value>
        ///     The status code.
        /// </value>
        public HttpStatusCode StatusCode
        {
            get
            {
                return _statusCode;
            }
        }
    }
}