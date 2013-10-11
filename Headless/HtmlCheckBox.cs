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
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="page"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="node"/> parameter is <c>null</c>.
        /// </exception>
        public HtmlCheckBox(IHtmlPage page, IXPathNavigable node) : base(page, node)
        {
        }

        /// <inheritdoc />
        protected internal override IEnumerable<PostEntry> BuildPostData()
        {
            if (Checked == false)
            {
                yield break;
            }

            var value = Value;

            if (string.IsNullOrEmpty(value))
            {
                // The value of "on" is the default by convention.
                value = "on";
            }

            yield return new PostEntry(Name, value);
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