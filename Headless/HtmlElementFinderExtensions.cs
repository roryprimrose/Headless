namespace Headless
{
    using System;
    using System.Collections.Generic;
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
        public static IEnumerable<T> AllByAttribute<T>(
            this IHtmlElementFinder<T> finder, 
            string attributeName, 
            string attributeValue) where T : HtmlElement
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
            var query = tagSelector + "[@" + attributeName + "='" + attributeValue + "']";

            return finder.Execute(query);
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
            if (finder == null)
            {
                throw new ArgumentNullException("finder");
            }

            return finder.AllByAttribute("name", name);
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

            return finder.Execute(".//" + tagName);
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
        /// <exception cref="System.ArgumentNullException">
        /// finder
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="finder"/> parameter is <c>null</c>.
        /// </exception>
        public static IEnumerable<T> AllByText<T>(this IHtmlElementFinder<T> finder, string text, bool ignoreCase)
            where T : HtmlElement
        {
            if (finder == null)
            {
                throw new ArgumentNullException("finder");
            }

            var tagSelector = finder.BuildElementQuery();
            string textFilter;

            if (ignoreCase)
            {
                textFilter = "[translate(./text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz') = translate('" +
                             text + "', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')]";
            }
            else
            {
                textFilter = "[./text() = '" + text + "']";
            }

            var query = tagSelector + textFilter;

            return finder.Execute(query);
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
            if (finder == null)
            {
                throw new ArgumentNullException("finder");
            }

            var matches = finder.AllByAttribute("id", id);

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
            if (finder == null)
            {
                throw new ArgumentNullException("finder");
            }

            var matches = finder.AllByAttribute("name", name);

            return matches.EnsureSingle();
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
    }
}