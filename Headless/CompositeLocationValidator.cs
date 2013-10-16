namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Headless.Properties;

    /// <summary>
    /// The <see cref="ComponentsLocationValidator"/>
    /// class uses both Uri and Regex validation of page locations.
    /// </summary>
    public class CompositeLocationValidator : RelaxedFolderLocationValidator
    {
        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">The <paramref name="location" /> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentException">
        ///     The <paramref name="location" /> is a relative location where an absolute location is required.
        /// </exception>
        /// <exception cref="ArgumentNullException">The <paramref name="matchingExpressions" /> is <c>null</c>.</exception>
        public override bool Matches(Uri location, IEnumerable<Regex> matchingExpressions)
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
        public override LocationValidationType ValidationType
        {
            get
            {
                return LocationValidationType.All;
            }
        }
    }
}