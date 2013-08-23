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
        public static IEnumerable<T> ByAttribute<T>(
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
        public static IEnumerable<T> ByName<T>(this IHtmlElementFinder<T> finder, string name) where T : HtmlElement
        {
            if (finder == null)
            {
                throw new ArgumentNullException("finder");
            }

            return finder.ByAttribute("name", name);
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
        public static IEnumerable<T> ByPredicate<T>(this IHtmlElementFinder<T> finder, Func<T, bool> predicate)
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
        public static IEnumerable<T> ByTagName<T>(this IHtmlElementFinder<T> finder, string tagName)
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

            return finder.Execute("//" + tagName);
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
        public static IEnumerable<T> ByText<T>(this IHtmlElementFinder<T> finder, string text) where T : HtmlElement
        {
            if (finder == null)
            {
                throw new ArgumentNullException("finder");
            }

            var tagSelector = finder.BuildElementQuery();
            var query = tagSelector + "[./text() = '" + text + "']";

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
        public static T FindById<T>(this IHtmlElementFinder<T> finder, string id) where T : HtmlElement
        {
            if (finder == null)
            {
                throw new ArgumentNullException("finder");
            }

            var matches = finder.ByAttribute("id", id);

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
        public static T FindByName<T>(this IHtmlElementFinder<T> finder, string name) where T : HtmlElement
        {
            if (finder == null)
            {
                throw new ArgumentNullException("finder");
            }

            var matches = finder.ByAttribute("name", name);

            return matches.EnsureSingle();
        }
    }
}