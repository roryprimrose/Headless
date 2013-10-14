namespace Headless
{
    using System.IO;

    /// <summary>
    ///     The <see cref="PostFileEntry" />
    ///     class is used to provide the information for posting file data.
    /// </summary>
    public class PostFileEntry : PostEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostFileEntry"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <remarks>
        /// The <paramref name="filePath"/> parameter must contain the absolute file path for the file file.
        /// </remarks>
        public PostFileEntry(string name, string filePath) : base(name, filePath)
        {
        }

        /// <summary>
        ///     Reads the content of the file.
        /// </summary>
        /// <returns>A <see cref="Stream" /> value.</returns>
        public virtual Stream ReadContent()
        {
            return File.OpenRead(Value);
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsValid
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Value))
                {
                    return false;
                }

                return File.Exists(Value);
            }
        }
    }
}