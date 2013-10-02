namespace Headless
{
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
        ///     Provides a finding implementation for searching for descendant <see cref="HtmlElement" /> values.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="HtmlElement" /> to find.</typeparam>
        /// <returns>A <see cref="IHtmlElementFinder{T}" /> value.</returns>
        IHtmlElementFinder<T> Find<T>() where T : HtmlElement;

        /// <summary>
        ///     Provides a finding implementation for searching for ancestor <see cref="HtmlElement" /> values.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="HtmlElement" /> to find.</typeparam>
        /// <returns>A <see cref="IHtmlElementFinder{T}" /> value.</returns>
        IHtmlElementFinder<T> FindAncestor<T>() where T : HtmlElement;

        /// <summary>
        /// Initializes the page using the details of another page.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <remarks>
        /// This is used internally as part of the <see cref="CloneAs{T}"/> method.
        /// </remarks>
        void Initialize(IHtmlPage page);

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
        ///     Gets the HTML of the element.
        /// </summary>
        /// <value>
        ///     The HTML of the element.
        /// </value>
        string Html
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