namespace Headless
{
    using HtmlAgilityPack;

    /// <summary>
    ///     The <see cref="HtmlForm" />
    ///     class exposes all the form fields for a form tag.
    /// </summary>
    [SupportedTag("form")]
    public class HtmlForm : HtmlElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlForm"/> class.
        /// </summary>
        /// <param name="page">
        /// The owning page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        public HtmlForm(IHtmlPage page, HtmlNode node) : base(page, node)
        {
        }

        /// <summary>
        ///     Gets the action of the form.
        /// </summary>
        /// <value>
        ///     The action of the form.
        /// </value>
        public string Action
        {
            get
            {
                return GetAttributeValue("action");
            }
        }

        /// <summary>
        ///     Gets the method of the form.
        /// </summary>
        /// <value>
        ///     The method of the form.
        /// </value>
        public string Method
        {
            get
            {
                return GetAttributeValue("method");
            }
        }

        /// <summary>
        ///     Gets the name of the form.
        /// </summary>
        /// <value>
        ///     The name of the form.
        /// </value>
        public string Name
        {
            get
            {
                return GetAttributeValue("name");
            }
        }

        /// <summary>
        ///     Gets the target of the form.
        /// </summary>
        /// <value>
        ///     The target of the form.
        /// </value>
        public string Target
        {
            get
            {
                return GetAttributeValue("target");
            }
        }
    }
}