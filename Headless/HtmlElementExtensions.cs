namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    ///     The <see cref="HtmlElementExtensions" />
    ///     class is used to provide extension methods for the <see cref="HtmlElement" /> class.
    /// </summary>
    public static class HtmlElementExtensions
    {
        /// <summary>
        /// Determines whether the element contains the specified CSS class.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <param name="classToFind">
        /// The class to find.
        /// </param>
        /// <returns>
        /// <c>true</c> if the element contains the specified CSS class; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsCssClass(this HtmlElement element, string classToFind)
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

            return
                matches.OfType<Match>()
                    .Any(x => string.Equals(x.Value, classToFind, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Ensures that on a single element is found.
        /// </summary>
        /// <typeparam name="T">
        /// The type of element to return.
        /// </typeparam>
        /// <param name="matchingElements">
        /// The matching elements.
        /// </param>
        /// <returns>
        /// The single matching <typeparamref name="T"/> element.
        /// </returns>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// More than one element was found.
        /// </exception>
        public static T EnsureSingle<T>(this IEnumerable<T> matchingElements) where T : HtmlElement
        {
            var elements = matchingElements.Take(2).ToList();

            if (elements.Count == 0)
            {
                throw new InvalidHtmlElementMatchException("No HTML elements match the criteria.");
            }

            if (elements.Count > 1)
            {
                throw new InvalidHtmlElementMatchException(
                    "Multiple HTML elements match the criteria but only one element is expected.");
            }

            return elements[0];
        }
    }
}