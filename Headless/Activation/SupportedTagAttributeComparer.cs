namespace Headless.Activation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     The <see cref="SupportedTagAttributeComparer" />
    ///     class is used to provide comparison support for the <see cref="SupportedTagAttribute" /> class.
    /// </summary>
    public class SupportedTagAttributeComparer : IEqualityComparer<SupportedTagAttribute>
    {
        /// <inheritdoc />
        public bool Equals(SupportedTagAttribute x, SupportedTagAttribute y)
        {
            if (x == null)
            {
                return false;
            }

            if (y == null)
            {
                return false;
            }

            if (x.TagName != y.TagName)
            {
                return false;
            }

            if (x.HasAttributeFilter != y.HasAttributeFilter)
            {
                return false;
            }

            if (x.HasAttributeFilter == false)
            {
                return true;
            }

            if (x.AttributeName != y.AttributeName)
            {
                return false;
            }

            if (x.AttributeValue != y.AttributeValue)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="obj"/> parameter is <c>null</c>.
        /// </exception>
        public int GetHashCode(SupportedTagAttribute obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            var overallKey = string.Concat(
                obj.TagName, 
                "|", 
                SafeString(obj.AttributeName), 
                "|" + SafeString(obj.AttributeValue));

            return overallKey.GetHashCode();
        }

        /// <summary>
        /// Returns a string value that is not null or surrounded by white space.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> value.
        /// </returns>
        private static string SafeString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            return value.Trim();
        }
    }
}