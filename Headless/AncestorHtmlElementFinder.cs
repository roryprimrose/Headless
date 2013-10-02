namespace Headless
{
    /// <summary>
    /// The <see cref="AncestorHtmlElementFinder{T}"/>
    ///     class is used to provide the common wrapper around the finding logic for <see cref="HtmlElement"/> instances in a
    ///     <see cref="HtmlPage"/> by searching ancestor elements.
    /// </summary>
    /// <typeparam name="T">
    /// The type of <see cref="HtmlElement"/> to find.
    /// </typeparam>
    public class AncestorHtmlElementFinder<T> : DefaultHtmlElementFinder<T> where T : HtmlElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AncestorHtmlElementFinder{T}"/> class.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="page"/> parameter is <c>null</c>.
        /// </exception>
        public AncestorHtmlElementFinder(IHtmlPage page) : base(page)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AncestorHtmlElementFinder{T}"/> class.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="element"/> parameter is <c>null</c>.
        /// </exception>
        public AncestorHtmlElementFinder(HtmlElement element) : base(element)
        {
        }

        /// <inheritdoc />
        protected override string QueryAxes()
        {
            return "ancestor";
        }
    }
}