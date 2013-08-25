namespace Headless
{
    using System.Collections.Generic;
    using System.Xml.XPath;
    using Headless.Activation;

    /// <summary>
    ///     The <see cref="HtmlCheckBox" />
    ///     class is used to represent an HTML checkbox.
    /// </summary>
    [SupportedTag("input", "type", "checkbox")]
    public class HtmlCheckBox : HtmlFormElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlCheckBox"/> class.
        /// </summary>
        /// <param name="page">
        /// The owning page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        public HtmlCheckBox(IHtmlPage page, IXPathNavigable node) : base(page, node)
        {
        }

        /// <inheritdoc />
        protected internal override IEnumerable<PostEntry> BuildPostData()
        {
            if (Checked)
            {
                yield return new PostEntry(Name, Value);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is checked; otherwise, <c>false</c>.
        /// </value>
        public bool Checked
        {
            get
            {
                return Node.IsChecked();
            }

            set
            {
                Node.SetChecked(value);
            }
        }
    }
}