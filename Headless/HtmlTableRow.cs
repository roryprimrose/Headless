namespace Headless
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
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
        /// <returns>An <see cref="IEnumerable{T}" /> value.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", 
            Justification =
                "The method calculates a result on each invocation " +
                "making a property misleading about the state of the instance.")]
        public IEnumerable<HtmlTableCell> GetCells()
        {
            return Find<HtmlTableCell>().All();
        }
    }
}