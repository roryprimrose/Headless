namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Xml.XPath;

    /// <summary>
    ///     The <see cref="InvalidHtmlElementException" />
    ///     is used to identify that an HTML element is not supported in the current context.
    /// </summary>
    [Serializable]
    public class InvalidHtmlElementException : Exception
    {
        /// <summary>
        ///     Stores the html node relate to the exception.
        /// </summary>
        [NonSerialized]
        private readonly IXPathNavigable _node;

        /// <summary>
        ///     Stores the tags supported by the element.
        /// </summary>
        [NonSerialized]
        private readonly IEnumerable<SupportedTagAttribute> _supportedTags;

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidHtmlElementException" /> class.
        /// </summary>
        public InvalidHtmlElementException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidHtmlElementException"/> class.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <param name="supportedTags">
        /// The supported tags.
        /// </param>
        public InvalidHtmlElementException(IXPathNavigable node, IReadOnlyCollection<SupportedTagAttribute> supportedTags)
            : this(BuildSupportedTagsMessage(node, supportedTags))
        {
            _node = node;
            _supportedTags = supportedTags;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidHtmlElementException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public InvalidHtmlElementException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidHtmlElementException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="inner">
        /// The inner.
        /// </param>
        public InvalidHtmlElementException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidHtmlElementException"/> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object
        ///     data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual
        ///     information about the source or destination.
        /// </param>
        protected InvalidHtmlElementException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Builds the supported tags message.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <param name="tags">
        /// The tags.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> value.
        /// </returns>
        private static string BuildSupportedTagsMessage(IXPathNavigable node, IEnumerable<SupportedTagAttribute> tags)
        {
            var supportedTags = tags.Select(x => x.ToString()).Aggregate((i, j) => i + Environment.NewLine + j);

            var navigator = node.GetNavigator();
            
            var message = string.Format(
                CultureInfo.CurrentCulture, 
                "The specified '{0}' element is invalid. The supported tags for this node are: {1}", 
                navigator.Name, 
                supportedTags);

            return message;
        }

        /// <summary>
        ///     Gets the node.
        /// </summary>
        /// <value>
        ///     The node.
        /// </value>
        public IXPathNavigable Node
        {
            get
            {
                return _node;
            }
        }

        /// <summary>
        ///     Gets the supported tags.
        /// </summary>
        /// <value>
        ///     The supported tags.
        /// </value>
        public IEnumerable<SupportedTagAttribute> SupportedTags
        {
            get
            {
                return _supportedTags;
            }
        }
    }
}