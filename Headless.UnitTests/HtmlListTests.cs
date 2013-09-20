namespace Headless.UnitTests
{
    using System.Linq;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="HtmlListTests" />
    ///     class tests the <see cref="HtmlList" /> class.
    /// </summary>
    [TestClass]
    public class HtmlListTests
    {
        /// <summary>
        ///     Runs a test for build post data uses option text when value is empty.
        /// </summary>
        [TestMethod]
        public void BuildPostDataUsesOptionTextWhenValueIsEmptyTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <select name='Data'>
               <option>1</option>
            </select>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");
            var list = form.Find<HtmlList>().ByName("Data");

            list.Value = "1";

            var parameters = form.BuildPostParameters(null).ToList();

            parameters.Should().Contain(x => x.Name == "Data" && x.Value == "1");
        }

        /// <summary>
        ///     Runs a test for value should select option matching text when value is empty.
        /// </summary>
        [TestMethod]
        public void ValueShouldSelectOptionMatchingTextWhenValueIsEmptyTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <select name='Data'>
               <option>1</option>
            </select>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var list = page.Find<HtmlList>().ByName("Data");

            list.Value = "1";

            var option = list.Find<AnyHtmlElement>().ByText("1");

            var actual = option.AttributeExists("selected");

            actual.Should().BeTrue();
        }
    }
}