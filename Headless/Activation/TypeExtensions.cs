﻿namespace Headless.Activation
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Xml.XPath;
    using Headless.Properties;

    /// <summary>
    ///     The <see cref="TypeExtensions" />
    ///     class provides extension methods for the <see cref="Type" /> class.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        ///     Stores the matching type cache.
        /// </summary>
        private static readonly IDictionary<string, ReadOnlyCollection<Type>> _matchingTypesCache =
            new Dictionary<string, ReadOnlyCollection<Type>>();

        /// <summary>
        ///     Stores the supported tag name cache.
        /// </summary>
        private static readonly IDictionary<string, ReadOnlyCollection<SupportedTagAttribute>> _supportedTagCache =
            new Dictionary<string, ReadOnlyCollection<SupportedTagAttribute>>();

        /// <summary>
        ///     Stores the tag name cache sync lock.
        /// </summary>
        private static readonly object _tagSyncLock = new object();

        /// <summary>
        ///     Stores the type cache sync lock.
        /// </summary>
        private static readonly object _typeSyncLock = new object();

        /// <summary>
        ///     The find best matching type.
        /// </summary>
        /// <param name="elementType">
        ///     The element type.
        /// </param>
        /// <param name="node">
        ///     The related node.
        /// </param>
        /// <returns>
        ///     A <see cref="Type" /> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="elementType" /> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="node" /> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidHtmlElementMatchException">
        ///     No type could be found to match the node.
        /// </exception>
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase",
            Justification = "HTML references are lower-case by convention.")]
        public static Type FindBestMatchingType(this Type elementType, IXPathNavigable node)
        {
            if (elementType == null)
            {
                throw new ArgumentNullException("elementType");
            }

            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            var navigator = node.GetNavigator();
            var possibleTypes = GetMatchingTypes(elementType).ToList();
            var matchingTypes = new List<Type>();

            // The node name should already be folded to lower case when the HTML was read
            var nodeName = navigator.Name;

            foreach (var possibleType in possibleTypes)
            {
                var attributes =
                    possibleType.GetCustomAttributes(typeof(SupportedTagAttribute), true)
                        .OfType<SupportedTagAttribute>();

                foreach (var attribute in attributes)
                {
                    if (nodeName.Equals(attribute.TagName, StringComparison.OrdinalIgnoreCase) == false)
                    {
                        continue;
                    }

                    if (attribute.HasAttributeFilter)
                    {
                        // The attribute name should already be folded to lower case when the HTML was read
                        var queryAttributeName = attribute.AttributeName.ToLowerInvariant();

                        var matchingAttribute = navigator.GetAttribute(queryAttributeName, string.Empty);

                        if (matchingAttribute.Equals(attribute.AttributeValue, StringComparison.OrdinalIgnoreCase))
                        {
                            matchingTypes.Add(possibleType);
                        }
                    }
                    else
                    {
                        matchingTypes.Add(possibleType);
                    }
                }
            }

            if (matchingTypes.Count == 0)
            {
                return typeof(AnyHtmlElement);
            }

            if (matchingTypes.Count > 1)
            {
                var matchingTypeNames = matchingTypes.Aggregate(string.Empty, (x, y) => x + Environment.NewLine + y);
                var message = string.Format(
                    CultureInfo.CurrentCulture,
                    Resources.TypeExtensions_MultipleTypeMatchesForNode,
                    elementType.FullName,
                    navigator.OuterXml,
                    matchingTypeNames);

                throw new InvalidHtmlElementMatchException(message);
            }

            return matchingTypes[0];
        }

        /// <summary>
        ///     Gets the loadable types.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>The available types in the specified assembly.</returns>
        /// <exception cref="System.ArgumentNullException">assembly</exception>
        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }

            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }

        /// <summary>
        ///     Gets the matching types.
        /// </summary>
        /// <param name="elementType">
        ///     Type of the element.
        /// </param>
        /// <returns>
        ///     An <see cref="IEnumerable{T}" /> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="elementType" /> parameter is <c>null</c>.
        /// </exception>
        public static ReadOnlyCollection<Type> GetMatchingTypes(this Type elementType)
        {
            if (elementType == null)
            {
                throw new ArgumentNullException("elementType");
            }

            var typeName = elementType.AssemblyQualifiedName;

            if (_matchingTypesCache.ContainsKey(typeName))
            {
                return _matchingTypesCache[typeName];
            }

            lock (_typeSyncLock)
            {
                if (_matchingTypesCache.ContainsKey(typeName))
                {
                    return _matchingTypesCache[typeName];
                }

                var assemblies = AppDomain.CurrentDomain.GetAssemblies();

                var matchingTypes =
                    assemblies.SelectMany(
                        assembly =>
                            assembly.GetLoadableTypes()
                                .Where(x => elementType.IsAssignableFrom(x) && x.IsAbstract == false));

                var types = new ReadOnlyCollection<Type>(matchingTypes.ToList());

                _matchingTypesCache.Add(typeName, types);

                return types;
            }
        }

        /// <summary>
        ///     Gets the supported tags.
        /// </summary>
        /// <param name="elementType">
        ///     Type of the element.
        /// </param>
        /// <returns>
        ///     An <see cref="ReadOnlyCollection{T}" /> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="elementType" /> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        ///     The type does not indicate any supported tags.
        /// </exception>
        public static ReadOnlyCollection<SupportedTagAttribute> GetSupportedTags(this Type elementType)
        {
            if (elementType == null)
            {
                throw new ArgumentNullException("elementType");
            }

            var typeName = elementType.AssemblyQualifiedName;

            if (_supportedTagCache.ContainsKey(typeName))
            {
                return _supportedTagCache[typeName];
            }

            lock (_tagSyncLock)
            {
                if (_supportedTagCache.ContainsKey(typeName))
                {
                    return _supportedTagCache[typeName];
                }

                var matchingTypes = GetMatchingTypes(elementType);
                var tags = FindSupportedTags(matchingTypes);

                if (tags.Count == 0)
                {
                    throw new InvalidOperationException(
                        "The type " + typeName + " does not indicate any supported tags.");
                }

                var supportedTags = new ReadOnlyCollection<SupportedTagAttribute>(tags);

                _supportedTagCache.Add(typeName, supportedTags);

                return supportedTags;
            }
        }

        /// <summary>
        ///     Finds the supported tags.
        /// </summary>
        /// <param name="types">
        ///     The types.
        /// </param>
        /// <returns>
        ///     An <see cref="IEnumerable{T}" /> value.
        /// </returns>
        private static IList<SupportedTagAttribute> FindSupportedTags(IEnumerable<Type> types)
        {
            var tagsFound = new List<SupportedTagAttribute>();
            var comparer = new SupportedTagAttributeComparer();

            foreach (var type in types)
            {
                var attributes =
                    type.GetCustomAttributes(typeof(SupportedTagAttribute), true).OfType<SupportedTagAttribute>();

                foreach (var attribute in attributes)
                {
                    if (tagsFound.Contains(attribute, comparer))
                    {
                        continue;
                    }

                    tagsFound.Add(attribute);
                }
            }

            return tagsFound;
        }
    }
}