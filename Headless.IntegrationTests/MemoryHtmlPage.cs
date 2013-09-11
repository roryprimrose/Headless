namespace Headless.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Xml;

    /// <summary>
    ///     The <see cref="MemoryHtmlPage" />
    ///     class is used to test <see cref="HtmlPage" />
    ///     using string HTML as the source data.
    /// </summary>
    internal class MemoryHtmlPage : DynamicHtmlPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryHtmlPage"/> class.
        /// </summary>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="html">
        /// The HTML.
        /// </param>
        public MemoryHtmlPage(IBrowser browser, string html)
        {
            IEnumerable<HttpOutcome> outcomes = new List<HttpOutcome>
            {
                new HttpOutcome(
                    Config.BaseWebAddress, 
                    HttpMethod.Get, 
                    HttpStatusCode.OK, 
                    "OK", 
                    TimeSpan.FromMilliseconds(5))
            };
            var result = new HttpResult(outcomes);

            var document = new XmlDocument();

            document.LoadXml(html);

            Initialize(browser, HttpStatusCode.OK, "OK", result, document);
        }
    }
}