namespace Headless
{
    using System.Net;
    using System.Xml;
    using System.Xml.XPath;
    using Headless.Activation;

    /// <summary>
    ///     The <see cref="IHtmlPage" />
    ///     interface defines the structure of an HTML page.
    /// </summary>
    public interface IHtmlPage : IPage
    {
        /// <summary>
        ///     Clones the current page as a <typeparamref name="T" /> value.
        /// </summary>
        /// <typeparam name="T">The type of page to return.</typeparam>
        /// <returns>A <typeparamref name="T" /> value.</returns>
        T CloneAs<T>() where T : IHtmlPage, new();

        /// <summary>
        ///     Provides a finding implementation for searching for child <see cref="HtmlElement" /> values.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="HtmlElement" /> to find.</typeparam>
        /// <returns>A <see cref="IHtmlElementFinder{T}" /> value.</returns>
        IHtmlElementFinder<T> Find<T>() where T : HtmlElement;

        /// <summary>
        /// Initializes the page using the specified values.
        /// </summary>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="statusCode">
        /// The status code.
        /// </param>
        /// <param name="statusDescription">
        /// The status description.
        /// </param>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <param name="document">
        /// The document.
        /// </param>
        void Initialize(
            IBrowser browser, 
            HttpStatusCode statusCode, 
            string statusDescription, 
            HttpResult result, 
            XmlDocument document);

        /// <summary>
        ///     Gets the HTML document of the page.
        /// </summary>
        /// <value>
        ///     The HTML document of the page.
        /// </value>
        IXPathNavigable Document
        {
            get;
        }

        /// <summary>
        ///     Gets the element factory.
        /// </summary>
        /// <value>
        ///     The element factory.
        /// </value>
        IHtmlElementFactory ElementFactory
        {
            get;
        }

        /// <summary>
        ///     Gets the HTML node for the page.
        /// </summary>
        /// <value>
        ///     The HTML node for the page.
        /// </value>
        IXPathNavigable Node
        {
            get;
        }
    }
}