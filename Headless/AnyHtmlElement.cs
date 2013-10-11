namespace Headless
{
    using System.Xml.XPath;
    using Headless.Activation;

    /// <summary>
    ///     The <see cref="AnyHtmlElement" />
    ///     class is used by <see cref="IHtmlElementFinder{T}"/> to match any HTML element.
    /// </summary>
    [SupportedTag("*")]
    public class AnyHtmlElement : HtmlElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnyHtmlElement"/> class.
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
        public AnyHtmlElement(IHtmlPage page, IXPathNavigable node) : base(page, node)
        {
        }
    }
}