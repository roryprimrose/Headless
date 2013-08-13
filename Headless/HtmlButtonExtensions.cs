namespace Headless
{
    using System;

    /// <summary>
    ///     The <see cref="HtmlButtonExtensions" />
    ///     class provides extension methods for the <see cref="HtmlButton" /> class.
    /// </summary>
    public static class HtmlButtonExtensions
    {
        /// <summary>
        /// Clicks the specified button.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <param name="button">
        /// The button.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="button"/> parameter is <c>null</c>.
        /// </exception>
        public static T Click<T>(this HtmlButton button) where T : Page, new()
        {
            if (button == null)
            {
                throw new ArgumentNullException("button");
            }

            var form = button.GetContainingForm();

            return form.Submit<T>();
        }
    }
}