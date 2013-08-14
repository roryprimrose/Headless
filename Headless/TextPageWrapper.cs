namespace Headless
{
    using System;

    /// <summary>
    ///     The <see cref="TextPageWrapper" />
    ///     provides the wrapper logic around a dynamic text page reference.
    /// </summary>
    internal class TextPageWrapper : TextPage
    {
        /// <summary>
        ///     The location.
        /// </summary>
        private readonly Uri _location;
        
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