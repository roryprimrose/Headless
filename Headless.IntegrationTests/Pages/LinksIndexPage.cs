namespace Headless.IntegrationTests.Pages
{
    using System;

    public class LinksIndexPage : HtmlPage
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
                return Find<HtmlLink>().ByText("Test External").EnsureSingle();
            }
        }

        /// <inheritdoc />
        public override Uri Location
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
                return Find<HtmlLink>().ByText("Test 301").EnsureSingle();
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
                return Find<HtmlLink>().ByText("Test 302").EnsureSingle();
            }
        }
    }
}