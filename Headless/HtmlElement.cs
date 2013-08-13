namespace Headless
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using HtmlAgilityPack;

    /// <summary>
    ///     The <see cref="HtmlElement" />
    ///     class provides the base wrapper around an HTML element.
    /// </summary>
    public abstract class HtmlElement
    {
        /// <summary>
        ///     Stores a reference to the html node for the element.
        /// </summary>
        private readonly HtmlNode _node;

        /// <summary>
        ///     Stores the reference to the owning page.
        /// </summary>
        private readonly HtmlPage _page;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElement"/> class.
        /// </summary>
        /// <param name="page">
        /// The owning page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        protected HtmlElement(HtmlPage page, HtmlNode node)
        {
            _page = page;
            _node = node;

            ValidateNode();
        }

        /// <summary>
        ///     Provides a finding implementation for searching for child <see cref="HtmlElement" /> values.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="HtmlElement" /> to find.</typeparam>
        /// <returns>A <see cref="HtmlElementFinder{T}" /> value.</returns>
        public HtmlElementFinder<T> Find<T>() where T : HtmlElement
        {
            return new HtmlElementFinder<T>(this);
        }

        /// <summary>
        ///     Validates the node.
        /// </summary>
        /// <exception cref="InvalidHtmlElementException">
        ///     new[]
        ///     {
        ///     a
        ///     }
        /// </exception>
        private void ValidateNode()
        {
            var supportedTags = GetType().GetSupportedTags();

            if (supportedTags.Contains(TagName, StringComparer.OrdinalIgnoreCase) == false)
            {
                throw new InvalidHtmlElementException(Node, supportedTags);
            }
        }

        /// <summary>
        ///     Gets the CSS class.
        /// </summary>
        /// <value>
        ///     The CSS class.
        /// </value>
        public string CssClass
        {
            get
            {
                var attribute = Node.Attributes["class"];

                if (attribute == null)
                {
                    return string.Empty;
                }

                return attribute.Value;
            }
        }

        /// <summary>
        ///     Gets the HTML of the element.
        /// </summary>
        /// <value>
        ///     The HTML of the element.
        /// </value>
        public string Html
        {
            get
            {
                return Node.OuterHtml;
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
            get
            {
                var attribute = Node.Attributes["id"];
                if (attribute == null)
                {
                    return string.Empty;
                }

                return attribute.Value;
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
                return Node.Name.ToLowerInvariant();
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
            get
            {
                return Node.InnerText;
            }
        }

        /// <summary>
        ///     Gets the node.
        /// </summary>
        /// <value>
        ///     The node.
        /// </value>
        protected internal HtmlNode Node
        {
            [DebuggerStepThrough]
            get
            {
                return _node;
            }
        }

        /// <summary>
        ///     Gets the owning page.
        /// </summary>
        /// <value>
        ///     The owning page.
        /// </value>
        protected internal HtmlPage Page
        {
            [DebuggerStepThrough]
            get
            {
                return _page;
            }
        }
    }
}