namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using HtmlAgilityPack;

    /// <summary>
    ///     The <see cref="InvalidHtmlElementException" />
    ///     is used to identify that an HTML element is not supported in the current context.
    /// </summary>
    public class InvalidHtmlElementException : Exception
    {
        /// <summary>
        ///     Stores the html node relate to the exception.
        /// </summary>
        private readonly HtmlNode _node;

        /// <summary>
        ///     Stores the tags supported by the element.
        /// </summary>
        private readonly IEnumerable<string> _supportedTags;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidHtmlElementException"/> class.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <param name="supportedTags">
        /// The supported tags.
        /// </param>
        public InvalidHtmlElementException(HtmlNode node, IEnumerable<string> supportedTags)
        {
            _node = node;
            _supportedTags = supportedTags;
        }

        /// <inheritdoc />
        public override string Message
        {
            get
            {
                var supportedTags = _supportedTags.Aggregate((i, j) => i + Environment.NewLine + j);

                var message = string.Format(
                    CultureInfo.CurrentCulture, 
                    "The specified '{0}' element is invalid. The supported tags for this node are: {1}", 
                    _node.Name, 
                    supportedTags);

                return message;
            }
        }
    }
}