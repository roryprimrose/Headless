namespace Headless
{
    using System.Collections.Generic;
    using System.Xml.XPath;
    using Headless.Activation;

    /// <summary>
    ///     The <see cref="HtmlListItem" />
    ///     class provides access to the HTML option elements for lists.
    /// </summary>
    [SupportedTag("option")]
    public class HtmlListItem : HtmlFormElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlListItem"/> class.
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
        public HtmlListItem(IHtmlPage page, IXPathNavigable node) : base(page, node)
        {
        }

        /// <inheritdoc />
        protected internal override IEnumerable<PostEntry> BuildPostData()
        {
            yield break;
        }

        /// <summary>
        ///     Gets the post value.
        /// </summary>
        /// <value>
        ///     The post value.
        /// </value>
        public string PostValue
        {
            get
            {
                var value = Value;

                if (string.IsNullOrEmpty(value))
                {
                    value = Text;
                }

                return value;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this option is selected.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this option is selected; otherwise, <c>false</c>.
        /// </value>
        public bool Selected
        {
            get
            {
                return Node.IsSelected();
            }

            set
            {
                Node.SetSelected(value);
            }
        }
    }
}