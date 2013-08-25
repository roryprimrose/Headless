namespace Headless
{
    using System.Collections.Generic;
    using System.Xml.XPath;

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
        protected HtmlFormElement(IHtmlPage page, IXPathNavigable node) : base(page, node)
        {
        }

        /// <summary>
        ///     Builds the post data.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}" /> value.</returns>
        protected internal virtual IEnumerable<PostEntry> BuildPostData()
        {
            yield return new PostEntry(Name, Value);
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
        ///     Gets or sets the value of the form element.
        /// </summary>
        /// <value>
        ///     The value of the form element.
        /// </value>
        public virtual string Value
        {
            get
            {
                return GetAttributeValue("value");
            }

            set
            {
                SetAttributeValue("value", value);
            }
        }
    }
}