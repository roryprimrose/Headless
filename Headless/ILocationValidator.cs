namespace Headless
{
    using System;

    /// <summary>
    ///     The <see cref="ILocationValidator" />
    ///     interface defines the methods for evaluating <see cref="Uri" /> location matches.
    /// </summary>
    public interface ILocationValidator
    {
        /// <summary>
        /// Determines whether the expected location matches the actual location.
        /// </summary>
        /// <param name="expectedLocation">
        /// The expected location.
        /// </param>
        /// <param name="actualLocation">
        /// The actual location.
        /// </param>
        /// <returns>
        /// <c>true</c> if the locations match; otherwise <c>false</c>.
        /// </returns>
        bool Matches(Uri expectedLocation, Uri actualLocation);
    }
}