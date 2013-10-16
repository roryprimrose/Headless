namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    ///     The <see cref="ILocationValidator" />
    ///     interface defines the methods for evaluating <see cref="Uri" /> location matches.
    /// </summary>
    public interface ILocationValidator
    {
        /// <summary>
        /// Determines whether the location to verify matches the expected location.
        /// </summary>
        /// <param name="location">
        /// The location to verify.
        /// </param>
        /// <param name="expectedLocation">
        /// The expected location.
        /// </param>
        /// <returns>
        /// <c>true</c> if the locations match; otherwise <c>false</c>.
        /// </returns>
        bool Matches(Uri location, Uri expectedLocation);

        /// <summary>
        /// Determines whether the location matches any of the specified regular expressions.
        /// </summary>
        /// <param name="location">
        /// The location to verify.
        /// </param>
        /// <param name="matchingExpressions">
        /// The matching expressions.
        /// </param>
        /// <returns>
        /// <c>true</c> if the location matches any provided expression; otherwise <c>false</c>.
        /// </returns>
        bool Matches(Uri location, IEnumerable<Regex> matchingExpressions);

        /// <summary>
        ///     Gets the type of the validation that this validator supports.
        /// </summary>
        /// <value>
        ///     The type of the validation that this validator supports.
        /// </value>
        LocationValidationType ValidationType
        {
            get;
        }
    }
}