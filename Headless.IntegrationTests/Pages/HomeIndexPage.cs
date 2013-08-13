namespace Headless.IntegrationTests.Pages
{
    using System;

    /// <summary>
    ///     The <see cref="HomeIndexPage" />
    ///     class is used to provide a wrapper around the home index page.
    /// </summary>
    public class HomeIndexPage : CommonLayoutPage
    {
        /// <inheritdoc />
        public override Uri Location
        {
            get
            {
                return Config.BaseWebAddress;
            }
        }
    }
}