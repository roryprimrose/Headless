namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net;

    /// <summary>
    ///     The <see cref="HtmlFormExtensions" />
    ///     class provides extension methods for the <see cref="HtmlForm" /> class.
    /// </summary>
    public static class HtmlFormExtensions
    {
        /// <summary>
        /// Builds the post parameters.
        /// </summary>
        /// <param name="form">
        /// The form.
        /// </param>
        /// <param name="sourceButton">
        /// The source button.
        /// </param>
        /// <returns>
        /// A <see cref="IEnumerable{T}"/> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="form"/> parameter is <c>null</c>.
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", 
            Justification = "The types here are logically correct for the purpose of the method.")]
        public static IEnumerable<PostEntry> BuildPostParameters(this HtmlForm form, HtmlButton sourceButton)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }

            // Find all the form elements that are not buttons
            var availableElements = form.Find<HtmlFormElement>().All().Where(x => x is HtmlButton == false).ToList();

            if (sourceButton != null && string.IsNullOrWhiteSpace(sourceButton.Name) == false)
            {
                // The source button can be identified to the server so it must be added to the post data
                availableElements.Add(sourceButton);
            }

            var postData = availableElements.SelectMany(x => x.BuildPostData());

            return postData;
        }

        /// <summary>
        /// Submits the specified form.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <param name="form">
        /// The form.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="form"/> parameter is <c>null</c>.
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", 
            Justification = "This action is only valid against a form.")]
        public static T Submit<T>(this HtmlForm form) where T : IPage, new()
        {
            return Submit<T>(form, null);
        }

        /// <summary>
        /// Submits the specified form.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <param name="form">
        /// The form.
        /// </param>
        /// <param name="sourceButton">
        /// The source button.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="form"/> parameter is <c>null</c>.
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", 
            Justification = "This action is only valid against a form.")]
        public static T Submit<T>(this HtmlForm form, HtmlButton sourceButton) where T : IPage, new()
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }

            var parameters = BuildPostParameters(form, sourceButton);

            return form.Page.Browser.PostTo<T>(parameters, form.PostLocation, HttpStatusCode.OK);
        }
    }
}