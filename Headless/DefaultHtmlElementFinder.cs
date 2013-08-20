namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.XPath;
    using Headless.Activation;

    /// <summary>
    /// The <see cref="DefaultHtmlElementFinder{T}"/>
    ///     class is used to provide the common wrapper around the finding logic for <see cref="HtmlElement"/> instances in a
    ///     <see cref="HtmlPage"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of <see cref="HtmlElement"/> to find.
    /// </typeparam>
    public class DefaultHtmlElementFinder<T> : IHtmlElementFinder<T> where T : HtmlElement
    {
        /// <summary>
        ///     The HTML node.
        /// </summary>
        private readonly IXPathNavigable _node;

        /// <summary>
        ///     The owning page.
        /// </summary>
        private readonly IHtmlPage _page;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultHtmlElementFinder{T}"/> class.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="page"/> parameter is <c>null</c>.
        /// </exception>
        public DefaultHtmlElementFinder(IHtmlPage page)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page");
            }

            _page = page;

            var navigator = page.Document.GetNavigator();

            navigator.MoveToRoot();

            _node = navigator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultHtmlElementFinder{T}"/> class.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="element"/> parameter is <c>null</c>.
        /// </exception>
        public DefaultHtmlElementFinder(HtmlElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            _page = element.Page;
            _node = element.Node;
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
        public string BuildElementQuery(Type elementType)
        {
            var supportedTags = elementType.GetSupportedTags().ToList();

            if (supportedTags.Count == 1)
            {
                // This is the base form which will be in the format //p[@class='sam']
                return "//" + BuildTagExpression(supportedTags.First());
            }

            // This expression is much more complex and will be in the format //*[self::p[@class='sam'] or self::div][@id='asdf']
            var selector = "//*[";

            for (var index = 0; index < supportedTags.Count; index++)
            {
                if (index > 0)
                {
                    selector += " or ";
                }

                selector += "self::" + BuildTagExpression(supportedTags[index]);
            }

            selector += "]";

            return selector;
        }

        /// <summary>
        /// Builds the element results.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        public IEnumerable<T> Execute(string query)
        {
            var navigator = _node.GetNavigator();

            var nodes = navigator.Select(query);

            return from IXPathNavigable node in nodes select _page.ElementFactory.Create<T>(_page, node);
        }

        /// <summary>
        /// Builds the tag expression.
        /// </summary>
        /// <param name="supportedTag">
        /// The supported tag.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> value.
        /// </returns>
        private static string BuildTagExpression(SupportedTagAttribute supportedTag)
        {
            if (supportedTag.HasAttributeFilter)
            {
                return supportedTag.TagName + "[@" + supportedTag.AttributeName + "='" + supportedTag.AttributeValue +
                       "']";
            }

            return supportedTag.TagName;
        }
    }
}