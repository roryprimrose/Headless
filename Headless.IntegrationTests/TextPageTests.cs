namespace Headless.IntegrationTests
{
    using System.Diagnostics.CodeAnalysis;
    using FluentAssertions;
    using Headless.IntegrationTests.Pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="TextPageTests" />
    ///     class tests the ability to download text files.
    /// </summary>
    [TestClass]
    public class TextPageTests
    {
        /// <summary>
        ///     Runs a test for can read text page using dynamic.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1119:StatementMustNotUseUnnecessaryParenthesis", 
            Justification = "Reviewed. Suppression is OK here.")]
        [TestMethod]
        public void CanReadTextPageUsingDynamicTest()
        {
            using (var browser = new Browser())
            {
                var page = (TextPage)browser.GoTo(Content.TextTest);

                page.Result.TraceResults();

                page.Content.Should().Be("This is a test text file");
            }
        }

        /// <summary>
        ///     Runs a test for can read text page using model.
        /// </summary>
        [TestMethod]
        public void CanReadTextPageUsingModelTest()
        {
            using (var browser = new Browser())
            {
                var page = browser.GoTo<TextContentPage>(Content.TextTest);

                page.Result.TraceResults();

                page.Content.Should().Be("This is a test text file");
            }
        }
    }
}