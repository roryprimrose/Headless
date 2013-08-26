namespace Headless.IntegrationTests
{
    using System;

    /// <summary>
    ///     The <see cref="Form" />
    ///     class provides the locations of addresses under the Form controller.
    /// </summary>
    public static class Form
    {
        /// <summary>
        ///     Gets the files address.
        /// </summary>
        /// <value>
        ///     The files address.
        /// </value>
        public static Uri Files
        {
            get
            {
                return new Uri(Config.BaseWebAddress, "form/files");
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
                return new Uri(Config.BaseWebAddress, "form/index");
            }
        }
    }
}