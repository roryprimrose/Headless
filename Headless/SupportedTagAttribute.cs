namespace Headless
{
    using System;
    using System.Globalization;

    /// <summary>
    ///     The <see cref="SupportedTagAttribute" />
    ///     class identifies the tag name associated with its related <see cref="HtmlElement" /> class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class SupportedTagAttribute : Attribute
    {
        /// <summary>
        ///     The attribute name.
        /// </summary>
        private readonly string _attributeName;

        /// <summary>
        ///     The attribute value.
        /// </summary>
        private readonly string _attributeValue;

        /// <summary>
        ///     Stores the tag name.
        /// </summary>
        private readonly string _tagName;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupportedTagAttribute"/> class.
        /// </summary>
        /// <param name="tagName">
        /// Name of the tag.
        /// </param>
        public SupportedTagAttribute(string tagName)
        {
            _tagName = tagName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SupportedTagAttribute"/> class.
        /// </summary>
        /// <param name="tagName">
        /// Name of the tag.
        /// </param>
        /// <param name="attributeName">
        /// Name of the attribute.
        /// </param>
        /// <param name="attributeValue">
        /// The attribute value.
        /// </param>
        public SupportedTagAttribute(string tagName, string attributeName, string attributeValue)
        {
            _tagName = tagName;
            _attributeName = attributeName;
            _attributeValue = attributeValue;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            const string TagOnlyFormat = "<{0} />";
            const string AttributeFormat = "<{0} {1}=\"{2}\" />";

            if (HasAttributeFilter)
            {
                return string.Format(
                    CultureInfo.CurrentCulture, 
                    AttributeFormat, 
                    TagName, 
                    AttributeName, 
                    AttributeValue);
            }

            return string.Format(CultureInfo.CurrentCulture, TagOnlyFormat, TagName);
        }

        /// <summary>
        ///     Gets the name of the attribute.
        /// </summary>
        /// <value>
        ///     The name of the attribute.
        /// </value>
        public string AttributeName
        {
            get
            {
                return _attributeName;
            }
        }

        /// <summary>
        ///     Gets the attribute value.
        /// </summary>
        /// <value>
        ///     The attribute value.
        /// </value>
        public string AttributeValue
        {
            get
            {
                return _attributeValue;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this instance has attribute filter.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has attribute filter; otherwise, <c>false</c>.
        /// </value>
        public bool HasAttributeFilter
        {
            get
            {
                if (string.IsNullOrWhiteSpace(AttributeName))
                {
                    return false;
                }

                if (string.IsNullOrWhiteSpace(AttributeValue))
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        ///     Gets the name of the tag.
        /// </summary>
        /// <value>
        ///     The name of the tag.
        /// </value>
        public string TagName
        {
            get
            {
                return _tagName;
            }
        }
    }
}