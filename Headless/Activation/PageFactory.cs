namespace Headless.Activation
{
    using System;
    using System.Net.Http;

    /// <summary>
    ///     The <see cref="PageFactory" />
    ///     class is used to provide factories that can create <see cref="IPage" />
    ///     instances in a way that is abstracted away from browser implementations.
    /// </summary>
    internal static class PageFactory
    {
        /// <summary>
        /// Returns the default factory that returns an <typeparamref name="T"/> page.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <returns>
        /// The <see cref="IPage"/>.
        /// </returns>
        public static IPage DefaultPageFactory<T>(IBrowser browser, HttpResponseMessage response, HttpResult result)
            where T : IPage, new()
        {
            var newPage = new T();

            newPage.Initialize(browser, response, result);

            return newPage;
        }

        /// <summary>
        /// Returns a factory function for creating an <see cref="IPage"/> depending on the content type of the response.
        /// </summary>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <returns>
        /// An <see cref="IPage"/> value.
        /// </returns>
        public static IPage DynamicPageFactory(IBrowser browser, HttpResponseMessage response, HttpResult result)
        {
            // Look at the content type header to determine the correct type of page to return
            var contentType = DetermineContentType(response);
            IPage page;

            if (contentType == ContentType.Html)
            {
                page = new DynamicHtmlPage();
            }
            else if (contentType == ContentType.Binary)
            {
                page = new BinaryPageWrapper();
            }
            else
            {
                page = new TextPageWrapper();
            }

            page.Initialize(browser, response, result);

            return page;
        }

        /// <summary>
        /// Determines the type of the content.
        /// </summary>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <returns>
        /// A <see cref="ContentType"/> value.
        /// </returns>
        private static ContentType DetermineContentType(HttpResponseMessage response)
        {
            var mediaType = response.Content.Headers.ContentType.MediaType;

            if (mediaType.Equals("text/html", StringComparison.OrdinalIgnoreCase))
            {
                return ContentType.Html;
            }

            if (mediaType.Equals("text/xml", StringComparison.OrdinalIgnoreCase))
            {
                return ContentType.Html;
            }

            // TODO: Find out more binary content types here
            if (mediaType.IndexOf("image", StringComparison.OrdinalIgnoreCase) > -1)
            {
                return ContentType.Binary;
            }

            return ContentType.Text;
        }

        /// <summary>
        ///     The <see cref="ContentType" />
        ///     enum identifies broad HTTP content types.
        /// </summary>
        private enum ContentType
        {
            /// <summary>
            ///     The document contains HTML/XML formatted text.
            /// </summary>
            Html = 0, 

            /// <summary>
            ///     The document contains text.
            /// </summary>
            Text, 

            /// <summary>
            ///     The document contains binary data.
            /// </summary>
            Binary
        }
    }
}