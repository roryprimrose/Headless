namespace Headless
{
    using System.Xml.XPath;
    using Headless.Activation;

    /// <summary>
    ///     The <see cref="HtmlTableCell" />
    ///     class is used to represent a HTML table cell element.
    /// </summary>
    [SupportedTag("td")]
    [SupportedTag("th")]
    public class HtmlTableCell : HtmlElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlTableCell"/> class.
        /// </summary>
        /// <param name="page">
        /// The owning page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        public HtmlTableCell(IHtmlPage page, IXPathNavigable node) : base(page, node)
        {
        }
    }
}