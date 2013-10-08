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
        ///     Runs a test for deselect should match item by text when it has value.
        /// </summary>
        [TestMethod]
        public void DeselectShouldMatchItemByTextWhenItHasValueTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <select name='Data' multiple>
               <option value='1' selected>Test</option>
               <option value='2' selected>Next</option>
            </select>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");
            var list = form.Find<HtmlList>().ByName("Data");

            var selectedValues = list.SelectedValues.ToList();

            selectedValues.Count.Should().Be(2);

            list.Deselect("Next");

            selectedValues = list.SelectedValues.ToList();

            selectedValues.Count.Should().Be(1);
            selectedValues[0].Should().Be("1");
        }

        /// <summary>
        ///     Runs a test for indexer should match list item by text when it has value.
        /// </summary>
        [TestMethod]
        public void IndexerShouldMatchListItemByTextWhenItHasValueTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <select name='Data'>
               <option value='1'>Test</option>
            </select>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");
            var list = form.Find<HtmlList>().ByName("Data");

            var item = list["Test"];

            item.Should().NotBeNull();
            item.Value.Should().Be("1");
        }

        /// <summary>
        ///     Runs a test for select should match item by text when it has value.
        /// </summary>
        [TestMethod]
        public void SelectShouldMatchItemByTextWhenItHasValueTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <select name='Data'>
               <option value='1'>Test</option>
               <option value='2'>Next</option>
            </select>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");
            var list = form.Find<HtmlList>().ByName("Data");

            list.Select("Next");

            var selectedValues = list.SelectedValues.ToList();

            selectedValues.Count.Should().Be(1);
            selectedValues[0].Should().Be("2");
        }

        /// <summary>
        ///     Runs a test for selected items returns empty with multiselect and no explicitly selected items.
        /// </summary>
        [TestMethod]
        public void SelectedItemsReturnsEmptyWithMultiselectAndNoExplicitlySelectedItemsTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <select name='Data' multiple>
               <option>1</option>
               <option>2</option>
            </select>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");
            var list = form.Find<HtmlList>().ByName("Data");

            var values = list.SelectedItems.ToList();

            values.Count.Should().Be(0);
        }

        /// <summary>
        ///     Runs a test for selected items returns explicitly selected item.
        /// </summary>
        [TestMethod]
        public void SelectedItemsReturnsExplicitlySelectedItemTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <select name='Data'>
               <option>1</option>
               <option selected>2</option>
            </select>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");
            var list = form.Find<HtmlList>().ByName("Data");

            var values = list.SelectedItems.ToList();

            values.Count.Should().Be(1);
            values[0].PostValue.Should().Be("2");
        }

        /// <summary>
        ///     Runs a test for selected items returns implicitly selected item in drop down list.
        /// </summary>
        [TestMethod]
        public void SelectedItemsReturnsImplicitlySelectedItemInDropDownListTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <select name='Data'>
               <option>1</option>
               <option>2</option>
            </select>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");
            var list = form.Find<HtmlList>().ByName("Data");

            var values = list.SelectedItems.ToList();

            values.Count.Should().Be(1);
            values[0].PostValue.Should().Be("1");
        }

        /// <summary>
        ///     Runs a test for selected values returns empty with multiselect and no explicitly selected items.
        /// </summary>
        [TestMethod]
        public void SelectedValuesReturnsEmptyWithMultiselectAndNoExplicitlySelectedItemsTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <select name='Data' multiple>
               <option>1</option>
               <option>2</option>
            </select>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");
            var list = form.Find<HtmlList>().ByName("Data");

            var values = list.SelectedValues.ToList();

            values.Count.Should().Be(0);
        }

        /// <summary>
        ///     Runs a test for selected values returns explicitly selected item.
        /// </summary>
        [TestMethod]
        public void SelectedValuesReturnsExplicitlySelectedItemTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <select name='Data'>
               <option>1</option>
               <option selected>2</option>
            </select>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");
            var list = form.Find<HtmlList>().ByName("Data");

            var values = list.SelectedValues.ToList();

            values.Count.Should().Be(1);
            values[0].Should().Be("2");
        }

        /// <summary>
        ///     Runs a test for selected values returns implicitly selected item in drop down list.
        /// </summary>
        [TestMethod]
        public void SelectedValuesReturnsImplicitlySelectedItemInDropDownListTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <select name='Data'>
               <option>1</option>
               <option>2</option>
            </select>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");
            var list = form.Find<HtmlList>().ByName("Data");

            var values = list.SelectedValues.ToList();

            values.Count.Should().Be(1);
            values[0].Should().Be("1");
        }

        /// <summary>
        ///     Runs a test for selected values should return text when selected value is empty.
        /// </summary>
        [TestMethod]
        public void SelectedValuesShouldReturnTextWhenSelectedValueIsEmptyTest()
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
            const string Expected = "1";

            var page = new HtmlPageStub(Html);

            var list = page.Find<HtmlList>().ByName("Data");

            list.Value = Expected;

            var values = list.SelectedValues.ToList();

            values.Count.Should().Be(1);
            values[0].Should().Be(Expected);
        }

        /// <summary>
        ///     Runs a test for setting selected values should match item by text when it has value.
        /// </summary>
        [TestMethod]
        public void SettingSelectedValuesShouldMatchItemByTextWhenItHasValueTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <select name='Data' multiple>
               <option value='1'>Test</option>
               <option value='2'>Next</option>
            </select>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");
            var list = form.Find<HtmlList>().ByName("Data");

            list.SelectedValues.Count().Should().Be(0);

            list.SelectedValues = new[]
            {
                "Test", "Next"
            };

            var selectedValues = list.SelectedValues.ToList();

            selectedValues.Count.Should().Be(2);
        }

        /// <summary>
        ///     Runs a test for setting value should match item by text when it has value.
        /// </summary>
        [TestMethod]
        public void SettingValueShouldMatchItemByTextWhenItHasValueTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <select name='Data' multiple>
               <option value='1'>Test</option>
               <option value='2'>Next</option>
            </select>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");
            var list = form.Find<HtmlList>().ByName("Data");

            list.SelectedValues.Count().Should().Be(0);

            list.Value = "Next";

            var selectedValues = list.SelectedValues.ToList();

            selectedValues.Count.Should().Be(1);
            selectedValues[0].Should().Be("2");
        }

        /// <summary>
        ///     Runs a test for value returns empty with multiselect and no explicitly selected items.
        /// </summary>
        [TestMethod]
        public void ValueReturnsEmptyWithMultiselectAndNoExplicitlySelectedItemsTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <select name='Data' multiple>
               <option>1</option>
               <option>2</option>
            </select>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");
            var list = form.Find<HtmlList>().ByName("Data");

            list.Value.Should().BeEmpty();
        }

        /// <summary>
        ///     Runs a test for value returns explicitly selected item.
        /// </summary>
        [TestMethod]
        public void ValueReturnsExplicitlySelectedItemTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <select name='Data'>
               <option>1</option>
               <option selected>2</option>
            </select>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");
            var list = form.Find<HtmlList>().ByName("Data");

            list.Value.Should().Be("2");
        }

        /// <summary>
        ///     Runs a test for value returns implicitly selected item in drop down list.
        /// </summary>
        [TestMethod]
        public void ValueReturnsImplicitlySelectedItemInDropDownListTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test'>
            <select name='Data'>
               <option>1</option>
               <option>2</option>
            </select>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");
            var list = form.Find<HtmlList>().ByName("Data");

            list.Value.Should().Be("1");
        }

        /// <summary>
        ///     Runs a test for value should return text when selected value is empty.
        /// </summary>
        [TestMethod]
        public void ValueShouldReturnTextWhenSelectedValueIsEmptyTest()
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
            const string Expected = "1";

            var page = new HtmlPageStub(Html);

            var list = page.Find<HtmlList>().ByName("Data");

            list.Value = Expected;

            list.Value.Should().Be(Expected);
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

        /// <summary>
        ///     Runs a test for values should return text when value is empty.
        /// </summary>
        [TestMethod]
        public void ValuesShouldReturnTextWhenValueIsEmptyTest()
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
            const string Expected = "1";

            var page = new HtmlPageStub(Html);

            var list = page.Find<HtmlList>().ByName("Data");

            var values = list.Values.ToList();

            values.Count.Should().Be(1);
            values[0].Should().Be(Expected);
        }
    }
}