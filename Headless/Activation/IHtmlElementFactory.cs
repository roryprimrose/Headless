namespace Headless.Activation
{
    using System.Xml.XPath;

    /// <summary>
    ///     The <see cref="IHtmlElementFactory" />
    ///     interface defines the members for creating <see cref="HtmlElement" /> instances.
    /// </summary>
    public interface IHtmlElementFactory
    {
        /// <summary>
        /// Creates the specified page.
        /// </summary>
        /// <typeparam name="T">
        /// The type of element to return.
        /// </typeparam>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        T Create<T>(IHtmlPage page, IXPathNavigable node) where T : HtmlElement;
    }
}