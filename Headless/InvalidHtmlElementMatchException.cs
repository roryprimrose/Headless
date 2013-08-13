namespace Headless
{
    using System;

    /// <summary>
    ///     The <see cref="InvalidHtmlElementMatchException" />
    ///     class identifies scenarios where an invalid element match is encountered.
    /// </summary>
    public class InvalidHtmlElementMatchException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidHtmlElementMatchException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public InvalidHtmlElementMatchException(string message) : base(message)
        {
        }
    }
}