namespace Headless
{
    using System;
    using System.Globalization;
    using System.Net;

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
        /// <param name="statusCode">
        /// The status code.
        /// </param>
        /// <param name="responseMessage">
        /// The response message.
        /// </param>
        /// <param name="responseTime">
        /// The response time.
        /// </param>
        public HttpOutcome(Uri location, HttpStatusCode statusCode, string responseMessage, TimeSpan responseTime)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location");
            }

            _location = location;
            _statusCode = statusCode;
            _responseMessage = responseMessage;
            _responseTime = responseTime;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            const string Layout = "{0} returned {1} ({2} - {3}) in {4} milliseconds";

            return string.Format(
                CultureInfo.CurrentCulture, 
                Layout, 
                Location, 
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