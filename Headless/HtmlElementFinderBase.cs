namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using Headless.Properties;

    /// <summary>
    /// The <see cref="HtmlElementFinderBase{T}"/>
    ///     class provides a base set of <see cref="HtmlElement"/> finding logic.
    /// </summary>
    /// <typeparam name="T">
    /// The type of element being searched for.
    /// </typeparam>
    public abstract class HtmlElementFinderBase<T> : IHtmlElementFinder<T> where T : HtmlElement
    {
        /// <summary>
        ///     The default query axes.
        /// </summary>
        protected const string DefaultQueryAxes = "descendant";

        /// <inheritdoc />
        public IEnumerable<T> All()
        {
            var query = BuildElementQuery();

            return Execute(query);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentException">
        ///     The <paramref name="attributeName" /> parameter is <c>null</c>, empty or only contains white-space.
        /// </exception>
        public IEnumerable<T> AllByAttribute(string attributeName, string attributeValue)
        {
            return AllByAttribute(attributeName, attributeValue, true);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentException">
        ///     The <paramref name="attributeName" /> parameter is <c>null</c>, empty or only
        ///     contains white-space.
        /// </exception>
        public IEnumerable<T> AllByAttribute(string attributeName, string attributeValue, bool ignoreCase)
        {
            if (string.IsNullOrWhiteSpace(attributeName))
            {
                throw new ArgumentException(Resources.Guard_NoValueProvided, "attributeName");
            }

            var tagSelector = BuildElementQuery();
            var attributeQuery = QueryFactory.BuildAttributeQuery(attributeName, attributeValue, ignoreCase);

            return Execute(tagSelector + attributeQuery);
        }

        /// <inheritdoc />
        public IEnumerable<T> AllByName(string name)
        {
            return AllByName(name, true);
        }

        /// <inheritdoc />
        public IEnumerable<T> AllByName(string name, bool ignoreCase)
        {
            return AllByAttribute("name", name, ignoreCase);
        }

        /// <inheritdoc />
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="predicate" /> parameter is <c>null</c>.
        /// </exception>
        public IEnumerable<T> AllByPredicate(Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return All().Where(predicate);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentException">
        ///     The <paramref name="tagName" /> parameter is <c>null</c>, empty or only contains white-space.
        /// </exception>
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", 
            Justification = "HTML references are lower-case by convention.")]
        public IEnumerable<T> AllByTagName(string tagName)
        {
            if (string.IsNullOrWhiteSpace(tagName))
            {
                throw new ArgumentException(Resources.Guard_NoValueProvided, "tagName");
            }

            var axes = QueryAxes();

            if (string.IsNullOrEmpty(axes))
            {
                axes = DefaultQueryAxes;
            }

            // Tag names are already folded to lower case when the HTML was read
            return Execute("./" + axes + "::*[self::*[local-name() = '" + tagName.ToLowerInvariant() + "']]");
        }

        /// <inheritdoc />
        public IEnumerable<T> AllByText(string text)
        {
            return AllByText(text, true);
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", 
            Justification = "HTML references are lower-case by convention.")]
        public IEnumerable<T> AllByText(string text, bool ignoreCase)
        {
            if (text == null)
            {
                text = string.Empty;
            }

            var tagSelector = BuildElementQuery();
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

            return Execute(query);
        }

        /// <inheritdoc />
        public IEnumerable<T> AllByValue(string value)
        {
            return AllByValue(value, true);
        }

        /// <inheritdoc />
        public IEnumerable<T> AllByValue(string value, bool ignoreCase)
        {
            return AllByAttribute("value", value, ignoreCase);
        }

        /// <inheritdoc />
        public abstract string BuildElementQuery();

        /// <inheritdoc />
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     More than one element was found.
        /// </exception>
        public T ByAttribute(string attributeName, string attributeValue)
        {
            return ByAttribute(attributeName, attributeValue, true);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     More than one element was found.
        /// </exception>
        public T ByAttribute(string attributeName, string attributeValue, bool ignoreCase)
        {
            var matches = AllByAttribute(attributeName, attributeValue, ignoreCase);

            var failureMessage = string.Format(
                CultureInfo.CurrentCulture, 
                Resources.HtmlElement_MultipleMatchesFoundForAttribute, 
                attributeName);

            return matches.EnsureSingle(failureMessage);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     More than one element was found.
        /// </exception>
        public T ById(string id)
        {
            return ById(id, true);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     No elements were found.
        /// </exception>
        public T ById(string id, bool ignoreCase)
        {
            var matches = AllByAttribute("id", id, ignoreCase);

            var failureMessage = string.Format(
                CultureInfo.CurrentCulture, 
                Resources.HtmlElement_MultipleMatchesFoundForId, 
                id);

            return matches.EnsureSingle(failureMessage);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     More than one element was found.
        /// </exception>
        public T ByName(string name)
        {
            return ByName(name, true);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     More than one element was found.
        /// </exception>
        public T ByName(string name, bool ignoreCase)
        {
            var matches = AllByName(name, ignoreCase);

            var failureMessage = string.Format(
                CultureInfo.CurrentCulture, 
                Resources.HtmlElement_MultipleMatchesFoundForName, 
                name);

            return matches.EnsureSingle(failureMessage);
        }

        /// <inheritdoc />
        /// <exception cref="System.ArgumentNullException">
        ///     finder
        ///     or
        ///     predicate
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     The <paramref name="predicate" /> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     More than one element was found.
        /// </exception>
        public T ByPredicate(Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            var matches = AllByPredicate(predicate);

            return matches.EnsureSingle();
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentException">
        ///     The <paramref name="tagName" /> parameter is <c>null</c>, empty or only contains white-space.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     More than one element was found.
        /// </exception>
        public T ByTagName(string tagName)
        {
            var matches = AllByTagName(tagName);

            return matches.EnsureSingle();
        }

        /// <inheritdoc />
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     More than one element was found.
        /// </exception>
        public T ByText(string text)
        {
            return ByText(text, true);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     More than one element was found.
        /// </exception>
        public T ByText(string text, bool ignoreCase)
        {
            var matches = AllByText(text, ignoreCase);

            var failureMessage = string.Format(
                CultureInfo.CurrentCulture, 
                Resources.HtmlElement_MultipleMatchesFoundForText, 
                text);

            return matches.EnsureSingle(failureMessage);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     More than one element was found.
        /// </exception>
        public T ByValue(string value)
        {
            return ByValue(value, true);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     No elements were found.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     No elements were found.
        /// </exception>
        public T ByValue(string value, bool ignoreCase)
        {
            var matches = AllByAttribute("value", value, ignoreCase);

            var failureMessage = string.Format(
                CultureInfo.CurrentCulture, 
                Resources.HtmlElement_MultipleMatchesFoundForValue, 
                value);

            return matches.EnsureSingle(failureMessage);
        }

        /// <inheritdoc />
        public abstract IEnumerable<T> Execute(string query);

        /// <inheritdoc />
        public T Singular()
        {
            return All().EnsureSingle();
        }

        /// <summary>
        ///     Gets the axes for the xpath query.
        /// </summary>
        /// <returns>A <see cref="string" /> value.</returns>
        protected virtual string QueryAxes()
        {
            return DefaultQueryAxes;
        }
    }
}