namespace Headless
{
    using System.Diagnostics;
    using System.IO;
    using System.Net.Http;
    using System.Xml;
    using System.Xml.XPath;
    using Headless.Activation;
    using Sgml;

    /// <summary>
    ///     The <see cref="HtmlPage" />
    ///     class provides the HTML response from a <see cref="Browser" /> request.
    /// </summary>
    public abstract class HtmlPage : Page, IHtmlPage
    {
        /// <summary>
        ///     Stores the content.
        /// </summary>
        private XmlDocument _content;

        /// <summary>
        ///     The element factory.
        /// </summary>
        private IHtmlElementFactory _elementFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HtmlPage" /> class.
        /// </summary>
        protected HtmlPage()
        {
            _elementFactory = new DefaultHtmlElementFactory();
        }

        /// <inheritdoc />
        public virtual IHtmlElementFinder<T> Find<T>() where T : HtmlElement
        {
            return new DefaultHtmlElementFinder<T>(this);
        }

        /// <inheritdoc />
        internal override void SetContent(HttpContent content)
        {
            var result = content.ReadAsStreamAsync().Result;

            using (TextReader reader = new StreamReader(result))
            {
                // setup SgmlReader
                using (var sgmlReader = new SgmlReader())
                {
                    sgmlReader.DocType = "HTML";
                    sgmlReader.WhitespaceHandling = WhitespaceHandling.All;
                    sgmlReader.CaseFolding = CaseFolding.ToLower;
                    sgmlReader.InputStream = reader;

                    // create document
                    _content = new XmlDocument
                    {
                        PreserveWhitespace = true, 
                        XmlResolver = null
                    };

                    _content.Load(sgmlReader);
                }
            }
        }

        /// <inheritdoc />
        public virtual IXPathNavigable Document
        {
            [DebuggerStepThrough]
            get
            {
                return _content;
            }
        }

        /// <inheritdoc />
        public virtual IHtmlElementFactory ElementFactory
        {
            get
            {
                return _elementFactory;
            }

            protected set
            {
                _elementFactory = value;
            }
        }

        /// <inheritdoc />
        public virtual IXPathNavigable Node
        {
            [DebuggerStepThrough]
            get
            {
                return _content.DocumentElement;
            }
        }
    }
}