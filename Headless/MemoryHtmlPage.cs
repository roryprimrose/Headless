namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using Headless.Properties;

    /// <summary>
    ///     The <see cref="MemoryHtmlPage" />
    ///     class provides a <see cref="HtmlPage" />
    ///     created from a string of HTML rather than the result of a browser request.
    /// </summary>
    public class MemoryHtmlPage : HtmlPage
    {
        /// <summary>
        ///     The target location of the memory page.
        /// </summary>
        private readonly Uri _targetLocation = new Uri("https://github.com/roryprimrose/Headless");

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryHtmlPage"/> class.
        /// </summary>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="html">
        /// The HTML.
        /// </param>
        /// <exception cref="ArgumentNullException">The <paramref name="browser"/> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">The <paramref name="html"/> parameter is <c>null</c>, empty or only contains white space.</exception>
        public MemoryHtmlPage(IBrowser browser, string html)
        {
            if (browser == null)
            {
                throw new ArgumentNullException("browser");
            }

            if (string.IsNullOrWhiteSpace(html))
            {
                throw new ArgumentException(Resources.HtmlPage_NoHtmlContentProvided, "html");
            }

            IEnumerable<HttpOutcome> outcomes = new List<HttpOutcome>
            {
                new HttpOutcome(_targetLocation, HttpMethod.Get, HttpStatusCode.OK, "OK", TimeSpan.FromMilliseconds(5))
            };
            var result = new HttpResult(outcomes);

            Initialize(browser, HttpStatusCode.OK, "OK", result);

            using (var reader = new StringReader(html))
            {
                SetContent(reader);
            }
        }

        /// <inheritdoc />
        public override Uri TargetLocation
        {
            get
            {
                return _targetLocation;
            }
        }
    }
}