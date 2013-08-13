namespace Headless.IntegrationTests
{
    using System;

    /// <summary>
    ///     The <see cref="Home" />
    ///     class provides the locations of addresses under the home controller.
    /// </summary>
    public static class Home
    {
        /// <summary>
        ///     Gets the about address.
        /// </summary>
        /// <value>
        ///     The about address.
        /// </value>
        public static Uri About
        {
            get
            {
                return new Uri(Config.BaseWebAddress, "home/about");
            }
        }

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
                return Config.BaseWebAddress;
            }
        }
    }
}