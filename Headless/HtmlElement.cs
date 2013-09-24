namespace Headless
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Xml.XPath;
    using Headless.Activation;

    /// <summary>
    ///     The <see cref="HtmlElement" />
    ///     class provides the base wrapper around an HTML element.
    /// </summary>
    public abstract class HtmlElement : IHtmlElement
    {
        /// <summary>
        ///     Stores a reference to the html node for the element.
        /// </summary>
        private readonly IXPathNavigable _node;

        /// <summary>
        ///     Stores the reference to the owning page.
        /// </summary>
        private readonly IHtmlPage _page;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElement"/> class.
        /// </summary>
        /// <param name="page">
        /// The owning page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        protected HtmlElement(IHtmlPage page, IXPathNavigable node)
        {
            _page = page;
            _node = node;

            ValidateNode();
        }

        /// <summary>
        /// Determines whether the specified attribute exists.
        /// </summary>
        /// <param name="attributeName">
        /// Name of the attribute.
        /// </param>
        /// <returns>
        /// <c>true</c> if the attribute exists; otherwise <c>false</c>.
        /// </returns>
        public bool AttributeExists(string attributeName)
        {
            var navigator = Node.GetNavigator();

            var exists = navigator.MoveToAttribute(attributeName, string.Empty);

            if (exists)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Provides a finding implementation for searching for child <see cref="HtmlElement" /> values.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="HtmlElement" /> to find.</typeparam>
        /// <returns>A <see cref="IHtmlElementFinder{T}" /> value.</returns>
        [DebuggerStepThrough]
        public IHtmlElementFinder<T> Find<T>() where T : HtmlElement
        {
            return new DefaultHtmlElementFinder<T>(this);
        }

        /// <summary>
        /// Gets the attribute value.
        /// </summary>
        /// <param name="attributeName">
        /// Name of the attribute.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> value or <c>null</c> if the attribute does not exist.
        /// </returns>
        public string GetAttribute(string attributeName)
        {
            var navigator = Node.GetNavigator();

            var exists = navigator.MoveToAttribute(attributeName, string.Empty);

            if (exists == false)
            {
                return null;
            }

            return navigator.Value;
        }

        /// <summary>
        /// Sets the attribute value.
        /// </summary>
        /// <param name="attributeName">
        /// Name of the attribute.
        /// </param>
        /// <param name="attributeValue">
        /// The attribute value.
        /// </param>
        public void SetAttribute(string attributeName, string attributeValue)
        {
            Node.SetAttribute(string.Empty, attributeName, string.Empty, attributeValue);
        }

        /// <summary>
        ///     Validates the node.
        /// </summary>
        /// <exception cref="InvalidHtmlElementException">
        ///     The node does not match any of the supported HTML tags for the type.
        /// </exception>
        private void ValidateNode()
        {
            var supportedTags = GetType().GetSupportedTags();

            if (supportedTags.Any(x => x.TagName == "*"))
            {
                // This type supports any tag
                return;
            }

            // Attempt to match a supporting tag that has specific attribute matching first
            var attributedSupportingTags = supportedTags.Where(x => x.HasAttributeFilter);

            if (
                attributedSupportingTags.Any(
                    x =>
                        x.TagName.Equals(TagName, StringComparison.OrdinalIgnoreCase) &&
                        x.AttributeValue.Equals(GetAttribute(x.AttributeName), StringComparison.OrdinalIgnoreCase)))
            {
                // We have a match between this element type and the HTML content of the node
                return;
            }

            var tagOnlySupportingTags = supportedTags.Where(x => x.HasAttributeFilter == false);

            if (tagOnlySupportingTags.Any(x => x.TagName.Equals(TagName, StringComparison.OrdinalIgnoreCase)))
            {
                // We have a match between this element type and the HTML content of the node
                return;
            }

            throw new InvalidHtmlElementException(Node, supportedTags);
        }

        /// <summary>
        ///     Gets the CSS class.
        /// </summary>
        /// <value>
        ///     The CSS class.
        /// </value>
        public string CssClass
        {
            [DebuggerStepThrough]
            get
            {
                return GetAttribute("class");
            }
        }

        /// <inheritdoc />
        public string Html
        {
            [DebuggerStepThrough]
            get
            {
                var navigator = Node.GetNavigator();

                return navigator.OuterXml;
            }
        }

        /// <summary>
        ///     Gets the id of the element.
        /// </summary>
        /// <value>
        ///     The id of the element.
        /// </value>
        public string Id
        {
            [DebuggerStepThrough]
            get
            {
                return GetAttribute("id");
            }
        }

        /// <summary>
        ///     Gets the name of the tag.
        /// </summary>
        /// <value>
        ///     The name of the tag.
        /// </value>
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", 
            Justification = "HTML tag names are express in lowercase by convention.")]
        public string TagName
        {
            [DebuggerStepThrough]
            get
            {
                var navigator = Node.GetNavigator();

                return navigator.Name.ToLowerInvariant();
            }
        }

        /// <summary>
        ///     Gets the text of the element.
        /// </summary>
        /// <value>
        ///     The text of the element.
        /// </value>
        public virtual string Text
        {
            [DebuggerStepThrough]
            get
            {
                var navigator = Node.GetNavigator();

                return navigator.Value;
            }
        }

        /// <summary>
        ///     Gets the node.
        /// </summary>
        /// <value>
        ///     The node.
        /// </value>
        protected internal IXPathNavigable Node
        {
            [DebuggerStepThrough]
            get
            {
                return ((IHtmlElement)this).Node;
            }
        }

        /// <summary>
        ///     Gets the owning page.
        /// </summary>
        /// <value>
        ///     The owning page.
        /// </value>
        protected internal IHtmlPage Page
        {
            [DebuggerStepThrough]
            get
            {
                return ((IHtmlElement)this).Page;
            }
        }

        /// <inheritdoc />
        IXPathNavigable IHtmlElement.Node
        {
            [DebuggerStepThrough]
            get
            {
                return _node;
            }
        }

        /// <inheritdoc />
        IHtmlPage IHtmlElement.Page
        {
            [DebuggerStepThrough]
            get
            {
                return _page;
            }
        }
    }
}