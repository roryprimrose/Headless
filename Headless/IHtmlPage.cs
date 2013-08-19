namespace Headless
{
    using System.Xml;

    /// <summary>
    ///     The <see cref="IHtmlPage" />
    ///     interface defines the structure of an HTML page.
    /// </summary>
    public interface IHtmlPage : IPage
    {
        /// <summary>
        ///     Provides a finding implementation for searching for child <see cref="HtmlElement" /> values.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="HtmlElement" /> to find.</typeparam>
        /// <returns>A <see cref="HtmlElementFinder{T}" /> value.</returns>
        HtmlElementFinder<T> Find<T>() where T : HtmlElement;

        /// <summary>
        ///     Gets the HTML document of the page.
        /// </summary>
        /// <value>
        ///     The HTML document of the page.
        /// </value>
        XmlDocument Document
        {
            get;
        }

        /// <summary>
        ///     Gets the HTML node for the page.
        /// </summary>
        /// <value>
        ///     The HTML node for the page.
        /// </value>
        XmlNode Node
        {
            get;
        }
    }
}