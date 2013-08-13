namespace Headless
{
    using HtmlAgilityPack;

    /// <summary>
    ///     The <see cref="IHtmlPage" />
    ///     interface defines the structure of an HTML page.
    /// </summary>
    public interface IHtmlPage : IPage
    {
        /// <summary>
        ///     Gets the HTML document of the page.
        /// </summary>
        /// <value>
        ///     The HTML document of the page.
        /// </value>
        HtmlDocument Document
        {
            get;
        }

        /// <summary>
        ///     Gets the HTML node for the page.
        /// </summary>
        /// <value>
        ///     The HTML node for the page.
        /// </value>
        HtmlNode Node
        {
            get;
        }
    }
}