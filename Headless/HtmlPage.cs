﻿namespace Headless
{
    using System.Diagnostics;
    using System.Net.Http;
    using HtmlAgilityPack;

    /// <summary>
    ///     The <see cref="HtmlPage" />
    ///     class provides the HTML response from a <see cref="Browser" /> request.
    /// </summary>
    public abstract class HtmlPage : Page, IHtmlPage
    {
        /// <summary>
        ///     Stores the content.
        /// </summary>
        private HtmlDocument _content;

        /// <inheritdoc />
        public HtmlElementFinder<T> Find<T>() where T : HtmlElement
        {
            return new HtmlElementFinder<T>(this);
        }

        /// <inheritdoc />
        internal override void SetContent(HttpContent content)
        {
            var result = content.ReadAsStreamAsync().Result;

            _content = new HtmlDocument();

            _content.Load(result);
        }

        /// <inheritdoc />
        public HtmlDocument Document
        {
            [DebuggerStepThrough]
            get
            {
                return _content;
            }
        }

        /// <inheritdoc />
        public HtmlNode Node
        {
            [DebuggerStepThrough]
            get
            {
                return _content.DocumentNode;
            }
        }
    }
}