namespace Headless.UnitTests
{
    using System.IO;
    using System.Net.Http;
    using System.Text;

    /// <summary>
    ///     The <see cref="StreamContentFactory" />
    ///     class is used to create <see cref="StreamContent" />
    ///     values.
    /// </summary>
    internal static class StreamContentFactory
    {
        /// <summary>
        /// Builds a <see cref="StreamContent"/> from the specified HTML.
        /// </summary>
        /// <param name="html">
        /// The HTML.
        /// </param>
        /// <returns>
        /// A <see cref="StreamContent"/> value.
        /// </returns>
        public static StreamContent FromHtml(string html)
        {
            var stream = new MemoryStream();

            using (var writer = new StreamWriter(stream, Encoding.UTF8, 1024, true))
            {
                writer.Write(html);
            }

            stream.Position = 0;

            return new StreamContent(stream);
        }
    }
}