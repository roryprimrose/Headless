namespace Headless
{
    using System.Diagnostics;
    using System.IO;
    using System.Net.Http;
    using System.Xml;
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

        /// <inheritdoc />
        public HtmlElementFinder<T> Find<T>() where T : HtmlElement
        {
            return new HtmlElementFinder<T>(this);
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
        public XmlDocument Document
        {
            [DebuggerStepThrough]
            get
            {
                return _content;
            }
        }

        /// <inheritdoc />
        public XmlNode Node
        {
            [DebuggerStepThrough]
            get
            {
                return _content.DocumentElement;
            }
        }
    }
}