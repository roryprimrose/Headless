namespace Headless
{
    using System;

    /// <summary>
    ///     The <see cref="StrictComponentsLocationValidator" />
    ///     class uses strict rules by default to validate locations using <see cref="Uri.GetComponents" />.
    /// </summary>
    public class StrictComponentsLocationValidator : ComponentsLocationValidator
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="StrictComponentsLocationValidator" /> class.
        /// </summary>
        public StrictComponentsLocationValidator()
        {
            VerificationParts = UriComponents.SchemeAndServer | UriComponents.PathAndQuery;
            CompareFormat = UriFormat.SafeUnescaped;
            CaseSensitive = true;
        }
    }
}