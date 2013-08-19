namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;
    using System.Xml.XPath;
    using Headless.Properties;

    /// <summary>
    ///     The <see cref="HtmlElementExtensions" />
    ///     class is used to provide extension methods for the <see cref="HtmlElement" /> class.
    /// </summary>
    public static class HtmlElementExtensions
    {
        /// <summary>
        /// Ensures that on a single element is found.
        /// </summary>
        /// <typeparam name="T">
        /// The type of element to return.
        /// </typeparam>
        /// <param name="elements">
        /// The elements to validate.
        /// </param>
        /// <returns>
        /// The single matching <typeparamref name="T"/> element.
        /// </returns>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// More than one element was found.
        /// </exception>
        public static T EnsureSingle<T>(this IEnumerable<T> elements) where T : HtmlElement
        {
            var filteredElements = elements.Take(2).ToList();

            if (filteredElements.Count == 0)
            {
                throw new InvalidHtmlElementMatchException(Resources.HtmlElement_NoMatchFound);
            }

            if (filteredElements.Count > 1)
            {
                throw new InvalidHtmlElementMatchException(Resources.HtmlElement_MultipleMatchesFound);
            }

            return filteredElements[0];
        }

        /// <summary>
        /// Gets the HTML form that contains the specified element.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <returns>
        /// A <see cref="HtmlForm"/> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="element"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="Headless.HtmlElementNotFoundException">
        /// No form element was found for the requested element.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// More than one form element was found for the requested element
        /// </exception>
        public static HtmlForm GetHtmlForm(this HtmlElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            var currentElement = element as HtmlForm;

            if (currentElement != null)
            {
                return currentElement;
            }

            var navigator = element.Node.GetNavigator();

            var form =
                navigator.SelectAncestors(XPathNodeType.Element, true)
                    .OfType<XPathNavigator>()
                    .FirstOrDefault(x => x.Name.Equals("form", StringComparison.OrdinalIgnoreCase));

            if (form == null)
            {
                throw new HtmlElementNotFoundException(Resources.HtmlElement_GetHtmlForm_FormNotFound, element.Node);
            }

            return new HtmlForm(element.Page, form);
        }

        /// <summary>
        /// Determines whether the element contains the specified CSS class.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <param name="cssClass">
        /// The CSS class to find.
        /// </param>
        /// <returns>
        /// <c>true</c> if the element contains the specified CSS class; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasClass(this HtmlElement element, string cssClass)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            var css = element.CssClass;

            if (string.IsNullOrWhiteSpace(css))
            {
                return false;
            }

            var matches = Regex.Matches(css, @"\w\S+\w", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return matches.OfType<Match>()
                .Any(x => string.Equals(x.Value, cssClass, StringComparison.OrdinalIgnoreCase));
        }
    }
}