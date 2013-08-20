namespace Headless
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="IHtmlElementFinder{T}"/>
    ///     interface defines the members for finding <see cref="HtmlElement"/>
    ///     instances within an <see cref="IHtmlPage"/> instance.
    /// </summary>
    /// <typeparam name="T">
    /// The type of <see cref="HtmlElement"/> to find.
    /// </typeparam>
    public interface IHtmlElementFinder<out T> where T : HtmlElement
    {
        /// <summary>
        /// Builds the tag name xpath selector.
        /// </summary>
        /// <param name="elementType">
        /// Type of the element.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> value.
        /// </returns>
        string BuildElementQuery(Type elementType);

        /// <summary>
        /// Builds the element results.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        IEnumerable<T> Execute(string query);
    }
}