namespace Headless
{
    using System.Net.Http;

    /// <summary>
    ///     The <see cref="TextPage" />
    ///     class provides the Text response from a <see cref="Browser" /> request.
    /// </summary>
    public abstract class TextPage : Page
    {
        /// <summary>
        ///     Stores the content.
        /// </summary>
        private string _content;

        /// <inheritdoc />
        internal override void SetContent(HttpContent content)
        {
            var result = content.ReadAsStringAsync().Result;

            _content = result;
        }

        /// <summary>
        ///     Gets the content.
        /// </summary>
        /// <value>
        ///     The content.
        /// </value>
        public string Content
        {
            get
            {
                return _content;
            }
        }
    }
}