namespace Headless
{
    using System;
    using System.Xml.XPath;
    using Headless.Activation;

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
        public HtmlLink(IHtmlPage page, IXPathNavigable node) : base(page, node)
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
            var location = HrefLocation;

            return Page.Browser.GoTo(location);
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
            var location = HrefLocation;

            return Page.Browser.GoTo<T>(location);
        }

        /// <summary>
        ///     Gets the href attribute value.
        /// </summary>
        /// <value>
        ///     The href attribute value.
        /// </value>
        /// <exception cref="System.InvalidOperationException">The link does not have an href attribute.</exception>
        public string Href
        {
            get
            {
                var href = GetAttributeValue("href");

                if (string.IsNullOrWhiteSpace(href))
                {
                    throw new InvalidOperationException("The link does not have an href attribute.");
                }

                return href;
            }
        }

        /// <summary>
        ///     Gets the href value.
        /// </summary>
        /// <value>
        ///     The href value.
        /// </value>
        /// <exception cref="System.InvalidOperationException">The link does not have an href attribute.</exception>
        public Uri HrefLocation
        {
            get
            {
                var location = new Uri(Href, UriKind.RelativeOrAbsolute);

                if (location.IsAbsoluteUri == false)
                {
                    location = new Uri(Page.Location, location);
                }

                return location;
            }
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