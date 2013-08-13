namespace Headless.IntegrationTests
{
    using System;

    /// <summary>
    ///     The <see cref="Config" />
    ///     class provides configuration information for running tests.
    /// </summary>
    internal static class Config
    {
        /// <summary>
        ///     Gets the base web address.
        /// </summary>
        /// <value>
        ///     The base web address.
        /// </value>
        public static Uri BaseWebAddress
        {
            get
            {
                return new Uri("http://localhost:25959/");
            }
        }
    }
}