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
        private Uri _targetLocation;

        /// <inheritdoc />
        public override void Initialize(IBrowser browser, HttpResponseMessage response, HttpResult result)
        {
            base.Initialize(browser, response, result);

            _targetLocation = result.Outcomes.Last().Location;
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