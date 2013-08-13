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
        ///     Stores the cache sync lock.
        /// </summary>
        private static readonly object _cacheSyncLock = new object();

        /// <summary>
        ///     Stores the supported tag cache.
        /// </summary>
        private static readonly IDictionary<string, IReadOnlyCollection<string>> _supportedTagCache =
            new Dictionary<string, IReadOnlyCollection<string>>();

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
            var typeName = elementType.FullName;

            if (_supportedTagCache.ContainsKey(typeName))
            {
                return _supportedTagCache[typeName];
            }

            lock (_cacheSyncLock)
            {
                if (_supportedTagCache.ContainsKey(typeName))
                {
                    return _supportedTagCache[typeName];
                }

                var attributes = elementType.GetCustomAttributes<SupportedTagAttribute>();
                var tags = new List<string>(attributes.Select(x => x.TagName));

                if (tags.Count == 0)
                {
                    throw new InvalidOperationException(
                        "The type " + typeName + " does not indicate any supported tags.");
                }

                var supportedTags = new ReadOnlyCollection<string>(tags);

                _supportedTagCache.Add(typeName, supportedTags);

                return tags;
            }
        }
    }
}