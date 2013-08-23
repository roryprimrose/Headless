namespace Headless.IntegrationTests.Pages
{
    /// <summary>
    ///     The <see cref="CommonLayoutPage" />
    ///     provides access to the common elements of the website.
    /// </summary>
    public abstract class CommonLayoutPage : HtmlPage
    {
        /// <summary>
        ///     Gets the link to the about page.
        /// </summary>
        /// <value>
        ///     The link to the about page.
        /// </value>
        public HtmlLink About
        {
            get
            {
                return Find<HtmlLink>().AllByText("About").EnsureSingle();
            }
        }
    }
}