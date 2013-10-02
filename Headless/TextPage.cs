namespace Headless
{
    using System;
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
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="content"/> parameter is <c>null</c>.
        /// </exception>
        protected internal override void SetContent(HttpContent content)
        {
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }

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