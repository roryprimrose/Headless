namespace Headless.IntegrationTests.Pages
{
    using System;
    using System.Linq;

    /// <summary>
    ///     The <see cref="FormIndexPage" />
    ///     class is used to wrap the form test page.
    /// </summary>
    public class FormIndexPage : HtmlPage
    {
        /// <summary>
        ///     Gets the location.
        /// </summary>
        public override Uri Location
        {
            get
            {
                return Form.Index;
            }
        }

        /// <summary>
        ///     Gets the submit.
        /// </summary>
        /// <value>
        ///     The submit.
        /// </value>
        public HtmlButton Submit
        {
            get
            {
                return Find<HtmlButton>().All().Single();
            }
        }

        /// <summary>
        ///     Gets the text.
        /// </summary>
        /// <value>
        ///     The text.
        /// </value>
        public HtmlTextElement Text
        {
            get
            {
                return Find<HtmlTextElement>().FindByName("Text");
            }
        }
    }
}