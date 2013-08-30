namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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

        /// <inheritdoc />
        public virtual string BuildElementQuery()
        {
            var elementType = typeof(T);
            var supportedTags = elementType.GetSupportedTags().ToList();

            if (supportedTags.Any(x => x.TagName == "*" && x.HasAttributeFilter == false))
            {
                // There is a wildcard tag name that does not filter by attributes
                // This filter alone will return all HTML elements within the current scope
                // There is no need to execute overly complex XPath queries based on all the available supported tags
                return ".//*";
            }

            if (supportedTags.Count == 1)
            {
                // This is the base form which will be in the format //p[@class='sam']
                return ".//" + BuildTagExpression(supportedTags.First());
            }

            // This expression is much more complex and will be in the format //*[self::p[@class='sam'] or self::div][@id='asdf']
            var selector = ".//*[";

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

        /// <inheritdoc />
        public virtual IEnumerable<T> Execute(string query)
        {
            var navigator = _node.GetNavigator();

            var nodes = navigator.Select(query);

            var htmlElements =
                (from IXPathNavigable node in nodes select _page.ElementFactory.Create<T>(_page, node)).ToList();

            var radioButtonNames = new List<string>();

            // Radio buttons are expressed as multiple HTML tags, but need to be returned as a single HtmlRadioButton instance
            // The factory above has already created multiple instances based on the multiple nodes found
            // so we now need to find any radio buttons and filter out the duplicates
            // This is a little inefficient because there is the overhead of creating and initializing the radio button 
            // multiple times however this code is much cleaner
            foreach (var element in htmlElements)
            {
                var radioButton = element as HtmlRadioButton;

                if (radioButton == null)
                {
                    // Not a radio button so no filtering is required
                    yield return element;
                }
                else if (radioButtonNames.Contains(radioButton.Name) == false)
                {
                    radioButtonNames.Add(radioButton.Name);

                    yield return element;
                }
            }
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
                const string AttributeFilterQuery = "*[local-name() = '{0}' and @{1}='{2}']";

                return string.Format(
                    CultureInfo.CurrentCulture, 
                    AttributeFilterQuery, 
                    supportedTag.TagName, 
                    supportedTag.AttributeName, 
                    supportedTag.AttributeValue);
            }

            return "*[local-name() = '" + supportedTag.TagName + "']";
        }
    }
}