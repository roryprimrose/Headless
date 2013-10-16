namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net;
    using System.Xml.XPath;
    using Headless.Activation;

    /// <summary>
    ///     The <see cref="HtmlForm" />
    ///     class is used to represent a HTML form.
    /// </summary>
    [SupportedTag("form")]
    public class HtmlForm : HtmlElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlForm"/> class.
        /// </summary>
        /// <param name="page">
        /// The owning page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="page"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="node"/> parameter is <c>null</c>.
        /// </exception>
        public HtmlForm(IHtmlPage page, IXPathNavigable node) : base(page, node)
        {
        }

        /// <summary>
        ///     Submits the specified form and returns the response from the server.
        /// </summary>
        /// <returns>
        ///     A <see cref="IPage" /> value.
        /// </returns>
        public dynamic Submit()
        {
            return Submit(null);
        }

        /// <summary>
        /// Submits the specified form and returns the response from the server.
        /// </summary>
        /// <param name="sourceButton">
        /// The button that invoked the submit action.
        /// </param>
        /// <returns>
        /// A <see cref="IPage"/> value.
        /// </returns>
        public dynamic Submit(HtmlButton sourceButton)
        {
            var parameters = BuildPostParameters(sourceButton);

            return Page.Browser.PostTo(parameters, PostLocation, HttpStatusCode.OK);
        }

        /// <summary>
        ///     Submits the specified form and returns the response from the server.
        /// </summary>
        /// <typeparam name="T">The type of page returned.</typeparam>
        /// <returns>
        ///     A <typeparamref name="T" /> value.
        /// </returns>
        public T Submit<T>() where T : IPage, new()
        {
            return Submit<T>(null);
        }

        /// <summary>
        /// Submits the specified form and returns the response from the server.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page returned.
        /// </typeparam>
        /// <param name="sourceButton">
        /// The button that invoked the submit action.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        public T Submit<T>(HtmlButton sourceButton) where T : IPage, new()
        {
            var parameters = BuildPostParameters(sourceButton);

            return Page.Browser.PostTo<T>(parameters, PostLocation, HttpStatusCode.OK);
        }

        /// <summary>
        ///     Gets the action of the form.
        /// </summary>
        /// <value>
        ///     The action of the form.
        /// </value>
        public string Action
        {
            get
            {
                return GetAttribute("action");
            }
        }

        /// <summary>
        ///     Gets the method of the form.
        /// </summary>
        /// <value>
        ///     The method of the form.
        /// </value>
        public string Method
        {
            get
            {
                return GetAttribute("method");
            }
        }

        /// <summary>
        ///     Gets the name of the form.
        /// </summary>
        /// <value>
        ///     The name of the form.
        /// </value>
        public string Name
        {
            get
            {
                return GetAttribute("name");
            }
        }

        /// <summary>
        ///     Gets the location that the form will post to.
        /// </summary>
        /// <value>
        ///     The location that the form will post to.
        /// </value>
        public Uri PostLocation
        {
            get
            {
                Uri location;
                var action = Action;

                if (string.IsNullOrWhiteSpace(action))
                {
                    // There is no action so we are posting to the current location
                    location = Page.TargetLocation;
                }
                else
                {
                    location = new Uri(action, UriKind.RelativeOrAbsolute);

                    if (location.IsAbsoluteUri == false)
                    {
                        location = new Uri(Page.TargetLocation, location);
                    }
                }

                return location;
            }
        }

        /// <summary>
        ///     Gets the target of the form.
        /// </summary>
        /// <value>
        ///     The target of the form.
        /// </value>
        public string Target
        {
            get
            {
                return GetAttribute("target");
            }
        }

        /// <summary>
        /// Builds the post parameters.
        /// </summary>
        /// <param name="sourceButton">
        /// The source button.
        /// </param>
        /// <returns>
        /// A <see cref="IEnumerable{T}"/> value.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", 
            Justification = "The types here are logically correct for the purpose of the method.")]
        public virtual IEnumerable<PostEntry> BuildPostParameters(HtmlButton sourceButton)
        {
            // Find all the form elements that are not buttons
            var availableElements = Find<HtmlFormElement>().All().Where(x => x is HtmlButton == false).ToList();

            if (sourceButton != null && string.IsNullOrWhiteSpace(sourceButton.Name) == false)
            {
                // The source button can be identified to the server so it must be added to the post data
                availableElements.Add(sourceButton);
            }

            var postData = availableElements.SelectMany(x => x.BuildPostData());

            return postData;
        }
    }
}