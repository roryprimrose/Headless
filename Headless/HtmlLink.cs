namespace Headless
{
    using HtmlAgilityPack;

    /// <summary>
    ///     The <see cref="HtmlLink" />
    ///     class provides access to HTML anchor elements.
    /// </summary>
    [SupportedTag("a")]
    public class HtmlLink : HtmlElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlLink"/> class.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        public HtmlLink(HtmlPage page, HtmlNode node) : base(page, node)
        {
        }

        /// <summary>
        ///     Gets the target of the hyperlink.
        /// </summary>
        /// <value>
        ///     The target of the hyperlink.
        /// </value>
        public string Target
        {
            get
            {
                return Node.Attributes["target"].Value;
            }
        }
    }
}