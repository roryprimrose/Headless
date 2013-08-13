namespace Headless
{
    using System;

    /// <summary>
    ///     The <see cref="HttpResultException" />
    ///     is used to identify that the <see cref="HttpResult{T}" /> value stored is not what was expected by the caller.
    /// </summary>
    public class HttpResultException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResultException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public HttpResultException(string message) : base(message)
        {
        }
    }
}