namespace Headless
{
    using System;
    using HtmlAgilityPack;

    /// <summary>
    ///     The <see cref="HtmlElementNotFoundException" />
    ///     class identifies when a <see cref="HtmlElement" />
    ///     could not be found for a given request.
    /// </summary>
    public class HtmlElementNotFoundException : Exception
    {
        /// <summary>
        ///     The related node.
        /// </summary>
        private readonly HtmlNode _relatedNode;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElementNotFoundException"/> class.
        /// </summary>
        /// <param name="relatedNode">
        /// The related node.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        public HtmlElementNotFoundException(HtmlNode relatedNode, string message) : base(message)
        {
            _relatedNode = relatedNode;
        }

        /// <summary>
        ///     Gets the related node.
        /// </summary>
        /// <value>
        ///     The related node.
        /// </value>
        public HtmlNode RelatedNode
        {
            get
            {
                return _relatedNode;
            }
        }
    }
}