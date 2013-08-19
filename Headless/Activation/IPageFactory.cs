namespace Headless.Activation
{
    using System.Net.Http;

    /// <summary>
    ///     The <see cref="IPageFactory" />
    ///     interface defines the operations for creating <see cref="IPage" /> instances.
    /// </summary>
    public interface IPageFactory
    {
        /// <summary>
        /// Creates a new <typeparamref name="T"/> instance for the specified browser, response and result.
        /// </summary>
        /// <typeparam name="T">The type of page to create.</typeparam>
        /// <param name="browser">The browser.</param>
        /// <param name="response">The response.</param>
        /// <param name="result">The result.</param>
        /// <returns>
        /// The <see cref="IPage" /> value.
        /// </returns>
        T Create<T>(IBrowser browser, HttpResponseMessage response, HttpResult result) where T : IPage, new();
    }
}