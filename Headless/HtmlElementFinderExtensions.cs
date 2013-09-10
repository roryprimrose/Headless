namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using Headless.Properties;

    /// <summary>
    ///     The <see cref="HtmlElementFinderExtensions" />
    ///     class provides extension methods for the <see cref="IHtmlElementFinder{T}" />
    ///     interface.
    /// </summary>
    public static class HtmlElementFinderExtensions
    {
        /// <summary>
        /// Finds all the elements of the requested type anywhere under this node.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="HtmlElement"/> to return.
        /// </typeparam>
        /// <param name="finder">
        /// The finder.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="finder"/> parameter is <c>null</c>.
        /// </exception>
        public static IEnumerable<T> All<T>(this IHtmlElementFinder<T> finder) where T : HtmlElement
        {
            if (finder == null)
            {
                throw new ArgumentNullException("finder");
            }

            var query = finder.BuildElementQuery();

            return finder.Execute(query);
        }

        /// <summary>
        /// Finds the elements by attribute anywhere under this node.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="HtmlElement"/> to return.
        /// </typeparam>
        /// <param name="finder">
        /// The finder.
        /// </param>
        /// <param name="attributeName">
        /// Name of the attribute.
        /// </param>
        /// <param name="attributeValue">
        /// The attribute value.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="finder"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <paramref name="attributeName"/> parameter is <c>null</c>, empty or only contains white-space.
        /// </exception>
        public static IEnumerable<T> AllByAttribute<T>(
            this IHtmlElementFinder<T> finder, 
            string attributeName, 
            string attributeValue) where T : HtmlElement
        {
            return finder.AllByAttribute(attributeName, attributeValue, true);
        }

        /// <summary>
        /// Finds the elements by attribute anywhere under this node.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="HtmlElement"/> to return.
        /// </typeparam>
        /// <param name="finder">
        /// The finder.
        /// </param>
        /// <param name="attributeName">
        /// Name of the attribute.
        /// </param>
        /// <param name="attributeValue">
        /// The attribute value.
        /// </param>
        /// <param name="ignoreCase">
        /// if set to <c>true</c>, the case sensitivity on the attribute value is ignored.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="finder"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <paramref name="attributeName"/> parameter is <c>null</c>, empty or only
        ///     contains white-space.
        /// </exception>
        public static IEnumerable<T> AllByAttribute<T>(
            this IHtmlElementFinder<T> finder, 
            string attributeName, 
            string attributeValue, 
            bool ignoreCase) where T : HtmlElement
        {
            if (finder == null)
            {
                throw new ArgumentNullException("finder");
            }

            if (string.IsNullOrWhiteSpace(attributeName))
            {
                throw new ArgumentException(Resources.Guard_NoValueProvided, "attributeName");
            }

            var tagSelector = finder.BuildElementQuery();
            var attributeQuery = QueryFactory.BuildAttributeQuery(attributeName, attributeValue, ignoreCase);

            return finder.Execute(tagSelector + attributeQuery);
        }

        /// <summary>
        /// Finds the elements by name anywhere under this node.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="HtmlElement"/> to return.
        /// </typeparam>
        /// <param name="finder">
        /// The finder.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="finder"/> parameter is <c>null</c>.
        /// </exception>
        public static IEnumerable<T> AllByName<T>(this IHtmlElementFinder<T> finder, string name) where T : HtmlElement
        {
            return finder.AllByName(name, true);
        }

        /// <summary>
        /// Finds the elements by name anywhere under this node.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="HtmlElement"/> to return.
        /// </typeparam>
        /// <param name="finder">
        /// The finder.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="ignoreCase">
        /// if set to <c>true</c> matching the name value will be case insensitive.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="finder"/> parameter is <c>null</c>.
        /// </exception>
        public static IEnumerable<T> AllByName<T>(this IHtmlElementFinder<T> finder, string name, bool ignoreCase)
            where T : HtmlElement
        {
            if (finder == null)
            {
                throw new ArgumentNullException("finder");
            }

            return finder.AllByAttribute("name", name, ignoreCase);
        }

        /// <summary>
        /// Finds the elements by predicate anywhere under this node.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="HtmlElement"/> to return.
        /// </typeparam>
        /// <param name="finder">
        /// The finder.
        /// </param>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="finder"/> parameter is <c>null</c>.
        /// </exception>
        public static IEnumerable<T> AllByPredicate<T>(this IHtmlElementFinder<T> finder, Func<T, bool> predicate)
            where T : HtmlElement
        {
            if (finder == null)
            {
                throw new ArgumentNullException("finder");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return finder.All().Where(predicate);
        }

        /// <summary>
        /// Finds the elements by their tag name under this node.
        /// </summary>
        /// <typeparam name="T">
        /// The type of element to return.
        /// </typeparam>
        /// <param name="finder">
        /// The finder.
        /// </param>
        /// <param name="tagName">
        /// The tag name.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="finder"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <paramref name="tagName"/> parameter is <c>null</c>, empty or only contains white-space.
        /// </exception>
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", 
            Justification = "HTML references are lower-case by convention.")]
        public static IEnumerable<T> AllByTagName<T>(this IHtmlElementFinder<T> finder, string tagName)
            where T : AnyHtmlElement
        {
            if (finder == null)
            {
                throw new ArgumentNullException("finder");
            }

            if (string.IsNullOrWhiteSpace(tagName))
            {
                throw new ArgumentException(Resources.Guard_NoValueProvided, "tagName");
            }

            // Tag names are already folded to lower case when the HTML was read
            return finder.Execute(".//" + tagName.ToLowerInvariant());
        }

        /// <summary>
        /// Finds the elements by text anywhere under this node.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="HtmlElement"/> to return.
        /// </typeparam>
        /// <param name="finder">
        /// The finder.
        /// </param>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="finder"/> parameter is <c>null</c>.
        /// </exception>
        public static IEnumerable<T> AllByText<T>(this IHtmlElementFinder<T> finder, string text) where T : HtmlElement
        {
            return finder.AllByText(text, true);
        }

        /// <summary>
        /// Finds the elements by text anywhere under this node.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="HtmlElement"/> to return.
        /// </typeparam>
        /// <param name="finder">
        /// The finder.
        /// </param>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="ignoreCase">
        /// if set to <c>true</c> the text comparison will be case insensitive; otherwise a case sensitive
        ///     comparison is made.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="finder"/> parameter is <c>null</c>.
        /// </exception>
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", 
            Justification = "HTML references are lower-case by convention.")]
        public static IEnumerable<T> AllByText<T>(this IHtmlElementFinder<T> finder, string text, bool ignoreCase)
            where T : HtmlElement
        {
            if (finder == null)
            {
                throw new ArgumentNullException("finder");
            }

            if (text == null)
            {
                text = string.Empty;
            }

            var tagSelector = finder.BuildElementQuery();
            string textFilter;

            if (ignoreCase)
            {
                // The text value is a literal value in the query
                // so it can be converted to lower case here rather than within the execution of the XPath query
                textFilter = "[" + QueryFactory.CaseQuery("./text()", true) + " = '" + text.ToLowerInvariant() + "']";
            }
            else
            {
                textFilter = "[./text() = '" + text + "']";
            }

            var query = tagSelector + textFilter;

            return finder.Execute(query);
        }

        /// <summary>
        /// Finds the elements by value anywhere under this node.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="HtmlElement"/> to return.
        /// </typeparam>
        /// <param name="finder">
        /// The finder.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="finder"/> parameter is <c>null</c>.
        /// </exception>
        public static IEnumerable<T> AllByValue<T>(this IHtmlElementFinder<T> finder, string value)
            where T : HtmlElement
        {
            return finder.AllByValue(value, true);
        }

        /// <summary>
        /// Finds the elements by value anywhere under this node.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="HtmlElement"/> to return.
        /// </typeparam>
        /// <param name="finder">
        /// The finder.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="ignoreCase">
        /// if set to <c>true</c> matching the value value will be case insensitive.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="finder"/> parameter is <c>null</c>.
        /// </exception>
        public static IEnumerable<T> AllByValue<T>(this IHtmlElementFinder<T> finder, string value, bool ignoreCase)
            where T : HtmlElement
        {
            if (finder == null)
            {
                throw new ArgumentNullException("finder");
            }

            return finder.AllByAttribute("value", value, ignoreCase);
        }

        /// <summary>
        /// Finds the element by id anywhere under this node.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="HtmlElement"/> to return.
        /// </typeparam>
        /// <param name="finder">
        /// The finder.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="finder"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// More than one element was found.
        /// </exception>
        public static T ById<T>(this IHtmlElementFinder<T> finder, string id) where T : HtmlElement
        {
            return finder.ById(id, true);
        }

        /// <summary>
        /// Finds the element by id anywhere under this node.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="HtmlElement"/> to return.
        /// </typeparam>
        /// <param name="finder">
        /// The finder.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="ignoreCase">
        /// if set to <c>true</c> the match on id will be case insensitive.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="finder"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// No elements were found.
        /// </exception>
        public static T ById<T>(this IHtmlElementFinder<T> finder, string id, bool ignoreCase) where T : HtmlElement
        {
            if (finder == null)
            {
                throw new ArgumentNullException("finder");
            }

            var matches = finder.AllByAttribute("id", id, ignoreCase);

            var failureMessage = string.Format(
                CultureInfo.CurrentCulture, 
                Resources.HtmlElement_MultipleMatchesFoundForId, 
                id);

            return matches.EnsureSingle(failureMessage);
        }

        /// <summary>
        /// Finds the element by name anywhere under this node.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="HtmlElement"/> to return.
        /// </typeparam>
        /// <param name="finder">
        /// The finder.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="finder"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// More than one element was found.
        /// </exception>
        public static T ByName<T>(this IHtmlElementFinder<T> finder, string name) where T : HtmlElement
        {
            return finder.ByName(name, true);
        }

        /// <summary>
        /// Finds the element by name anywhere under this node.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="HtmlElement"/> to return.
        /// </typeparam>
        /// <param name="finder">
        /// The finder.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="ignoreCase">
        /// if set to <c>true</c> matching the name value will be case insensitive.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="finder"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// More than one element was found.
        /// </exception>
        public static T ByName<T>(this IHtmlElementFinder<T> finder, string name, bool ignoreCase) where T : HtmlElement
        {
            if (finder == null)
            {
                throw new ArgumentNullException("finder");
            }

            var matches = finder.AllByName(name, ignoreCase);

            var failureMessage = string.Format(
                CultureInfo.CurrentCulture, 
                Resources.HtmlElement_MultipleMatchesFoundForName, 
                name);

            return matches.EnsureSingle(failureMessage);
        }

        /// <summary>
        /// Finds the element by tag name under this node.
        /// </summary>
        /// <typeparam name="T">
        /// The type of element to return.
        /// </typeparam>
        /// <param name="finder">
        /// The finder.
        /// </param>
        /// <param name="tagName">
        /// The tag name.
        /// </param>
        /// <returns>
        /// An <typeparamref name="T"/> value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="finder"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <paramref name="tagName"/> parameter is <c>null</c>, empty or only contains white-space.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// More than one element was found.
        /// </exception>
        public static T ByTagName<T>(this IHtmlElementFinder<T> finder, string tagName) where T : AnyHtmlElement
        {
            if (finder == null)
            {
                throw new ArgumentNullException("finder");
            }

            var matches = finder.AllByTagName(tagName);

            return matches.EnsureSingle();
        }

        /// <summary>
        /// Finds the element by name anywhere under this node.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="HtmlElement"/> to return.
        /// </typeparam>
        /// <param name="finder">
        /// The finder.
        /// </param>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="finder"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// More than one element was found.
        /// </exception>
        public static T ByText<T>(this IHtmlElementFinder<T> finder, string text) where T : HtmlElement
        {
            return finder.ByText(text, true);
        }

        /// <summary>
        /// Finds the element by name anywhere under this node.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="HtmlElement"/> to return.
        /// </typeparam>
        /// <param name="finder">
        /// The finder.
        /// </param>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="ignoreCase">
        /// if set to <c>true</c> matching the name value will be case insensitive.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="finder"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// More than one element was found.
        /// </exception>
        public static T ByText<T>(this IHtmlElementFinder<T> finder, string text, bool ignoreCase) where T : HtmlElement
        {
            if (finder == null)
            {
                throw new ArgumentNullException("finder");
            }

            var matches = finder.AllByText(text, ignoreCase);

            var failureMessage = string.Format(
                CultureInfo.CurrentCulture, 
                Resources.HtmlElement_MultipleMatchesFoundForText, 
                text);

            return matches.EnsureSingle(failureMessage);
        }

        /// <summary>
        /// Finds the element by value anywhere under this node.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="HtmlElement"/> to return.
        /// </typeparam>
        /// <param name="finder">
        /// The finder.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="finder"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// More than one element was found.
        /// </exception>
        public static T ByValue<T>(this IHtmlElementFinder<T> finder, string value) where T : HtmlElement
        {
            return finder.ByValue(value, true);
        }

        /// <summary>
        /// Finds the element by value anywhere under this node.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="HtmlElement"/> to return.
        /// </typeparam>
        /// <param name="finder">
        /// The finder.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="ignoreCase">
        /// if set to <c>true</c> the match on value will be case insensitive.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="finder"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// No elements were found.
        /// </exception>
        public static T ByValue<T>(this IHtmlElementFinder<T> finder, string value, bool ignoreCase)
            where T : HtmlElement
        {
            if (finder == null)
            {
                throw new ArgumentNullException("finder");
            }

            var matches = finder.AllByAttribute("value", value, ignoreCase);

            var failureMessage = string.Format(
                CultureInfo.CurrentCulture, 
                Resources.HtmlElement_MultipleMatchesFoundForValue, 
                value);

            return matches.EnsureSingle(failureMessage);
        }
    }
}