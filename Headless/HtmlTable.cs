namespace Headless
{
    using System.Collections.Generic;
    using System.Xml.XPath;
    using Headless.Activation;

    /// <summary>
    ///     The <see cref="HtmlTable" />
    ///     class is used to represent a HTML table element.
    /// </summary>
    [SupportedTag("table")]
    public class HtmlTable : HtmlElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlTable"/> class.
        /// </summary>
        /// <param name="page">
        /// The owning page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="page"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="node"/> parameter is <c>null</c>.
        /// </exception>
        public HtmlTable(IHtmlPage page, IXPathNavigable node) : base(page, node)
        {
        }

        /// <summary>
        ///     Gets the rows in the table.
        /// </summary>
        /// <value>
        ///     The rows.
        /// </value>
        public IEnumerable<HtmlTableRow> Rows
        {
            get
            {
                return Find<HtmlTableRow>().All();
            }
        }
    }
}