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
        ///     Runs a test for form on dynamic page.
        /// </summary>
        [TestMethod]
        public void FormOnDynamicPageTest()
        {
            using (var browser = new Browser())
            {
                const string ColorValue = "#00ff00";
                const string DateValue = "2013-08-15";
                var datetimeValue = Guid.NewGuid().ToString();
                const string DatetimelocalValue = "2013-08-22T22:57";
                const string EmailValue = "here@there.com";
                var hiddenValue = Guid.NewGuid().ToString();
                const string MonthValue = "2013-08";
                const string NumberValue = "-1";
                var passwordValue = Guid.NewGuid().ToString();
                var rangeValue = Environment.TickCount.ToString();
                var searchValue = Guid.NewGuid().ToString();
                var telValue = Guid.NewGuid().ToString();
                var textValue = Guid.NewGuid().ToString();
                var textBlockValue = Guid.NewGuid() + Environment.NewLine + Guid.NewGuid();
                const string TimeValue = "01:59";
                var urlValue = Guid.NewGuid().ToString();
                const string WeekValue = "2013-W05";

                var page = browser.GoTo(Form.Index);

                ((IPage)page).Result.TraceResults();

                page.Color.Value = ColorValue;
                page.Date.Value = DateValue;
                page.DateTime.Value = datetimeValue;
                page.DateTimeLocal.Value = DatetimelocalValue;
                page.Email.Value = EmailValue;
                page.Hidden.Value = hiddenValue;
                page.Month.Value = MonthValue;
                page.Number.Value = NumberValue;
                page.Password.Value = passwordValue;
                page.Range.Value = rangeValue;
                page.Search.Value = searchValue;
                page.Tel.Value = telValue;
                page.Text.Value = textValue;
                page.TextBlock.Value = textBlockValue;
                page.Time.Value = TimeValue;
                page.Url.Value = urlValue;
                page.Week.Value = WeekValue;

                var postedPage = page.Submit.Click();

                ((IPage)postedPage).Result.TraceResults();

                ((string)postedPage.Color.Value).Should().Be(ColorValue);
                ((string)postedPage.Date.Value).Should().Be(DateValue);
                ((string)postedPage.DateTime.Value).Should().Be(datetimeValue);
                ((string)postedPage.DateTimeLocal.Value).Should().Be(DatetimelocalValue);
                ((string)postedPage.Email.Value).Should().Be(EmailValue);
                ((string)postedPage.Hidden.Value).Should().Be(hiddenValue);
                ((string)postedPage.Month.Value).Should().Be(MonthValue);
                ((string)postedPage.Number.Value).Should().Be(NumberValue);
                ((string)postedPage.Password.Value).Should().Be(passwordValue);
                ((string)postedPage.Range.Value).Should().Be(rangeValue);
                ((string)postedPage.Search.Value).Should().Be(searchValue);
                ((string)postedPage.Text.Value).Should().Be(textValue);
                ((string)postedPage.TextBlock.Value).Should().Be(textBlockValue);
                ((string)postedPage.Time.Value).Should().Be(TimeValue);
                ((string)postedPage.Url.Value).Should().Be(urlValue);
                ((string)postedPage.Week.Value).Should().Be(WeekValue);
            }
        }

        /// <summary>
        ///     Runs a test for form on page model.
        /// </summary>
        [TestMethod]
        public void FormOnPageModelTest()
        {
            using (var browser = new Browser())
            {
                const string ColorValue = "#00ff00";
                const string DateValue = "2013-08-15";
                var datetimeValue = Guid.NewGuid().ToString();
                const string DatetimelocalValue = "2013-08-22T22:57";
                const string EmailValue = "here@there.com";
                var hiddenValue = Guid.NewGuid().ToString();
                const string MonthValue = "2013-08";
                const string NumberValue = "-1";
                var passwordValue = Guid.NewGuid().ToString();
                var rangeValue = Environment.TickCount.ToString();
                var searchValue = Guid.NewGuid().ToString();
                var telValue = Guid.NewGuid().ToString();
                var textValue = Guid.NewGuid().ToString();
                var textBlockValue = Guid.NewGuid() + Environment.NewLine + Guid.NewGuid();
                const string TimeValue = "01:59";
                var urlValue = Guid.NewGuid().ToString();
                const string WeekValue = "2013-W05";

                var page = browser.GoTo<FormIndexPage>();

                page.Result.TraceResults();

                page.Color.Value = ColorValue;
                page.Date.Value = DateValue;
                page.DateTime.Value = datetimeValue;
                page.DateTimeLocal.Value = DatetimelocalValue;
                page.Email.Value = EmailValue;
                page.Hidden.Value = hiddenValue;
                page.Month.Value = MonthValue;
                page.Number.Value = NumberValue;
                page.Password.Value = passwordValue;
                page.Range.Value = rangeValue;
                page.Search.Value = searchValue;
                page.Tel.Value = telValue;
                page.Text.Value = textValue;
                page.TextBlock.Value = textBlockValue;
                page.Time.Value = TimeValue;
                page.Url.Value = urlValue;
                page.Week.Value = WeekValue;

                var postedPage = page.Submit.Click<FormIndexPage>();

                postedPage.Result.TraceResults();

                postedPage.Color.Value.Should().Be(ColorValue);
                postedPage.Date.Value.Should().Be(DateValue);
                postedPage.DateTime.Value.Should().Be(datetimeValue);
                postedPage.DateTimeLocal.Value.Should().Be(DatetimelocalValue);
                postedPage.Email.Value.Should().Be(EmailValue);
                postedPage.Hidden.Value.Should().Be(hiddenValue);
                postedPage.Month.Value.Should().Be(MonthValue);
                postedPage.Number.Value.Should().Be(NumberValue);
                postedPage.Password.Value.Should().Be(passwordValue);
                postedPage.Range.Value.Should().Be(rangeValue);
                postedPage.Search.Value.Should().Be(searchValue);
                postedPage.Text.Value.Should().Be(textValue);
                postedPage.TextBlock.Value.Should().Be(textBlockValue);
                postedPage.Time.Value.Should().Be(TimeValue);
                postedPage.Url.Value.Should().Be(urlValue);
                postedPage.Week.Value.Should().Be(WeekValue);
            }
        }
    }
}