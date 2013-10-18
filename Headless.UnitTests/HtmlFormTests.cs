namespace Headless.UnitTests
{
    using System.Linq;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="HtmlFormTests" />
    ///     class tests the <see cref="HtmlForm" /> class.
    /// </summary>
    [TestClass]
    public class HtmlFormTests
    {
        /// <summary>
        ///     Runs a test for action returns attribute value.
        /// </summary>
        [TestMethod]
        public void ActionReturnsAttributeValueTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test' method='get' action='https://google.com/?existing=stuff'>
            <input type='text' name='data' value='here' />
            <button type='submit' name='Submit' value='Save'>Save</button>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            form.Action.Should().Be("https://google.com/?existing=stuff");
        }

        /// <summary>
        ///     Runs a test for action target returns absolute form action.
        /// </summary>
        [TestMethod]
        public void ActionTargetReturnsAbsoluteFormActionTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test' method='get' action='https://google.com/?existing=stuff'>
            <input type='text' name='data' value='here' />
            <button type='submit' name='Submit' value='Save'>Save</button>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            form.ActionTarget.ToString().Should().Be("https://google.com/?existing=stuff");
        }

        /// <summary>
        ///     Runs a test for action target returns target location combined with relative action attribute.
        /// </summary>
        [TestMethod]
        public void ActionTargetReturnsTargetLocationCombinedWithRelativeActionAttributeTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test' method='get' action='more/here'>
            <input type='text' name='data' value='here' />
            <button type='submit' name='Submit' value='Save'>Save</button>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            form.ActionTarget.ToString().Should().Be(page.TargetLocation + "more/here");
        }

        /// <summary>
        ///     Runs a test for action target returns target location when with no action attribute.
        /// </summary>
        [TestMethod]
        public void ActionTargetReturnsTargetLocationWhenWithNoActionAttributeTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test' method='get'>
            <input type='text' name='data' value='here' />
            <button type='submit' name='Submit' value='Save'>Save</button>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            form.ActionTarget.ToString().Should().Be(page.TargetLocation.ToString());
        }

        /// <summary>
        ///     Runs a test for build get location appends to action query string.
        /// </summary>
        [TestMethod]
        public void BuildGetLocationAppendsToActionQueryStringTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test' method='get' action='https://google.com/?existing=stuff'>
            <input type='text' name='data' value='here' />
            <button type='submit' name='Submit' value='Save'>Save</button>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            var location = form.BuildGetLocation(null);

            location.ToString().Should().Be("https://google.com/?existing=stuff&data=here");
        }

        /// <summary>
        ///     Runs a test for build get location excludes button when submitted directly.
        /// </summary>
        [TestMethod]
        public void BuildGetLocationExcludesButtonWhenSubmittedDirectlyTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test' method='get' action='https://google.com/'>
            <button type='submit' name='Submit' value='Save'>Save</button>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            var location = form.BuildGetLocation(null);

            location.ToString().Should().Be("https://google.com/");
        }

        /// <summary>
        ///     Runs a test for build get location returns action with clicked button and no other form elements.
        /// </summary>
        [TestMethod]
        public void BuildGetLocationReturnsActionWithClickedButtonAndNoOtherFormElementsTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test' method='get' action='https://google.com/'>
            <button type='submit' name='Submit' value='Save'>Save</button>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");
            var button = page.Find<HtmlButton>().Singular();

            var location = form.BuildGetLocation(button);

            location.ToString().Should().Be("https://google.com/?Submit=Save");
        }

        /// <summary>
        ///     Runs a test for build get location returns action with multiple post elements.
        /// </summary>
        [TestMethod]
        public void BuildGetLocationReturnsActionWithMultiplePostElementsTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test' method='get' action='https://google.com/'>
            <input type='text' name='data' value='here' />
            <input type='hidden' name='more' value='entries' />
            <button type='submit' name='Submit' value='Save'>Save</button>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            var location = form.BuildGetLocation(null);

            location.ToString().Should().Be("https://google.com/?data=here&more=entries");
        }

        /// <summary>
        ///     Runs a test for build get location returns action with no form elements.
        /// </summary>
        [TestMethod]
        public void BuildGetLocationReturnsActionWithNoFormElementsTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test' method='get' action='https://google.com/'>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            var location = form.BuildGetLocation(null);

            location.ToString().Should().Be("https://google.com/");
        }

        /// <summary>
        ///     Runs a test for build get location returns action with post elements.
        /// </summary>
        [TestMethod]
        public void BuildGetLocationReturnsActionWithPostElementsTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test' method='get' action='https://google.com/'>
            <input type='text' name='data' value='here' />
            <button type='submit' name='Submit' value='Save'>Save</button>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            var location = form.BuildGetLocation(null);

            location.ToString().Should().Be("https://google.com/?data=here");
        }

        /// <summary>
        ///     Runs a test for build get location returns action with URL encoded post elements.
        /// </summary>
        [TestMethod]
        public void BuildGetLocationReturnsActionWithUrlEncodedPostElementsTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test' method='get' action='https://google.com/'>
            <input type='text' name='da ta' value='he re' />
            <button type='submit' name='Submit' value='Save'>Save</button>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            var location = form.BuildGetLocation(null);

            location.ToString().Should().Be("https://google.com/?da+ta=he+re");
        }

        /// <summary>
        ///     Runs a test for build post entries excludes buttons.
        /// </summary>
        [TestMethod]
        public void BuildPostEntriesExcludesButtonsTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test' method='get' action='https://google.com/?existing=stuff'>
            <input type='text' name='data' value='here' />
            <button type='submit' name='Submit' value='Save'>Save</button>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            var entries = form.BuildPostParameters(null).ToList();

            entries.Count.Should().Be(1);
            entries.Any(x => x.Name == "Submit").Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for build post entries includes clicked button.
        /// </summary>
        [TestMethod]
        public void BuildPostEntriesIncludesClickedButtonTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test' method='get' action='https://google.com/?existing=stuff'>
            <input type='text' name='data' value='here' />
            <button type='submit' name='Submit' value='Save'>Save</button>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");
            var button = page.Find<HtmlButton>().Singular();

            var entries = form.BuildPostParameters(button).ToList();

            entries.Count.Should().Be(2);
            entries.Any(x => x.Name == "data").Should().BeTrue();
            entries.Any(x => x.Name == "Submit").Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for method returns attribute value.
        /// </summary>
        [TestMethod]
        public void MethodReturnsAttributeValueTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test' method='get' action='https://google.com/?existing=stuff'>
            <input type='text' name='data' value='here' />
            <button type='submit' name='Submit' value='Save'>Save</button>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            form.Method.Should().Be("get");
        }

        /// <summary>
        ///     Runs a test for name returns attribute value.
        /// </summary>
        [TestMethod]
        public void NameReturnsAttributeValueTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test' method='get' action='https://google.com/?existing=stuff'>
            <input type='text' name='data' value='here' />
            <button type='submit' name='Submit' value='Save'>Save</button>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            form.Name.Should().Be("Test");
        }

        /// <summary>
        ///     Runs a test for target returns attribute value.
        /// </summary>
        [TestMethod]
        public void TargetReturnsAttributeValueTest()
        {
            const string Html = @"
<html>
    <head />
    <body>
        <form name='Test' method='get' target='_blank' action='https://google.com/?existing=stuff'>
            <input type='text' name='data' value='here' />
            <button type='submit' name='Submit' value='Save'>Save</button>
        </form>
    </body>
</html>";

            var page = new HtmlPageStub(Html);

            var form = page.Find<HtmlForm>().ByName("Test");

            form.Target.Should().Be("_blank");
        }
    }
}