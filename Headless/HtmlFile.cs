namespace Headless
{
    using System.Collections.Generic;
    using System.Xml.XPath;
    using Headless.Activation;

    /// <summary>
    ///     The <see cref="HtmlFile" />
    ///     class is used to provide access to HTML file input elements.
    /// </summary>
    [SupportedTag("input", "type", "file")]
    public class HtmlFile : HtmlFormElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlFile"/> class.
        /// </summary>
        /// <param name="page">
        /// The owning page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        public HtmlFile(IHtmlPage page, IXPathNavigable node) : base(page, node)
        {
        }

        /// <inheritdoc />
        protected internal override IEnumerable<PostEntry> BuildPostData()
        {
            yield return new PostFileEntry(Name, Value);
        }
    }
}