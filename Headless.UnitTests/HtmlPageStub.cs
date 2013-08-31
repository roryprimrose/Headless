namespace Headless.UnitTests
{
    using System;
    using System.IO;

    /// <summary>
    ///     The <see cref="HtmlPageStub" />
    ///     class is used to test HTML page functionality.
    /// </summary>
    internal class HtmlPageStub : HtmlPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlPageStub"/> class.
        /// </summary>
        /// <param name="htmlContent">
        /// Content of the HTML.
        /// </param>
        public HtmlPageStub(string htmlContent)
        {
            using (var reader = new StringReader(htmlContent))
            {
                SetContent(reader);
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
    }
}