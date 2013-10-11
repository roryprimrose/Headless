namespace Headless.IntegrationTests.Pages
{
    using System;

    /// <summary>
    ///     The <see cref="RedirectTemporaryPage" />
    ///     class provides the page model for the redirect temporary page.
    /// </summary>
    public class RedirectTemporaryPage : HtmlPage
    {
        /// <inheritdoc />
        public override Uri TargetLocation
        {
            get
            {
                return Redirect.Temporary;
            }
        }
    }
}