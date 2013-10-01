namespace Headless.IntegrationTests
{
    using System;

    /// <summary>
    ///     The <see cref="Content" />
    ///     class provides the locations of addresses under the contents folder.
    /// </summary>
    public static class Content
    {
        /// <summary>
        ///     Gets the binary test.
        /// </summary>
        /// <value>
        ///     The binary test.
        /// </value>
        public static Uri BinaryTest
        {
            get
            {
                return new Uri(Config.BaseWebAddress, "/images/bullet.png");
            }
        }

        /// <summary>
        ///     Gets the text test address.
        /// </summary>
        /// <value>
        ///     The text test address.
        /// </value>
        public static Uri TextTest
        {
            get
            {
                return new Uri(Config.BaseWebAddress, "content/texttest.txt");
            }
        }
    }
}