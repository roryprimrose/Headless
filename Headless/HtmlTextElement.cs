namespace Headless
{
    using System.Xml;
    
    /// <summary>
    ///     The <see cref="HtmlTextElement" />
    ///     is used to represent input type=text and textarea elements.
    ///     It is also the default fallback type for input elements.
    /// </summary>
    [SupportedTag("input", "type", "color")]
    [SupportedTag("input", "type", "date")]
    [SupportedTag("input", "type", "datetime")]
    [SupportedTag("input", "type", "datetime-local")]
    [SupportedTag("input", "type", "email")]
    [SupportedTag("input", "type", "file")]
    [SupportedTag("input", "type", "hidden")]
    [SupportedTag("input", "type", "month")]
    [SupportedTag("input", "type", "number")]
    [SupportedTag("input", "type", "password")]
    [SupportedTag("input", "type", "range")]
    [SupportedTag("input", "type", "search")]
    [SupportedTag("input", "type", "tel")]
    [SupportedTag("input", "type", "text")]
    [SupportedTag("input", "type", "time")]
    [SupportedTag("input", "type", "url")]
    [SupportedTag("input", "type", "week")]
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
        public HtmlTextElement(IHtmlPage page, XmlNode node) : base(page, node)
        {
        }
    }
}