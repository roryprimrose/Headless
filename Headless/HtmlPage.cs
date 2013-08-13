namespace Headless
{
    using System.Diagnostics;
    using System.Net.Http;
    using HtmlAgilityPack;

    /// <summary>
    ///     The <see cref="HtmlPage" />
    ///     class provides the HTML response from a <see cref="Browser" /> request.
    /// </summary>
    public abstract class HtmlPage : Page
    {
        /// <summary>
        ///     Stores the content.
        /// </summary>
        private HtmlDocument _content;

        /// <summary>
        ///     Provides a finding implementation for searching for child <see cref="HtmlElement" /> values.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="HtmlElement" /> to find.</typeparam>
        /// <returns>A <see cref="HtmlElementFinder{T}" /> value.</returns>
        public HtmlElementFinder<T> Find<T>() where T : HtmlElement
        {
            return new HtmlElementFinder<T>(this);
        }

        /// <inheritdoc />
        internal override void SetContent(HttpContent content)
        {
            var result = content.ReadAsStreamAsync().Result;

            _content = new HtmlDocument();

            _content.Load(result);
        }

        /// <summary>
        ///     Gets the content.
        /// </summary>
        /// <value>
        ///     The content.
        /// </value>
        public HtmlDocument Content
        {
            [DebuggerStepThrough]
            get
            {
                return _content;
            }
        }
    }
}