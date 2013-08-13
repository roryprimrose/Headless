namespace Headless
{
    using HtmlAgilityPack;

    /// <summary>
    ///     The <see cref="HtmlButton" />
    ///     class provides access to HTML button elements.
    /// </summary>
    [SupportedTag("submit")]
    [SupportedTag("button")]
    public class HtmlButton : HtmlElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlButton"/> class.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        public HtmlButton(HtmlPage page, HtmlNode node) : base(page, node)
        {
        }
    }
}