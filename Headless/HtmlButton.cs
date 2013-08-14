namespace Headless
{
    using HtmlAgilityPack;

    /// <summary>
    ///     The <see cref="HtmlButton" />
    ///     class provides access to HTML button elements.
    /// </summary>
    [SupportedTag("submit")]
    [SupportedTag("button")]
    public class HtmlButton : HtmlFormElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlButton"/> class.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        public HtmlButton(HtmlPage page, HtmlNode node) : base(page, node)
        {
        }

        /// <summary>
        ///     Clicks the specified button.
        /// </summary>
        /// <typeparam name="T">The type of page to return.</typeparam>
        /// <returns>
        ///     A <typeparamref name="T" /> value.
        /// </returns>
        public T Click<T>() where T : Page, new()
        {
            var form = this.GetHtmlForm();

            return form.Submit<T>();
        }
    }
}