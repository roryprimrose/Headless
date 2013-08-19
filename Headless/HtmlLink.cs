namespace Headless
{
    using System;
    using System.Xml;

    /// <summary>
    ///     The <see cref="HtmlLink" />
    ///     class provides access to HTML anchor elements.
    /// </summary>
    [SupportedTag("a")]
    public class HtmlLink : HtmlElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlLink"/> class.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        public HtmlLink(IHtmlPage page, XmlNode node) : base(page, node)
        {
        }

        /// <summary>
        ///     Clicks the specified element.
        /// </summary>
        /// <returns>
        ///     A dynamic value for the page.
        /// </returns>
        public dynamic Click()
        {
            return Click<DynamicHtmlPage>();
        }

        /// <summary>
        ///     Clicks the specified element.
        /// </summary>
        /// <typeparam name="T">The type of page to return.</typeparam>
        /// <returns>
        ///     A <see cref="IPage" /> value.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">The link does not have an href attribute.</exception>
        public T Click<T>() where T : IPage, new()
        {
            var href = Node.Attributes["href"].Value;

            if (string.IsNullOrWhiteSpace(href))
            {
                throw new InvalidOperationException("The link does not have an href attribute.");
            }

            var location = new Uri(href, UriKind.RelativeOrAbsolute);

            if (location.IsAbsoluteUri == false)
            {
                location = new Uri(Page.Location, location);
            }

            return Page.Browser.GoTo<T>(location);
        }

        /// <summary>
        ///     Gets the target of the hyperlink.
        /// </summary>
        /// <value>
        ///     The target of the hyperlink.
        /// </value>
        public string Target
        {
            get
            {
                return GetAttributeValue("target");
            }
        }
    }
}