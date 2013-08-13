namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Headless.Activation;
    using HtmlAgilityPack;

    /// <summary>
    /// The <see cref="HtmlElementFinder{T}"/>
    ///     class is used to provide the common wrapper around the finding logic for <see cref="HtmlElement"/> instances in a
    ///     <see cref="HtmlPage"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of <see cref="HtmlElement"/> to find.
    /// </typeparam>
    public class HtmlElementFinder<T> where T : HtmlElement
    {
        /// <summary>
        ///     The HTML node.
        /// </summary>
        private readonly HtmlNode _node;

        /// <summary>
        ///     The owning page.
        /// </summary>
        private readonly IHtmlPage _page;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElementFinder{T}"/> class.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        public HtmlElementFinder(IHtmlPage page) : this(page, page.Document.DocumentNode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElementFinder{T}"/> class.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        public HtmlElementFinder(HtmlElement element) : this(element.Page, element.Node)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElementFinder{T}"/> class.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        public HtmlElementFinder(IHtmlPage page, HtmlNode node)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page");
            }

            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            _page = page;
            _node = node;
        }

        /// <summary>
        ///     Finds all the elements of the requested type anywhere under this node.
        /// </summary>
        /// <returns>
        ///     An <see cref="IEnumerable{T}" /> value.
        /// </returns>
        public IEnumerable<T> All()
        {
            var tagSelector = BuildTagNameSelector(typeof(T));
            var query = "//" + tagSelector;

            return BuildElementResults(_page, _node, query);
        }

        /// <summary>
        /// Finds the elements by attribute anywhere under this node.
        /// </summary>
        /// <param name="attributeName">
        /// Name of the attribute.
        /// </param>
        /// <param name="attributeValue">
        /// The attribute value.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        public IEnumerable<T> ByAttribute(string attributeName, string attributeValue)
        {
            var tagSelector = BuildTagNameSelector(typeof(T));
            var query = "//" + tagSelector + "[@" + attributeName + "='" + attributeValue + "']";

            return BuildElementResults(_page, _node, query);
        }

        /// <summary>
        /// Finds the elements by name anywhere under this node.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        public IEnumerable<T> ByName(string name)
        {
            return ByAttribute("name", name);
        }

        /// <summary>
        /// Finds the elements by predicate anywhere under this node.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        public IEnumerable<T> ByPredicate(Func<T, bool> predicate)
        {
            return All().Where(predicate);
        }

        /// <summary>
        /// Finds the elements by text anywhere under this node.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        public IEnumerable<T> ByText(string text)
        {
            var tagSelector = BuildTagNameSelector(typeof(T));
            var query = "//" + tagSelector + "[./text() = '" + text + "']";

            return BuildElementResults(_page, _node, query);
        }

        /// <summary>
        /// Finds the element by id anywhere under this node.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        public T FindById(string id)
        {
            var matches = ByAttribute("id", id);

            return matches.EnsureSingle();
        }

        /// <summary>
        /// Finds the element by name anywhere under this node.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        public T FindByName(string name)
        {
            var matches = ByAttribute("name", name);

            return matches.EnsureSingle();
        }

        /// <summary>
        /// Builds the element results.
        /// </summary>
        /// <param name="owningPage">
        /// The owning page.
        /// </param>
        /// <param name="parentNode">
        /// The parent node.
        /// </param>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        private static IEnumerable<T> BuildElementResults(IHtmlPage owningPage, HtmlNode parentNode, string query)
        {
            var nodes = parentNode.SelectNodes(query);

            if (nodes == null)
            {
                return new List<T>();
            }

            return nodes.Select(node => HtmlElementFactory.Create<T>(owningPage, node));
        }

        /// <summary>
        /// Builds the tag name xpath selector.
        /// </summary>
        /// <param name="elementType">
        /// Type of the element.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> value.
        /// </returns>
        private static string BuildTagNameSelector(Type elementType)
        {
            var supportedTags = elementType.GetSupportedTags();

            if (supportedTags.Count == 1)
            {
                return supportedTags.First();
            }

            return "(" + string.Join("|", supportedTags) + ")";
        }
    }
}