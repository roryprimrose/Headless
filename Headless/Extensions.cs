namespace Headless
{
    using System.Globalization;

    /// <summary>
    ///     The <see cref="Extensions" />
    ///     class is used to provide common extension methods.
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Builds to string.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> value.
        /// </returns>
        internal static string BuildToString(this IPage page)
        {
            const string Layout = "{0}: {1} - {2} ({3}) - {4}";

            return string.Format(
                CultureInfo.CurrentCulture,
                Layout,
                page.GetType().Name,
                page.Location,
                page.StatusCode,
                (int)page.StatusCode,
                page.StatusDescription);
        }
    }
}