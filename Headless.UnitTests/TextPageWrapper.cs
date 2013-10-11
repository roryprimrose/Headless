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
        ///     The TargetLocation.
        /// </summary>
        private readonly Uri _targetLocation;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextPageWrapper" /> class.
        /// </summary>
        public TextPageWrapper()
            : this(new Uri("https://google.com"))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextPageWrapper"/> class.
        /// </summary>
        /// <param name="targetLocation">
        /// The target location.
        /// </param>
        public TextPageWrapper(Uri targetLocation)
        {
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