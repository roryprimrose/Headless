namespace Headless
{
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
        /// <param name="page">The owning page.</param>
        /// <param name="node">The node.</param>
        public HtmlCheckBox(IHtmlPage page, IXPathNavigable node)
            : base(page, node)
        {
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is checked; otherwise, <c>false</c>.
        /// </value>
        public bool Checked
        {
            get
            {
                var navigator = Node.GetNavigator();

                var checkedFound = navigator.MoveToAttribute("checked", string.Empty);

                return checkedFound;
            }

            set
            {
                var navigator = Node.GetNavigator();

                var checkedFound = navigator.MoveToAttribute("checked", string.Empty);

                if (checkedFound == value)
                {
                    // No change is required
                    return;
                }

                if (value)
                {
                    // Set the attribute
                    navigator.SetAttribute(string.Empty, "checked", string.Empty, "checked");
                }
                else
                {
                    // Remove the checked attribute if it exists
                    navigator.DeleteSelf();
                }
            }
        }
    }
}