namespace Headless
{
    /// <summary>
    ///     The <see cref="PageContentType" />
    ///     enum identifies broad page content types for a HTTP payload.
    /// </summary>
    public enum PageContentType
    {
        /// <summary>
        ///     The document contains HTML formatted text.
        /// </summary>
        Html = 0,

        /// <summary>
        ///     The document contains text.
        /// </summary>
        Text,

        /// <summary>
        ///     The document contains binary data.
        /// </summary>
        Binary
    }
}