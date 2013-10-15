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
        public bool Matches(Uri actualLocation, Uri expectedLocation)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">The <paramref name="actualLocation" /> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentException">
        ///     The <paramref name="actualLocation" /> is a relative location where an absolute location is required.
        /// </exception>
        /// <exception cref="ArgumentNullException">The <paramref name="matchingExpressions" /> is <c>null</c>.</exception>
        public bool Matches(Uri actualLocation, IEnumerable<Regex> matchingExpressions)
        {
            if (actualLocation == null)
            {
                throw new ArgumentNullException("actualLocation");
            }

            if (actualLocation.IsAbsoluteUri == false)
            {
                throw new ArgumentException(Resources.Uri_LocationMustBeAbsolute, "actualLocation");
            }

            if (matchingExpressions == null)
            {
                throw new ArgumentNullException("matchingExpressions");
            }

            return matchingExpressions.Any(x => x.IsMatch(actualLocation.ToString()));
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