﻿namespace Headless
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     The <see cref="SupportedTagAttributeComparer" />
    ///     class is used to provide comparison support for the <see cref="SupportedTagAttribute" /> class.
    /// </summary>
    internal class SupportedTagAttributeComparer : IEqualityComparer<SupportedTagAttribute>
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

            if (SafeString(x.AttributeName) != SafeString(y.AttributeName))
            {
                return false;
            }

            if (SafeString(x.AttributeValue) != SafeString(y.AttributeValue))
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc />
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