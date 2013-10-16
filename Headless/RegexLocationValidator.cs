namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Headless.Properties;

    /// <summary>
    ///     The <see cref="RegexLocationValidator" />
    ///     class validates page locations based on regular expressions.
    /// </summary>
    public class RegexLocationValidator : ILocationValidator
    {
        /// <inheritdoc />
        /// <exception cref="NotImplementedException">This method is not implemented.</exception>
        public bool Matches(Uri location, Uri expectedLocation)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">The <paramref name="location" /> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentException">
        ///     The <paramref name="location" /> is a relative location where an absolute location is required.
        /// </exception>
        /// <exception cref="ArgumentNullException">The <paramref name="matchingExpressions" /> is <c>null</c>.</exception>
        public bool Matches(Uri location, IEnumerable<Regex> matchingExpressions)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location");
            }

            if (location.IsAbsoluteUri == false)
            {
                throw new ArgumentException(Resources.Uri_LocationMustBeAbsolute, "location");
            }

            if (matchingExpressions == null)
            {
                throw new ArgumentNullException("matchingExpressions");
            }

            return matchingExpressions.Any(x => x.IsMatch(location.ToString()));
        }

        /// <inheritdoc />
        public LocationValidationType ValidationType
        {
            get
            {
                return LocationValidationType.RegexOnly;
            }
        }
    }
}