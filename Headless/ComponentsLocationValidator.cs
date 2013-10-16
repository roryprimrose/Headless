namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Headless.Properties;

    /// <summary>
    ///     The <see cref="ComponentsLocationValidator" />
    ///     class uses <see cref="Uri.GetComponents" /> to validate <see cref="Uri" /> locations.
    /// </summary>
    public abstract class ComponentsLocationValidator : ILocationValidator
    {
        /// <inheritdoc />
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="expectedLocation" /> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        ///     The <paramref name="expectedLocation" /> is a relative location where an absolute location is required.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="location" /> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        ///     The <paramref name="location" /> is a relative location where an absolute location is required.
        /// </exception>
        public virtual bool Matches(Uri location, Uri expectedLocation)
        {
            ValidateParameters(expectedLocation, location);

            var compareWith = VerificationParts;

            StringComparison comparisonType;

            if (CaseSensitive)
            {
                comparisonType = StringComparison.Ordinal;
            }
            else
            {
                comparisonType = StringComparison.OrdinalIgnoreCase;
            }

            var compareValue = string.Compare(
                expectedLocation.GetComponents(compareWith, CompareFormat), 
                location.GetComponents(compareWith, CompareFormat), 
                comparisonType);

            if (compareValue == 0)
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        /// <exception cref="NotImplementedException">This method is not implemented.</exception>
        public virtual bool Matches(Uri location, IEnumerable<Regex> matchingExpressions)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Validates that appropriate parameters are supplied.
        /// </summary>
        /// <param name="expectedLocation">
        /// The expected location.
        /// </param>
        /// <param name="actualLocation">
        /// The actual location.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="expectedLocation"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// The <paramref name="expectedLocation"/> is a relative location where an absolute location is required.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="actualLocation"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// The <paramref name="actualLocation"/> is a relative location where an absolute location is required.
        /// </exception>
        protected static void ValidateParameters(Uri expectedLocation, Uri actualLocation)
        {
            if (actualLocation == null)
            {
                throw new ArgumentNullException("actualLocation");
            }

            if (actualLocation.IsAbsoluteUri == false)
            {
                throw new ArgumentException(Resources.Uri_LocationMustBeAbsolute, "actualLocation");
            }

            if (expectedLocation == null)
            {
                throw new ArgumentNullException("expectedLocation");
            }

            if (expectedLocation.IsAbsoluteUri == false)
            {
                throw new ArgumentException(Resources.Uri_LocationMustBeAbsolute, "expectedLocation");
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether location matching is case sensitive.
        /// </summary>
        /// <value>
        ///     <c>true</c> if location matching is case sensitive; otherwise, <c>false</c>.
        /// </value>
        public bool CaseSensitive
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the compare format.
        /// </summary>
        /// <value>
        ///     The compare format.
        /// </value>
        public UriFormat CompareFormat
        {
            get;
            set;
        }

        /// <inheritdoc />
        public virtual LocationValidationType ValidationType
        {
            get
            {
                return LocationValidationType.UriOnly;
            }
        }

        /// <summary>
        ///     Gets or sets the verification parts.
        /// </summary>
        /// <value>
        ///     The verification parts.
        /// </value>
        protected UriComponents VerificationParts
        {
            get;
            set;
        }
    }
}