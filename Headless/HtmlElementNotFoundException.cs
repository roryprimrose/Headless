namespace Headless
{
    using System;
    using System.Runtime.Serialization;
    using System.Xml;

    /// <summary>
    ///     The <see cref="HtmlElementNotFoundException" />
    ///     class identifies when a <see cref="HtmlElement" />
    ///     could not be found for a given request.
    /// </summary>
    [Serializable]
    public class HtmlElementNotFoundException : Exception
    {
        /// <summary>
        ///     The related node.
        /// </summary>
        [NonSerialized]
        private readonly XmlNode _relatedNode;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HtmlElementNotFoundException" /> class.
        /// </summary>
        public HtmlElementNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElementNotFoundException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public HtmlElementNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElementNotFoundException"/> class.
        /// </summary>
        /// <param name="message">
        /// The error message that explains the reason for the exception.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing in
        ///     Visual Basic) if no inner exception is specified.
        /// </param>
        public HtmlElementNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElementNotFoundException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="relatedNode">
        /// The related node.
        /// </param>
        public HtmlElementNotFoundException(string message, XmlNode relatedNode) : base(message)
        {
            _relatedNode = relatedNode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElementNotFoundException"/> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object
        ///     data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual
        ///     information about the source or destination.
        /// </param>
        protected HtmlElementNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        ///     Gets the related node.
        /// </summary>
        /// <value>
        ///     The related node.
        /// </value>
        public XmlNode RelatedNode
        {
            get
            {
                return _relatedNode;
            }
        }
    }
}