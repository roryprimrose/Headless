namespace Headless.UnitTests
{
    using System;
    using System.Xml;
    using System.Xml.XPath;

    /// <summary>
    ///     The <see cref="HtmlPageStub" />
    ///     class is used to test HTML page functionality.
    /// </summary>
    internal class HtmlPageStub : HtmlPage
    {
        /// <summary>
        ///     The document.
        /// </summary>
        private readonly XmlDocument _document;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlPageStub"/> class.
        /// </summary>
        /// <param name="document">
        /// The document.
        /// </param>
        public HtmlPageStub(XmlDocument document)
        {
            _document = document;
        }

        /// <inheritdoc />
        public override IXPathNavigable Document
        {
            get
            {
                return _document;
            }
        }

        /// <inheritdoc />
        public override Uri Location
        {
            get
            {
                return new Uri("http://somewhere.com");
            }
        }

        /// <inheritdoc />
        public override IXPathNavigable Node
        {
            get
            {
                return _document.DocumentElement;
            }
        }
    }
}