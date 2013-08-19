namespace Headless.UnitTests
{
    using System;

    /// <summary>
    ///     The <see cref="PageWrapper" />
    ///     class is used for internal testing.
    /// </summary>
    internal class PageWrapper : TextPage
    {
        /// <summary>
        ///     The location.
        /// </summary>
        private readonly Uri _location;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PageWrapper" /> class.
        /// </summary>
        public PageWrapper()
        {
            _location = new Uri("https://google.com");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageWrapper"/> class.
        /// </summary>
        /// <param name="location">
        /// The location.
        /// </param>
        public PageWrapper(Uri location)
        {
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