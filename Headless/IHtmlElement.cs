namespace Headless
{
    using HtmlAgilityPack;

    /// <summary>
    ///     The <see cref="IHtmlElement" />
    ///     interface defines the structure of an element in an <see cref="IHtmlPage" />.
    /// </summary>
    public interface IHtmlElement
    {
        /// <summary>
        ///     Gets the HTML node of the element.
        /// </summary>
        /// <value>
        ///     The HTML node of the element.
        /// </value>
        HtmlNode Node
        {
            get;
        }

        /// <summary>
        ///     Gets the page that contains the element.
        /// </summary>
        /// <value>
        ///     The page that contains the element.
        /// </value>
        IHtmlPage Page
        {
            get;
        }
    }
}