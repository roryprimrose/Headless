namespace Headless
{
    /// <summary>
    ///     The <see cref="LocationValidationType" />
    ///     enum defines the type of location validation supported by a <see cref="ILocationValidator" /> instance.
    /// </summary>
    public enum LocationValidationType
    {
        /// <summary>
        ///     The location validation evaluates matches against both <see cref="IPage.TargetLocation" /> and
        ///     <see cref="IPage.LocationExpressions" />.
        /// </summary>
        All = 0, 

        /// <summary>
        ///     The location validation only evaluates matches against the <see cref="IPage.TargetLocation" /> value.
        /// </summary>
        UriOnly, 

        /// <summary>
        ///     The location validation only evaluates matches against the <see cref="IPage.LocationExpressions" />.
        /// </summary>
        RegexOnly
    }
}