namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    ///     The <see cref="TypeExtensions" />
    ///     class provides extension methods for the <see cref="Type" /> class.
    /// </summary>
    internal static class TypeExtensions
    {
        /// <summary>
        ///     Stores the matching type cache.
        /// </summary>
        private static readonly IDictionary<string, IReadOnlyCollection<Type>> _matchingTypesCache =
            new Dictionary<string, IReadOnlyCollection<Type>>();

        /// <summary>
        ///     Stores the supported tag name cache.
        /// </summary>
        private static readonly IDictionary<string, IReadOnlyCollection<string>> _supportedTagCache =
            new Dictionary<string, IReadOnlyCollection<string>>();

        /// <summary>
        ///     Stores the tag name cache sync lock.
        /// </summary>
        private static readonly object _tagSyncLock = new object();

        /// <summary>
        ///     Stores the type cache sync lock.
        /// </summary>
        private static readonly object _typeSyncLock = new object();

        /// <summary>
        /// Gets the matching types.
        /// </summary>
        /// <param name="elementType">
        /// Type of the element.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> value.
        /// </returns>
        public static IEnumerable<Type> GetMatchingTypes(Type elementType)
        {
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
                    assemblies.SelectMany(assembly => assembly.GetTypes().Where(elementType.IsAssignableFrom));

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
        public static IReadOnlyCollection<string> GetSupportedTags(this Type elementType)
        {
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

                var supportedTags = new ReadOnlyCollection<string>(tags);

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
        private static IList<string> FindSupportedTags(IEnumerable<Type> types)
        {
            var tagsFound = new List<string>();

            foreach (var type in types)
            {
                var attribute = type.GetCustomAttribute<SupportedTagAttribute>();

                if (tagsFound.Contains(attribute.TagName))
                {
                    continue;
                }

                tagsFound.Add(attribute.TagName);
            }

            return tagsFound;
        }
    }
}