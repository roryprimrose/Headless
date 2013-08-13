namespace Headless
{
    using HtmlAgilityPack;

    /// <summary>
    ///     The <see cref="DynamicHtmlElement" />
    ///     class is used to match any HTML element when using the dynamic support.
    /// </summary>
    [SupportedTag("*")]
    public class DynamicHtmlElement : HtmlElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicHtmlElement"/> class.
        /// </summary>
        /// <param name="page">
        /// The owning page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        public DynamicHtmlElement(IHtmlPage page, HtmlNode node) : base(page, node)
        {
        }
    }
}