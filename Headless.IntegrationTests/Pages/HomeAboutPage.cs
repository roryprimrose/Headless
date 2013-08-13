﻿namespace Headless.IntegrationTests.Pages
{
    using System;

    /// <summary>
    ///     The <see cref="HomeAboutPage" />
    ///     class is used to provide a wrapper around the home about page.
    /// </summary>
    public class HomeAboutPage : CommonLayoutPage
    {
        /// <inheritdoc />
        public override Uri Location
        {
            get
            {
                return new Uri(Config.BaseWebAddress, "home/about");
            }
        }
    }
}