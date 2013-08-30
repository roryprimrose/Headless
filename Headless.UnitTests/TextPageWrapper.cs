namespace Headless.UnitTests
{
    using System;

    /// <summary>
    ///     The <see cref="TextPageWrapper" />
    ///     class is used for internal testing.
    /// </summary>
    internal class TextPageWrapper : TextPage
    {
        /// <summary>
        ///     The location.
        /// </summary>
        private readonly Uri _location;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextPageWrapper" /> class.
        /// </summary>
        public TextPageWrapper()
        {
            _location = new Uri("https://google.com");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextPageWrapper"/> class.
        /// </summary>
        /// <param name="location">
        /// The location.
        /// </param>
        public TextPageWrapper(Uri location)
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