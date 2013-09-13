namespace Headless
{
    using System;
    using System.Linq;
    using System.Net.Http;

    /// <summary>
    ///     The <see cref="TextPageWrapper" />
    ///     provides the wrapper logic around a dynamic text page reference.
    /// </summary>
    internal class TextPageWrapper : TextPage
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