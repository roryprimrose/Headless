namespace Headless.DemoSite.Models
{
    /// <summary>
    ///     The <see cref="FileData" />
    ///     class is used as the model for testing file uploads with form data.
    /// </summary>
    public class FileData
    {
        /// <summary>
        ///     Gets or sets the file count.
        /// </summary>
        /// <value>
        ///     The file count.
        /// </value>
        public int FileCount
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets some data.
        /// </summary>
        /// <value>
        ///     Some data.
        /// </value>
        public string SomeData
        {
            get;
            set;
        }
    }
}