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
        ///     Finds all the elements of the requested type anywhere under this node.
        /// </summary>
        /// <returns>
        ///     An <see cref="IEnumerable{T}" /> value.
        /// </returns>
        IEnumerable<T> All();

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
        IEnumerable<T> AllByAttribute(string attributeName, string attributeValue);

        /// <summary>
        /// Finds the elements by attribute anywhere under this node.
        /// </summary>
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
        IEnumerable<T> AllByAttribute(string attributeName, string attributeValue, bool ignoreCase);

        /// <summary>
        /// Finds the elements by name anywhere under this node.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        IEnumerable<T> AllByName(string name);

        /// <summary>
        /// Finds the elements by name anywhere under this node.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="ignoreCase">
        /// if set to <c>true</c> matching the name value will be case insensitive.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        IEnumerable<T> AllByName(string name, bool ignoreCase);

        /// <summary>
        /// Finds the elements by predicate anywhere under this node.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        IEnumerable<T> AllByPredicate(Func<T, bool> predicate);

        /// <summary>
        /// Finds the elements by their tag name under this node.
        /// </summary>
        /// <param name="tagName">
        /// The tag name.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        IEnumerable<T> AllByTagName(string tagName);

        /// <summary>
        /// Finds the elements by text anywhere under this node.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        IEnumerable<T> AllByText(string text);

        /// <summary>
        /// Finds the elements by text anywhere under this node.
        /// </summary>
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
        IEnumerable<T> AllByText(string text, bool ignoreCase);

        /// <summary>
        /// Finds the elements by value anywhere under this node.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        IEnumerable<T> AllByValue(string value);

        /// <summary>
        /// Finds the elements by value anywhere under this node.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="ignoreCase">
        /// if set to <c>true</c> matching the value value will be case insensitive.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        IEnumerable<T> AllByValue(string value, bool ignoreCase);

        /// <summary>
        ///     Builds the tag name xpath selector.
        /// </summary>
        /// <returns>
        ///     A <see cref="string" /> value.
        /// </returns>
        string BuildElementQuery();

        /// <summary>
        /// Finds the element by attribute anywhere under this node.
        /// </summary>
        /// <param name="attributeName">
        /// The attribute name.
        /// </param>
        /// <param name="attributeValue">
        /// The attribute value.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        T ByAttribute(string attributeName, string attributeValue);

        /// <summary>
        /// Finds the element by attribute anywhere under this node.
        /// </summary>
        /// <param name="attributeName">
        /// The attribute name.
        /// </param>
        /// <param name="attributeValue">
        /// The attribute value.
        /// </param>
        /// <param name="ignoreCase">
        /// if set to <c>true</c> matching the name value will be case insensitive.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        T ByAttribute(string attributeName, string attributeValue, bool ignoreCase);

        /// <summary>
        /// Finds the element by id anywhere under this node.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        T ById(string id);

        /// <summary>
        /// Finds the element by id anywhere under this node.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="ignoreCase">
        /// if set to <c>true</c> the match on id will be case insensitive.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        T ById(string id, bool ignoreCase);

        /// <summary>
        /// Finds the element by name anywhere under this node.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        T ByName(string name);

        /// <summary>
        /// Finds the element by name anywhere under this node.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="ignoreCase">
        /// if set to <c>true</c> matching the name value will be case insensitive.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        T ByName(string name, bool ignoreCase);

        /// <summary>
        /// Finds an element by predicate anywhere under this node.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <returns>
        /// An <typeparamref name="T"/> value.
        /// </returns>
        T ByPredicate(Func<T, bool> predicate);

        /// <summary>
        /// Finds the element by tag name under this node.
        /// </summary>
        /// <param name="tagName">
        /// The tag name.
        /// </param>
        /// <returns>
        /// An <typeparamref name="T"/> value.
        /// </returns>
        T ByTagName(string tagName);

        /// <summary>
        /// Finds the element by name anywhere under this node.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        T ByText(string text);

        /// <summary>
        /// Finds the element by name anywhere under this node.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="ignoreCase">
        /// if set to <c>true</c> matching the name value will be case insensitive.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        T ByText(string text, bool ignoreCase);

        /// <summary>
        /// Finds the element by value anywhere under this node.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        T ByValue(string value);

        /// <summary>
        /// Finds the element by value anywhere under this node.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="ignoreCase">
        /// if set to <c>true</c> the match on value will be case insensitive.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        T ByValue(string value, bool ignoreCase);

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