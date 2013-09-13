namespace Headless
{
    using System;

    /// <summary>
    ///     The <see cref="HtmlPageWrapper" />
    ///     class is used to provide a wrapper for the <see cref="DynamicHtmlPage" />
    ///     to avoid code duplication.
    /// </summary>
    internal class HtmlPageWrapper : HtmlPage
    {
        /// <summary>
        ///     The TargetLocation.
        /// </summary>
        private readonly Uri _targetLocation;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlPageWrapper"/> class.
        /// </summary>
        /// <param name="targetLocation">
        /// The target location.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="targetLocation"/> parameter is <c>null</c>.
        /// </exception>
        public HtmlPageWrapper(Uri targetLocation)
        {
            if (targetLocation == null)
            {
                throw new ArgumentNullException("targetLocation");
            }

            _targetLocation = targetLocation;
        }

        /// <inheritdoc />
        public override Uri TargetLocation
        {
            get
            {
                return _targetLocation;
            }
        }
    }
}