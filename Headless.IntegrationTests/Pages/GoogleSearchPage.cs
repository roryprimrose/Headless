﻿namespace Headless.IntegrationTests.Pages
{
    using System;

    /// <summary>
    ///     The <see cref="GoogleSearchPage" />
    ///     provides a wrapper around the Google default search page.
    /// </summary>
    public class GoogleSearchPage : HtmlPage
    {
        /// <inheritdoc />
        protected override bool IsValidLocation(Uri location)
        {
            var isValid = base.IsValidLocation(location);

            if (isValid)
            {
                return true;
            }

            if (location.ToString().StartsWith("https://www.google.com", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public override Uri Location
        {
            get
            {
                return new Uri("https://google.com");
            }
        }
    }
}