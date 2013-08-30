namespace Headless
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Headless.Properties;

    /// <summary>
    ///     The <see cref="QueryFactory" />
    ///     class provides methods for building XPath queries.
    /// </summary>
    public static class QueryFactory
    {
        /// <summary>
        /// Builds an XPath query for the specified attribute name and value.
        /// </summary>
        /// <param name="attributeName">
        /// Name of the attribute.
        /// </param>
        /// <param name="attributeValue">
        /// The attribute value.
        /// </param>
        /// <param name="ignoreCase">
        /// if set to <c>true</c> the case of the attribute value will be ignored.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> value.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// The <paramref name="attributeName"/> parameter is <c>null</c>, empty or only contains white-space.
        /// </exception>
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", 
            Justification = "HTML references are lower-case by convention.")]
        public static string BuildAttributeQuery(string attributeName, string attributeValue, bool ignoreCase)
        {
            if (string.IsNullOrWhiteSpace(attributeName))
            {
                throw new ArgumentException(Resources.Guard_NoValueProvided, "attributeName");
            }

            if (attributeValue == null)
            {
                attributeValue = string.Empty;
            }

            string attributeQuery;

            // Attribute names are folded to lowercase when the HTML is read
            var queryAttributeName = attributeName.ToLowerInvariant();

            if (ignoreCase)
            {
                // This literal value can be converted to lower case here rather than within the execution of the XPath query
                var queryAttributeValue = attributeValue.ToLowerInvariant();

                // The query execution will push the value of the node being checked to lowercase for the
                // specified attribute name
                attributeQuery = "[" + CaseQuery("@" + queryAttributeName, true) + "='" + queryAttributeValue + "']";
            }
            else
            {
                attributeQuery = "[@" + queryAttributeName + "='" + attributeValue + "']";
            }

            return attributeQuery;
        }

        /// <summary>
        /// Returns the query as either a case sensitive or case insensitive query.
        /// </summary>
        /// <param name="queryPart">
        /// The part of the query to process.
        /// </param>
        /// <param name="ignoreCase">
        /// if set to <c>true</c> the query will ignore case.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> value.
        /// </returns>
        public static string CaseQuery(string queryPart, bool ignoreCase)
        {
            if (ignoreCase)
            {
                return "translate(" + queryPart + ", 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')";
            }

            return queryPart;
        }
    }
}