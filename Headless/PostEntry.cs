namespace Headless
{
    using System;
    using System.Globalization;
    using Headless.Properties;

    /// <summary>
    ///     The <see cref="PostEntry" />
    ///     class describes an entry for sending POST data.
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
        /// <remarks>
        /// This constructor uses <see cref="CultureInfo.InvariantCulture"/> to convert the value to a
        ///     <see cref="string"/>.
        /// </remarks>
        public PostEntry(string name, short value) : this(name, value.ToString(CultureInfo.InvariantCulture))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostEntry"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <remarks>
        /// This constructor uses <see cref="CultureInfo.InvariantCulture"/> to convert the value to a
        ///     <see cref="string"/>.
        /// </remarks>
        public PostEntry(string name, int value) : this(name, value.ToString(CultureInfo.InvariantCulture))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostEntry"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <remarks>
        /// This constructor uses <see cref="CultureInfo.InvariantCulture"/> to convert the value to a
        ///     <see cref="string"/>.
        /// </remarks>
        public PostEntry(string name, long value) : this(name, value.ToString(CultureInfo.InvariantCulture))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostEntry"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public PostEntry(string name, Guid value) : this(name, value.ToString())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostEntry"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public PostEntry(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(Resources.Guard_NoValueProvided, "name");
            }

            Name = name;

            if (value != null)
            {
                Value = value.ToString();
            }
        }

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