namespace Headless.IntegrationTests.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    ///     The <see cref="HomeIndexPage" />
    ///     class is used to provide a wrapper around the home index page.
    /// </summary>
    public class HomeIndexPage : CommonLayoutPage
    {
        /// <inheritdoc />
        public override IEnumerable<Regex> LocationExpressions
        {
            get
            {
                var withControllerAction = new Uri(Config.BaseWebAddress, "/home/index");

                // Validate against the address with the controller and action specified (plus optional trailing /)
                yield return new Regex(Regex.Escape(withControllerAction.ToString()) + "(/)?", RegexOptions.IgnoreCase);

                // Validate against the root URL (plus optional trailing /)
                yield return new Regex(Regex.Escape(Config.BaseWebAddress.ToString()) + "(/)?", RegexOptions.IgnoreCase)
                    ;
            }
        }

        /// <inheritdoc />
        public override Uri TargetLocation
        {
            get
            {
                return Home.Index;
            }
        }
    }
}