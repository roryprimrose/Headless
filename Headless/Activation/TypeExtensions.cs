namespace Headless.Activation
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
        private static readonly IDictionary<string, IReadOnlyCollection<Type>> _matchingTypesCache =
            new Dictionary<string, IReadOnlyCollection<Type>>();

        /// <summary>
        ///     Stores the supported tag name cache.
        /// </summary>
        private static readonly IDictionary<string, IReadOnlyCollection<SupportedTagAttribute>> _supportedTagCache =
            new Dictionary<string, IReadOnlyCollection<SupportedTagAttribute>>();

        /// <summary>
        ///     Stores the tag name cache sync lock.
        /// </summary>
        private static readonly object _tagSyncLock = new object();

        /// <summary>
        ///     Stores the type cache sync lock.
        /// </summary>
        private static readonly object _typeSyncLock = new object();

        /// <summary>
        /// The find best matching type.
        /// </summary>
        /// <param name="elementType">
        /// The element type.
        /// </param>
        /// <param name="node">
        /// The related node.
        /// </param>
        /// <returns>
        /// A <see cref="Type"/> value.
        /// </returns>
        /// <exception cref="InvalidHtmlElementMatchException">
        /// No type could be found to match the node.
        /// </exception>
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
            var nodeName = navigator.Name;

            foreach (var possibleType in possibleTypes)
            {
                var attributes = possibleType.GetCustomAttributes<SupportedTagAttribute>();

                foreach (var attribute in attributes)
                {
                    if (nodeName != attribute.TagName)
                    {
                        continue;
                    }

                    if (attribute.HasAttributeFilter)
                    {
                        var matchingAttribute = navigator.GetAttribute(attribute.AttributeName, string.Empty);
                        
                        if (matchingAttribute == attribute.AttributeValue)
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
        /// Gets the matching types.
        /// </summary>
        /// <param name="elementType">
        /// Type of the element.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        public static IReadOnlyCollection<Type> GetMatchingTypes(this Type elementType)
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
                            assembly.GetTypes().Where(x => elementType.IsAssignableFrom(x) && x.IsAbstract == false));

                var types = new ReadOnlyCollection<Type>(matchingTypes.ToList());

                _matchingTypesCache.Add(typeName, types);

                return types;
            }
        }

        /// <summary>
        /// Gets the supported tags.
        /// </summary>
        /// <param name="elementType">
        /// Type of the element.
        /// </param>
        /// <returns>
        /// An <see cref="IReadOnlyCollection{T}"/> value.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// The type does not indicate any supported tags.
        /// </exception>
        public static IReadOnlyCollection<SupportedTagAttribute> GetSupportedTags(this Type elementType)
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
        /// Finds the supported tags.
        /// </summary>
        /// <param name="types">
        /// The types.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        private static IList<SupportedTagAttribute> FindSupportedTags(IEnumerable<Type> types)
        {
            var tagsFound = new List<SupportedTagAttribute>();
            var comparer = new SupportedTagAttributeComparer();

            foreach (var type in types)
            {
                var attributes = type.GetCustomAttributes<SupportedTagAttribute>();

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