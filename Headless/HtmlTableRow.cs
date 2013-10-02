namespace Headless
{
    using System.Collections.Generic;
    using System.Xml.XPath;
    using Headless.Activation;

    /// <summary>
    ///     The <see cref="HtmlTableRow" />
    ///     class is used to represent a HTML table row element.
    /// </summary>
    [SupportedTag("tr")]
    public class HtmlTableRow : HtmlElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlTableRow"/> class.
        /// </summary>
        /// <param name="page">
        /// The owning page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        public HtmlTableRow(IHtmlPage page, IXPathNavigable node) : base(page, node)
        {
        }

        /// <summary>
        ///     Gets the cells in the table row.
        /// </summary>
        /// <value>
        ///     The cells.
        /// </value>
        public IEnumerable<HtmlTableCell> Cells
        {
            get
            {
                return Find<HtmlTableCell>().All();
            }
        }
    }
}