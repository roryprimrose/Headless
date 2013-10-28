namespace Headless
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization;
    using Headless.Properties;

    /// <summary>
    ///     The <see cref="HttpOutcomeException" />
    ///     is used to identify that HTTP response encountered was not expected.
    /// </summary>
    [Serializable]
    public class HttpOutcomeException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpOutcomeException" /> class.
        /// </summary>
        public HttpOutcomeException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpOutcomeException" /> class.
        /// </summary>
        /// <param name="result">
        ///     The result.
        /// </param>
        /// <param name="expectedStatusCode">
        ///     The expected status code.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="result" /> parameter is <c>null</c>.
        /// </exception>
        public HttpOutcomeException(HttpResult result, HttpStatusCode expectedStatusCode)
            : base(GenerateIncorrectStatusMessage(result, expectedStatusCode))
        {
            Result = result;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpOutcomeException" /> class.
        /// </summary>
        /// <param name="result">
        ///     The result.
        /// </param>
        /// <param name="targetLocation">
        ///     The target location.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="result" /> parameter is <c>null</c>.
        /// </exception>
        public HttpOutcomeException(HttpResult result, Uri targetLocation)
            : base(GenerateIncorrectLocationMessage(result, targetLocation))
        {
            Result = result;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpOutcomeException" /> class.
        /// </summary>
        /// <param name="message">
        ///     The message that describes the error.
        /// </param>
        public HttpOutcomeException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpOutcomeException" /> class.
        /// </summary>
        /// <param name="message">
        ///     The message.
        /// </param>
        /// <param name="inner">
        ///     The inner.
        /// </param>
        public HttpOutcomeException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpOutcomeException" /> class.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="inner">The inner.</param>
        public HttpOutcomeException(HttpResult result, Exception inner)
            : this(GenerateFailureMessage(result, inner), inner)
        {
            Result = result;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpOutcomeException" /> class.
        /// </summary>
        /// <param name="info">
        ///     The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object
        ///     data about the exception being thrown.
        /// </param>
        /// <param name="context">
        ///     The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual
        ///     information about the source or destination.
        /// </param>
        protected HttpOutcomeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Result = info.GetValue("Result", typeof(HttpResult)) as HttpResult;
        }

        /// <inheritdoc />
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("Result", Result, typeof(HttpResult));
        }

        /// <summary>
        ///     Generates the failure message.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="inner">The inner.</param>
        /// <returns>The failure message.</returns>
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="result" /> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="inner" /> parameter is <c>null</c>.
        /// </exception>
        private static string GenerateFailureMessage(HttpResult result, Exception inner)
        {
            if (result == null)
            {
                throw new ArgumentNullException("result");
            }

            if (inner == null)
            {
                throw new ArgumentNullException("inner");
            }
            
            var lastOutcome = result.Outcomes.Last();

            var outcomes = result.Outcomes.Aggregate(string.Empty, (x, y) => x + Environment.NewLine + y);

            // We have been requested to go to a location that doesn't match the requested page
            var message = string.Format(
                CultureInfo.CurrentCulture,
                "The {0} request to {1} has failed with the message '{2}'. The responses for this request were: {3}",
                lastOutcome.Method,
                lastOutcome.Location,
                inner.Message,
                outcomes);

            return message;
        }

        /// <summary>
        ///     Generates the incorrect location message.
        /// </summary>
        /// <param name="result">
        ///     The result.
        /// </param>
        /// <param name="targetLocation">
        ///     The target location.
        /// </param>
        /// <returns>
        ///     A <see cref="string" /> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="result" /> parameter is <c>null</c>.
        /// </exception>
        private static string GenerateIncorrectLocationMessage(HttpResult result, Uri targetLocation)
        {
            if (result == null)
            {
                throw new ArgumentNullException("result");
            }

            var lastOutcome = result.Outcomes.Last();

            var outcomes = result.Outcomes.Aggregate(string.Empty, (x, y) => x + Environment.NewLine + y);

            // We have been requested to go to a location that doesn't match the requested page
            var message = string.Format(
                CultureInfo.CurrentCulture,
                Resources.HttpOutcomeException_InvalidLocation,
                lastOutcome.Location,
                targetLocation,
                outcomes);

            return message;
        }

        /// <summary>
        ///     Generates the incorrect status message.
        /// </summary>
        /// <param name="result">
        ///     The result.
        /// </param>
        /// <param name="expectedStatusCode">
        ///     The expected status code.
        /// </param>
        /// <returns>
        ///     A <see cref="string" /> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="result" /> parameter is <c>null</c>.
        /// </exception>
        private static string GenerateIncorrectStatusMessage(HttpResult result, HttpStatusCode expectedStatusCode)
        {
            if (result == null)
            {
                throw new ArgumentNullException("result");
            }

            var lastOutcome = result.Outcomes.Last();

            var outcomes = result.Outcomes.Aggregate(string.Empty, (x, y) => x + Environment.NewLine + y);

            var message = string.Format(
                CultureInfo.CurrentCulture,
                Resources.HttpOutcomeException_InvalidResponseStatus,
                expectedStatusCode,
                lastOutcome.StatusCode,
                outcomes);

            return message;
        }

        /// <summary>
        ///     Gets the result.
        /// </summary>
        /// <value>
        ///     The result.
        /// </value>
        public HttpResult Result
        {
            get;
            private set;
        }
    }
}