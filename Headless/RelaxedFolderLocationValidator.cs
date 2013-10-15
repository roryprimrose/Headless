namespace Headless
{
    using System;

    /// <summary>
    ///     The <see cref="RelaxedFolderLocationValidator" />
    ///     class uses relaxed rules for validating locations using <see cref="Uri.GetComponents" />.
    /// </summary>
    /// <remarks>
    ///     In addition to ignoring query strings and running case insensitive validations, this class also
    ///     automatically appends trailing slashes to paths that appear to represent folders before running validation.
    ///     This fixes validation issues where http://test.com/testing does not match http://test.com/testing/ where the server
    ///     is likely to interpret the two as the same resource.
    /// </remarks>
    public class RelaxedFolderLocationValidator : ComponentsLocationValidator
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RelaxedFolderLocationValidator" /> class.
        /// </summary>
        public RelaxedFolderLocationValidator()
        {
            VerificationParts = UriComponents.SchemeAndServer | UriComponents.Path;
            CompareFormat = UriFormat.SafeUnescaped;
            CaseSensitive = false;
        }

        /// <inheritdoc />
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="actualLocation" /> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        ///     The <paramref name="actualLocation" /> is a relative location where an absolute location is required.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="expectedLocation" /> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        ///     The <paramref name="expectedLocation" /> is a relative location where an absolute location is required.
        /// </exception>
        /// <remarks>
        ///     This method appends a forward slash character to the path part of each <see cref="Uri" />
        ///     where the path appears to be a folder reference that lacks a trailing forward slash.
        ///     This fixes validation issues where http://test.com/testing does not match http://test.com/testing/ where the server
        ///     is likely to interpret the two as the same resource.
        /// </remarks>
        public override bool Matches(Uri actualLocation, Uri expectedLocation)
        {
            CheckLocationValues(expectedLocation, actualLocation);

            var convertedExpected = MakeSafeFolderLocation(expectedLocation);
            var convertedActual = MakeSafeFolderLocation(actualLocation);

            return base.Matches(convertedActual, convertedExpected);
        }

        /// <summary>
        /// Makes the safe folder location.
        /// </summary>
        /// <param name="location">
        /// The location.
        /// </param>
        /// <returns>
        /// A <see cref="Uri"/> value.
        /// </returns>
        private Uri MakeSafeFolderLocation(Uri location)
        {
            var path = location.GetComponents(UriComponents.Path, CompareFormat);

            if (string.IsNullOrEmpty(path))
            {
                return location;
            }

            if (path.EndsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                return location;
            }

            // Check for a file name
            var folderIndex = path.LastIndexOf("/", StringComparison.OrdinalIgnoreCase);

            if (folderIndex > -1)
            {
                var finalPart = path.Substring(folderIndex + 1);

                if (finalPart.IndexOf(".", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    // This looks like a file so this location should not be converted
                    return location;
                }
            }

            var originalLocation = location.ToString();
            var updatedLocation = originalLocation.Replace(path, path + "/");

            return new Uri(updatedLocation);
        }
    }
}