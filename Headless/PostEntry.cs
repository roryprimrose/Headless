namespace Headless
{
    using System;
    using Headless.Properties;

    /// <summary>
    ///     The <see cref="PostEntry" />
    ///     structure is used to describe an entry for sending POST data.
    /// </summary>
    public struct PostEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostEntry"/> struct.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public PostEntry(string name, string value) : this()
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(Resources.Guard_NoValueProvided, "name");
            }

            Name = name;
            Value = value;
        }

        /// <summary>
        ///     Determines whether the two specified values are equal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     <c>true</c> if the specified values are equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(PostEntry left, PostEntry right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Determines whether the two specified values are not equal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     <c>true</c> if the specified values are not equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(PostEntry left, PostEntry right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="PostEntry"/> is equal to this instance.
        /// </summary>
        /// <param name="other">
        /// The <see cref="PostEntry"/> to compare with this instance.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="PostEntry"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(PostEntry other)
        {
            return string.Equals(Name, other.Name) && string.Equals(Value, other.Value);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="System.Object"/> to compare with this instance.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is PostEntry && Equals((PostEntry)obj);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Value != null ? Value.GetHashCode() : 0);
            }
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