namespace Headless
{
    using System;
    using System.IO;

    /// <summary>
    ///     The <see cref="PostFileStreamEntry" />
    ///     class provides the information about a file to post with the specified <see cref="Stream" />.
    /// </summary>
    public class PostFileStreamEntry : PostFileEntry, IDisposable
    {
        /// <summary>
        ///     The data.
        /// </summary>
        private readonly Stream _data;

        /// <summary>
        ///     Stores whether this instance has been disposed.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostFileStreamEntry"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="data"/> parameter is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// The <paramref name="value"/> parameter should be the name of the file or the path to a file.
        ///     The <paramref name="data"/> stream is disposed when this instance is disposed.
        /// </remarks>
        public PostFileStreamEntry(string name, string value, Stream data) : base(name, value)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            _data = data;
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        public override Stream ReadContent()
        {
            return _data;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed == false)
            {
                if (disposing)
                {
                    try
                    {
                        // Dispose managed resources.
                        _data.Dispose();
                    }
                    catch (ObjectDisposedException)
                    {
                        // We are ignoring this because the HTTP request may have already disposed this stream
                        // This disposal is to ensure that other uses of this class result in the stream being disposed
                    }
                }

                // There are no unmanaged resources to release, but
                // if we add them, they need to be released here.
            }

            _disposed = true;
        }

        /// <inheritdoc />
        public override bool IsValid
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Value))
                {
                    return false;
                }

                // This implementation only requires a value, not the existence of a file path
                return true;
            }
        }
    }
}