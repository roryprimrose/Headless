namespace Headless.IntegrationTests.Pages
{
    using System;

    /// <summary>
    ///     The <see cref="TextContentPage" />
    ///     class is used to test the download of text content.
    /// </summary>
    public class TextContentPage : TextPage
    {
        /// <inheritdoc />
        public override Uri TargetLocation
        {
            get
            {
                return IntegrationTests.Content.TextTest;
            }
        }
    }
}