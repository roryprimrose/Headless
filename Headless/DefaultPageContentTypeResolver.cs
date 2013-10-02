namespace Headless
{
    using System;

    /// <summary>
    ///     The <see cref="DefaultPageContentTypeResolver" />
    ///     class provides the default implementation for resolving page content types.
    /// </summary>
    public class DefaultPageContentTypeResolver : IPageContentTypeResolver
    {
        /// <inheritdoc />
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="mediaType"/> parameter is <c>null</c>.
        /// </exception>
        public virtual PageContentType DeterminePageType(string mediaType)
        {
            if (mediaType == null)
            {
                throw new ArgumentNullException("mediaType");
            }

            if (mediaType.Equals("text/html", StringComparison.OrdinalIgnoreCase))
            {
                return PageContentType.Html;
            }

            if (mediaType.StartsWith("text/", StringComparison.OrdinalIgnoreCase))
            {
                return PageContentType.Text;
            }

            return PageContentType.Binary;
        }
    }
}