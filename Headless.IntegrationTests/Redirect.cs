namespace Headless.IntegrationTests
{
    using System;

    /// <summary>
    ///     The <see cref="Redirect" />
    ///     class provides the locations of addresses under the redirect controller.
    /// </summary>
    public static class Redirect
    {
        /// <summary>
        ///     Gets the index address.
        /// </summary>
        /// <value>
        ///     The index address.
        /// </value>
        public static Uri Index
        {
            get
            {
                return new Uri(Config.BaseWebAddress, "redirect/index");
            }
        }
    }
}