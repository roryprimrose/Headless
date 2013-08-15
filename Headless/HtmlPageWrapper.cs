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
        ///     The location.
        /// </summary>
        private readonly Uri _location;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlPageWrapper"/> class.
        /// </summary>
        /// <param name="location">
        /// The location.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="location"/> parameter is <c>null</c>.
        /// </exception>
        public HtmlPageWrapper(Uri location)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location");
            }

            _location = location;
        }

        /// <inheritdoc />
        public override bool IsOn(Uri location)
        {
            // There is no verification of dynamic page locations because there is no model to define where the current location should be
            return true;
        }

        /// <inheritdoc />
        public override Uri Location
        {
            get
            {
                return _location;
            }
        }
    }
}