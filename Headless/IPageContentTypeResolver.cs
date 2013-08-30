namespace Headless
{
    /// <summary>
    ///     The <see cref="IPageContentTypeResolver" />
    ///     interface defines the members for resolving page content type information.
    /// </summary>
    public interface IPageContentTypeResolver
    {
        /// <summary>
        /// Determines the type of page content from the specified MIME type.
        /// </summary>
        /// <param name="mediaType">Type of the media.</param>
        /// <returns>
        /// A <see cref="PageContentType" /> value.
        /// </returns>
        PageContentType DeterminePageType(string mediaType);
    }
}