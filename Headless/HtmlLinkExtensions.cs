namespace Headless
{
    using System;

    /// <summary>
    ///     The <see cref="HtmlLinkExtensions" />
    ///     class provides extension methods for the <see cref="HtmlLink" /> class.
    /// </summary>
    public static class HtmlLinkExtensions
    {
        /// <summary>
        /// Clicks the specified element.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <returns>
        /// A <see cref="HttpResult{T}"/> value.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// The link does not have an href attribute.
        /// </exception>
        public static T Click<T>(this HtmlLink element) where T : Page, new()
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            var href = element.Node.Attributes["href"].Value;

            if (string.IsNullOrWhiteSpace(href))
            {
                throw new InvalidOperationException("The link does not have an href attribute.");
            }

            var location = new Uri(href, UriKind.RelativeOrAbsolute);

            if (location.IsAbsoluteUri == false)
            {
                location = new Uri(element.Page.Location, location);
            }

            return element.Page.Browser.GoTo<T>(location);
        }
    }
}