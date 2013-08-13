namespace Headless
{
    using HtmlAgilityPack;

    /// <summary>
    ///     The <see cref="HtmlFormElement" />
    ///     class provides the base description of an element in an HTML form.
    /// </summary>
    public abstract class HtmlFormElement : HtmlElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlFormElement"/> class.
        /// </summary>
        /// <param name="page">
        /// The owning page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        protected HtmlFormElement(HtmlPage page, HtmlNode node) : base(page, node)
        {
        }

        /// <summary>
        ///     Gets the name of the form element.
        /// </summary>
        /// <value>
        ///     The name of the form element.
        /// </value>
        public string Name
        {
            get
            {
                return GetAttributeValue("name");
            }
        }

        /// <summary>
        ///     Gets the value of the form element.
        /// </summary>
        /// <value>
        ///     The value of the form element.
        /// </value>
        public string Value
        {
            get
            {
                return GetAttributeValue("value");
            }
        }
    }
}