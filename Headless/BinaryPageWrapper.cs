namespace Headless
{
    using System;
    using System.Linq;
    using System.Net.Http;

    /// <summary>
    ///     The <see cref="BinaryPageWrapper" />
    ///     provides the wrapper logic around a dynamic binary page reference.
    /// </summary>
    internal class BinaryPageWrapper : BinaryPage
    {
        /// <summary>
        ///     The location.
        /// </summary>
        private Uri _location;

        /// <inheritdoc />
        public override void Initialize(IBrowser browser, HttpResponseMessage response, HttpResult result)
        {
            base.Initialize(browser, response, result);

            _location = result.Outcomes.Last().Location;
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