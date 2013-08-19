namespace Headless.Activation
{
    using System.Net.Http;

    /// <summary>
    ///     The <see cref="DefaultPageFactory" />
    ///     class creates a page using its default construction and initialization.
    /// </summary>
    public class DefaultPageFactory : IPageFactory
    {
        /// <inheritdoc />
        public T Create<T>(IBrowser browser, HttpResponseMessage response, HttpResult result) where T : IPage, new()
        {
            var newPage = new T();

            newPage.Initialize(browser, response, result);

            return newPage;
        }
    }
}