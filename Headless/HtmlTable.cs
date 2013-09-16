namespace Headless
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
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
        public HtmlTable(IHtmlPage page, IXPathNavigable node) : base(page, node)
        {
        }

        /// <summary>
        ///     Gets the rows in the table.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}" /> value.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", 
            Justification =
                "The method calculates a result on each invocation " +
                "making a property misleading about the state of the instance.")]
        public IEnumerable<HtmlTableRow> GetRows()
        {
            return Find<HtmlTableRow>().All();
        }
    }
}