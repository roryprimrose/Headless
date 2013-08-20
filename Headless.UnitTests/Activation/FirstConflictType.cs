namespace Headless.UnitTests.Activation
{
    using System.Xml.XPath;
    using Headless.Activation;

    /// <summary>
    ///     The <see cref="FirstConflictType" />
    ///     class is used to test type resolutions where there is a tag conflict.
    /// </summary>
    [SupportedTag("conflict")]
    internal class FirstConflictType : HtmlElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FirstConflictType"/> class.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        public FirstConflictType(IHtmlPage page, IXPathNavigable node) : base(page, node)
        {
        }
    }
}