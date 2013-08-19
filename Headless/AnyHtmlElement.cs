namespace Headless
{
    using System.Xml.XPath;

    /// <summary>
    ///     The <see cref="AnyHtmlElement" />
    ///     class is used to match any HTML element.
    /// </summary>
    [SupportedTag("*")]
    public class AnyHtmlElement : HtmlElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnyHtmlElement"/> class.
        /// </summary>
        /// <param name="page">
        /// The owning page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        public AnyHtmlElement(IHtmlPage page, IXPathNavigable node) : base(page, node)
        {
        }
    }
}