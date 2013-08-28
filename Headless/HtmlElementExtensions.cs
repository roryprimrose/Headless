namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
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
        /// No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// More than one element was found.
        /// </exception>
        public static T EnsureSingle<T>(this IEnumerable<T> elements) where T : HtmlElement
        {
            return EnsureSingle(elements, Resources.HtmlElement_MultipleMatchesFound);
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

            var navigable = element.Node;
            var page = element.Page;

            return GetHtmlForm(navigable, page);
        }

        /// <summary>
        /// Gets the HTML form.
        /// </summary>
        /// <param name="navigable">
        /// The navigable.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <returns>
        /// A <see cref="HtmlForm"/> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="navigable"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="page"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="Headless.HtmlElementNotFoundException">
        /// No form element was found for the requested element.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// More than one form element was found for the requested element
        /// </exception>
        public static HtmlForm GetHtmlForm(this IXPathNavigable navigable, IHtmlPage page)
        {
            if (navigable == null)
            {
                throw new ArgumentNullException("navigable");
            }

            if (page == null)
            {
                throw new ArgumentNullException("page");
            }

            var navigator = navigable.GetNavigator();

            var form =
                navigator.SelectAncestors(XPathNodeType.Element, true)
                    .OfType<XPathNavigator>()
                    .FirstOrDefault(x => x.Name.Equals("form", StringComparison.OrdinalIgnoreCase));

            if (form == null)
            {
                throw new HtmlElementNotFoundException(Resources.HtmlElement_GetHtmlForm_FormNotFound, navigable);
            }

            return new HtmlForm(page, form);
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
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="element"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <paramref name="cssClass"/> parameter is <c>null</c>, empty or only contains white-space.
        /// </exception>
        public static bool HasClass(this HtmlElement element, string cssClass)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            if (string.IsNullOrWhiteSpace(cssClass))
            {
                throw new ArgumentException(Resources.Guard_NoValueProvided, "cssClass");
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

        /// <summary>
        /// Ensures that on a single element is found.
        /// </summary>
        /// <typeparam name="T">
        /// The type of element to return.
        /// </typeparam>
        /// <param name="elements">
        /// The elements to validate.
        /// </param>
        /// <param name="multipleElementsFailureMessage">
        /// The multiple elements failure message.
        /// </param>
        /// <returns>
        /// The single matching <typeparamref name="T"/> element.
        /// </returns>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// No elements were found.
        /// </exception>
        internal static T EnsureSingle<T>(this IEnumerable<T> elements, string multipleElementsFailureMessage)
            where T : HtmlElement
        {
            var filteredElements = elements.Take(2).ToList();

            if (filteredElements.Count == 0)
            {
                throw new InvalidHtmlElementMatchException(Resources.HtmlElement_NoMatchFound);
            }

            if (filteredElements.Count > 1)
            {
                throw new InvalidHtmlElementMatchException(multipleElementsFailureMessage);
            }

            return filteredElements[0];
        }
    }
}