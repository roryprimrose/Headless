namespace Headless
{
    using System;
    using System.Runtime.Serialization;

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
        /// Initializes a new instance of the <see cref="HttpOutcomeException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public HttpOutcomeException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpOutcomeException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="inner">
        /// The inner.
        /// </param>
        public HttpOutcomeException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpOutcomeException"/> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object
        ///     data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual
        ///     information about the source or destination.
        /// </param>
        protected HttpOutcomeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}