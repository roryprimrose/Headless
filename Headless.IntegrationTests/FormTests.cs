namespace Headless.IntegrationTests
{
    using System;
    using FluentAssertions;
    using Headless.IntegrationTests.Pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="FormTests" />
    ///     class tests the <see cref="Form" /> class.
    /// </summary>
    [TestClass]
    public class FormTests
    {
        /// <summary>
        ///     Runs a test for text field on dynamic page.
        /// </summary>
        [TestMethod]
        public void TextFieldOnDynamicPageTest()
        {
            using (var browser = new Browser())
            {
                var newValue = Guid.NewGuid().ToString();

                var page = browser.GoTo(Form.Index);

                ((IPage)page).Result.TraceResults();

                page.Text.Value = newValue;

                var postedPage = page.Submit.Click();

                ((IPage)postedPage).Result.TraceResults();

                ((string)postedPage.Text.Value).Should().Be(newValue);
            }
        }

        /// <summary>
        ///     Runs a test for text field on page model.
        /// </summary>
        [TestMethod]
        public void TextFieldOnPageModelTest()
        {
            using (var browser = new Browser())
            {
                var newValue = Guid.NewGuid().ToString();

                var page = browser.GoTo<FormIndexPage>();

                page.Result.TraceResults();

                page.Text.Value = newValue;
                var postedPage = page.Submit.Click<FormIndexPage>();

                postedPage.Result.TraceResults();

                postedPage.Text.Value.Should().Be(newValue);
            }
        }
    }
}