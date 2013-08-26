namespace Headless
{
    using System;
    using Headless.Properties;

    /// <summary>
    ///     The <see cref="PostEntry" />
    ///     structure is used to describe an entry for sending POST data.
    /// </summary>
    public class PostEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostEntry"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <exception cref="ArgumentException">
        /// The <paramref name="name"/> parameter is <c>null</c>, empty or only contains white-space.
        /// </exception>
        public PostEntry(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(Resources.Guard_NoValueProvided, "name");
            }

            Name = name;
            Value = value;
        }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        public string Value
        {
            get;
            private set;
        }
    }
}