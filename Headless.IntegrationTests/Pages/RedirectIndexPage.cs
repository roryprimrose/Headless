namespace Headless.IntegrationTests.Pages
{
    using System;

    /// <summary>
    ///     The <see cref="RedirectIndexPage" />
    ///     class provides the page model for the redirect index page.
    /// </summary>
    public class RedirectIndexPage : HtmlPage
    {
        /// <summary>
        ///     Gets the external link.
        /// </summary>
        /// <value>
        ///     The external link.
        /// </value>
        public HtmlLink External
        {
            get
            {
                return Find<HtmlLink>().AllByText("External").EnsureSingle();
            }
        }

        /// <inheritdoc />
        public override Uri TargetLocation
        {
            get
            {
                return new Uri(Config.BaseWebAddress, "redirect");
            }
        }

        /// <summary>
        ///     Gets the permanent link.
        /// </summary>
        /// <value>
        ///     The permanent link.
        /// </value>
        public HtmlLink Permanent
        {
            get
            {
                return Find<HtmlLink>().AllByText("Permanent").EnsureSingle();
            }
        }

        /// <summary>
        ///     Gets the temporary link.
        /// </summary>
        /// <value>
        ///     The temporary link.
        /// </value>
        public HtmlLink Temporary
        {
            get
            {
                return Find<HtmlLink>().AllByText("Temporary").EnsureSingle();
            }
        }
    }
}