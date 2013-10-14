namespace Headless.DemoSite.Models
{
    /// <summary>
    ///     The <see cref="FileMetadata" />
    ///     class provides information about posted files.
    /// </summary>
    public class FileMetadata
    {
        /// <summary>
        ///     Gets or sets the content.
        /// </summary>
        /// <value>
        ///     The content.
        /// </value>
        public string Content
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the type of the content.
        /// </summary>
        /// <value>
        ///     The type of the content.
        /// </value>
        public string ContentType
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the name of the file.
        /// </summary>
        /// <value>
        ///     The name of the file.
        /// </value>
        public string FileName
        {
            get;
            set;
        }
    }
}