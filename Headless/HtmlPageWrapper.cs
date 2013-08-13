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
        public override Uri Location
        {
            get
            {
                return _location;
            }
        }
    }
}