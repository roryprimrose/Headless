namespace Headless
{
    using HtmlAgilityPack;

    /// <summary>
    ///     The <see cref="HtmlTextElement" />
    ///     is used to represent input type=text and textarea elements.
    ///     It is also the default fallback type for input elements.
    /// </summary>
    [SupportedTag("input")]
    [SupportedTag("textarea")]
    public class HtmlTextElement : HtmlFormElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlTextElement"/> class.
        /// </summary>
        /// <param name="page">
        /// The owning page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        public HtmlTextElement(IHtmlPage page, HtmlNode node) : base(page, node)
        {
        }
    }
}