namespace Headless
{
    using System.Collections.Generic;
    using System.Linq;
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
        ///     Gets the HTML form that contains the current element.
        /// </summary>
        /// <value>
        ///     The form.
        /// </value>
        /// <exception cref="Headless.HtmlElementNotFoundException">No form element was found for the requested element.</exception>
        /// <exception cref="InvalidHtmlElementMatchException">More than one form element was found for the requested element</exception>
        public HtmlForm Form
        {
            get
            {
                return FindAncestor<HtmlForm>().Singular();
            }
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
                return GetAttribute("name");
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
                return GetAttribute("value");
            }

            set
            {
                SetAttribute("value", value);
            }
        }
    }
}