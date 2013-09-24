namespace Headless
{
    using System;
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
        private IXPathNavigable _content;

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
        public T CloneAs<T>() where T : IHtmlPage, new()
        {
            var page = new T();

            page.Initialize(this);

            return page;
        }

        /// <inheritdoc />
        public virtual IHtmlElementFinder<T> Find<T>() where T : HtmlElement
        {
            return new DefaultHtmlElementFinder<T>(this);
        }

        /// <inheritdoc />
        /// <exception cref="System.ArgumentNullException">
        ///     The <paramref name="page" /> parameter is <c>null</c>.
        /// </exception>
        public void Initialize(IHtmlPage page)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page");
            }

            Initialize(page.Browser, page.StatusCode, page.StatusDescription, page.Result);

            _content = page.Document;
        }

        /// <inheritdoc />
        protected internal override void SetContent(HttpContent content)
        {
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }

            var result = content.ReadAsStreamAsync().Result;

            using (TextReader reader = new StreamReader(result))
            {
                SetContent(reader);
            }
        }

        /// <summary>
        /// Sets the content of the page.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="reader"/> parameter is <c>null</c>.
        /// </exception>
        protected void SetContent(TextReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            // setup SgmlReader
            using (var sgmlReader = new SgmlReader())
            {
                sgmlReader.DocType = "HTML";
                sgmlReader.IgnoreDtd = true;
                sgmlReader.WhitespaceHandling = WhitespaceHandling.All;
                sgmlReader.CaseFolding = CaseFolding.ToLower;
                sgmlReader.InputStream = reader;

                // create document
                var document = new XmlDocument
                {
                    PreserveWhitespace = true, 
                    XmlResolver = null
                };

                document.Load(sgmlReader);

                _content = document;
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
        public string Html
        {
            get
            {
                var navigator = Node.GetNavigator();

                return navigator.OuterXml;
            }
        }

        /// <summary>
        ///     Gets the HTML document of the page.
        /// </summary>
        /// <value>
        ///     The HTML document of the page.
        /// </value>
        protected internal IXPathNavigable Document
        {
            [DebuggerStepThrough]
            get
            {
                return ((IHtmlPage)this).Document;
            }
        }

        /// <summary>
        ///     Gets the node.
        /// </summary>
        /// <value>
        ///     The node.
        /// </value>
        protected internal IXPathNavigable Node
        {
            [DebuggerStepThrough]
            get
            {
                return ((IHtmlPage)this).Node;
            }
        }

        /// <inheritdoc />
        IXPathNavigable IHtmlPage.Document
        {
            [DebuggerStepThrough]
            get
            {
                return _content;
            }
        }

        /// <inheritdoc />
        IXPathNavigable IHtmlPage.Node
        {
            [DebuggerStepThrough]
            get
            {
                var navigator = _content.GetNavigator();

                navigator.MoveToRoot();
                navigator.MoveToFirstChild();

                return navigator;
            }
        }
    }
}