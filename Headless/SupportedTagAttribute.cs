namespace Headless
{
    using System;

    /// <summary>
    ///     The <see cref="SupportedTagAttribute" />
    ///     class identifies the tag name associated with its related <see cref="HtmlElement" /> class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class SupportedTagAttribute : Attribute
    {
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