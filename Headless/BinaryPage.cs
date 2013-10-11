namespace Headless
{
    using System;
    using System.Net.Http;

    /// <summary>
    ///     The <see cref="BinaryPage" />
    ///     class provides the binary response from a <see cref="Browser" /> request as an in-memory byte array.
    /// </summary>
    /// <remarks>This class stores the binary response from the server in memory. 
    /// Care must be taken when using this class to load large amounts of binary data.
    /// Any data loaded from the server will remain in memory until the page instance is garbage collected.
    /// </remarks>
    public abstract class BinaryPage : Page
    {
        /// <summary>
        ///     Stores the content.
        /// </summary>
        private byte[] _content;

        /// <summary>
        ///     Gets the content of the page.
        /// </summary>
        /// <returns>A <see cref="byte" /> array.</returns>
        public byte[] GetContent()
        {
            return _content;
        }

        /// <inheritdoc />
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="content"/> parameter is <c>null</c>.
        /// </exception>
        protected internal override void SetContent(HttpContent content)
        {
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }

            _content = content.ReadAsByteArrayAsync().Result;
        }
    }
}