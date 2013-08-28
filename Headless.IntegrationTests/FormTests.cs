namespace Headless.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
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
        ///     Runs a test for drop down on dynamic page.
        /// </summary>
        [TestMethod]
        public void DropDownOnDynamicPageTest()
        {
            var testValues = new[]
            {
                string.Empty, "First", "Second", "Third", "Fourth", "Fifth"
            };

            using (var browser = new Browser())
            {
                var page = browser.GoTo(Form.Index);

                ((IPage)page).Result.TraceResults();

                var dropDown = page.DropDown as HtmlList;

                dropDown.Should().NotBeNull();
                dropDown.Values.SequenceEqual(testValues).Should().BeTrue();

                ((string)page.DropDown.Value).Should().BeNullOrEmpty();

                for (var index = 0; index < testValues.Length; index++)
                {
                    page.DropDown.Value = testValues[index];

                    page = page.Submit.Click();

                    ((string)page.DropDown.Value).Should().Be(testValues[index]);
                }

                page.DropDown.Value = null;

                page = page.Submit.Click();

                ((string)page.DropDown.Value).Should().BeNullOrEmpty();
            }
        }

        /// <summary>
        ///     Runs a test for drop down on page model.
        /// </summary>
        [TestMethod]
        public void DropDownOnPageModelTest()
        {
            var testValues = new[]
            {
                string.Empty, "First", "Second", "Third", "Fourth", "Fifth"
            };

            using (var browser = new Browser())
            {
                var page = browser.GoTo<FormIndexPage>();

                page.Result.TraceResults();

                page.DropDown.Values.SequenceEqual(testValues).Should().BeTrue();

                page.DropDown.Value.Should().BeNullOrEmpty();

                for (var index = 0; index < testValues.Length; index++)
                {
                    page.DropDown.Value = testValues[index];

                    page = page.Submit.Click<FormIndexPage>();

                    page.DropDown.Value.Should().Be(testValues[index]);
                }

                page.DropDown.Value = null;

                page = page.Submit.Click<FormIndexPage>();

                page.DropDown.Value.Should().BeNullOrEmpty();
            }
        }

        /// <summary>
        ///     Runs a test for dynamic page can submit from form directly including button reference.
        /// </summary>
        [TestMethod]
        public void DynamicPageCanSubmitFromFormDirectlyIncludingButtonReferenceTest()
        {
            using (var browser = new Browser())
            {
                var textValue = Guid.NewGuid().ToString();

                var page = browser.GoTo(Form.Index);

                ((IPage)page).Result.TraceResults();

                ((bool)page.Toggle.Checked).Should().BeFalse();

                page.Text.Value = textValue;

                var postedPage = page.TestForm.Submit(page.Submit);

                ((IPage)postedPage).Result.TraceResults();

                ((string)postedPage.Text.Value).Should().Be(textValue);
            }
        }

        /// <summary>
        ///     Runs a test for dynamic page can submit from form directly.
        /// </summary>
        [TestMethod]
        public void DynamicPageCanSubmitFromFormDirectlyTest()
        {
            using (var browser = new Browser())
            {
                var textValue = Guid.NewGuid().ToString();

                var page = browser.GoTo(Form.Index);

                ((IPage)page).Result.TraceResults();

                ((bool)page.Toggle.Checked).Should().BeFalse();

                page.Text.Value = textValue;

                var postedPage = page.TestForm.Submit();

                ((IPage)postedPage).Result.TraceResults();

                ((string)postedPage.Text.Value).Should().Be(textValue);
            }
        }

        /// <summary>
        ///     Runs a test for files on dynamic page.
        /// </summary>
        [TestMethod]
        public void FilesOnDynamicPageTest()
        {
            var firstFile = Path.GetTempFileName();
            var secondFile = Path.GetTempFileName();

            try
            {
                File.WriteAllText(firstFile, Guid.NewGuid().ToString());
                File.WriteAllText(secondFile, Guid.NewGuid().ToString());

                using (var browser = new Browser())
                {
                    var textValue = Guid.NewGuid().ToString();

                    var page = browser.GoTo(Form.Files);

                    ((IPage)page).Result.TraceResults();

                    page.SomeData.Value = textValue;
                    page.files1.Value = firstFile;
                    page.files2.Value = secondFile;

                    var postedPage = page.Submit.Click();

                    ((IPage)postedPage).Result.TraceResults();

                    ((string)postedPage.SomeData.Value).Should().Be(textValue);
                    ((string)postedPage.FileCount.Text).Should().Be("2");
                }
            }
            finally
            {
                File.Delete(firstFile);
                File.Delete(secondFile);
            }
        }

        /// <summary>
        ///     Runs a test for files on static page.
        /// </summary>
        [TestMethod]
        public void FilesOnStaticPageTest()
        {
            var firstFile = Path.GetTempFileName();
            var secondFile = Path.GetTempFileName();

            try
            {
                File.WriteAllText(firstFile, Guid.NewGuid().ToString());
                File.WriteAllText(secondFile, Guid.NewGuid().ToString());

                using (var browser = new Browser())
                {
                    var textValue = Guid.NewGuid().ToString();

                    var page = browser.GoTo<FormFilePage>();

                    page.Result.TraceResults();

                    page.SomeData.Value = textValue;
                    page.File1.Value = firstFile;
                    page.File2.Value = secondFile;

                    var postedPage = page.Submit.Click<FormFilePage>();

                    postedPage.Result.TraceResults();

                    postedPage.SomeData.Value.Should().Be(textValue);
                    postedPage.FileCount.Text.Should().Be("2");
                }
            }
            finally
            {
                File.Delete(firstFile);
                File.Delete(secondFile);
            }
        }

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
                var rangeValue = Environment.TickCount.ToString(CultureInfo.InvariantCulture);
                var searchValue = Guid.NewGuid().ToString();
                var telValue = Guid.NewGuid().ToString();
                var textValue = Guid.NewGuid().ToString();
                var textBlockValue = Guid.NewGuid() + Environment.NewLine + Guid.NewGuid();
                const string TimeValue = "01:59";
                var urlValue = Guid.NewGuid().ToString();
                const string WeekValue = "2013-W05";
                const bool Toggle = true;

                var page = browser.GoTo(Form.Index);

                ((IPage)page).Result.TraceResults();

                ((bool)page.Toggle.Checked).Should().BeFalse();

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
                page.Toggle.Checked = Toggle;

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
                ((bool)postedPage.Toggle.Checked).Should().Be(Toggle);
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
                var rangeValue = Environment.TickCount.ToString(CultureInfo.InvariantCulture);
                var searchValue = Guid.NewGuid().ToString();
                var telValue = Guid.NewGuid().ToString();
                var textValue = Guid.NewGuid().ToString();
                var textBlockValue = Guid.NewGuid() + Environment.NewLine + Guid.NewGuid();
                const string TimeValue = "01:59";
                var urlValue = Guid.NewGuid().ToString();
                const string WeekValue = "2013-W05";
                const bool Toggle = true;

                var page = browser.GoTo<FormIndexPage>();

                page.Result.TraceResults();

                page.Toggle.Checked.Should().BeFalse();

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
                page.Toggle.Checked = Toggle;

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
                postedPage.Toggle.Checked.Should().Be(Toggle);
            }
        }

        /// <summary>
        ///     Runs a test for list on dynamic page.
        /// </summary>
        [TestMethod]
        public void ListOnDynamicPageTest()
        {
            var testValues = new[]
            {
                "First", "Second", "Third", "Fourth", "Fifth"
            };

            using (var browser = new Browser())
            {
                var page = browser.GoTo(Form.Index);

                ((IPage)page).Result.TraceResults();

                var list = page.List as HtmlList;

                list.Should().NotBeNull();
                list.Values.SequenceEqual(testValues).Should().BeTrue();

                ((string)page.List.Value).Should().BeNullOrEmpty();

                for (var index = 0; index < testValues.Length; index++)
                {
                    var expectedValues = new List<string>();

                    for (var innerLoop = 0; innerLoop <= index; innerLoop++)
                    {
                        page.List.Select(testValues[innerLoop]);

                        expectedValues.Add(testValues[innerLoop]);
                    }

                    page = page.Submit.Click();

                    var values = ((IEnumerable<string>)page.List.SelectedValues).ToList();

                    values.SequenceEqual(expectedValues).Should().BeTrue();
                }

                page.List.Value = null;

                page = page.Submit.Click();

                ((string)page.List.Value).Should().BeNullOrEmpty();
            }
        }

        /// <summary>
        ///     Runs a test for list on page model.
        /// </summary>
        [TestMethod]
        public void ListOnPageModelTest()
        {
            var testValues = new[]
            {
                "First", "Second", "Third", "Fourth", "Fifth"
            };

            using (var browser = new Browser())
            {
                var page = browser.GoTo<FormIndexPage>();

                page.Result.TraceResults();

                page.List.Values.SequenceEqual(testValues).Should().BeTrue();

                page.List.Value.Should().BeNullOrEmpty();

                for (var index = 0; index < testValues.Length; index++)
                {
                    var expectedValues = new List<string>();

                    for (var innerLoop = 0; innerLoop <= index; innerLoop++)
                    {
                        page.List.Select(testValues[innerLoop]);

                        expectedValues.Add(testValues[innerLoop]);
                    }

                    page = page.Submit.Click<FormIndexPage>();

                    var values = page.List.SelectedValues.ToList();

                    values.SequenceEqual(expectedValues).Should().BeTrue();
                }

                page.List.Value = null;

                page = page.Submit.Click<FormIndexPage>();

                page.List.Value.Should().BeNullOrEmpty();
            }
        }

        /// <summary>
        ///     Runs a test for radio buttons on dynamic page.
        /// </summary>
        [TestMethod]
        public void RadioButtonsOnDynamicPageTest()
        {
            var testValues = new[]
            {
                "First", "Second", "Third", "Fourth", "Fifth"
            };

            using (var browser = new Browser())
            {
                var page = browser.GoTo(Form.Index);

                ((IPage)page).Result.TraceResults();

                var radio = page.Radio as HtmlRadioButton;

                radio.Should().NotBeNull();
                radio.Values.SequenceEqual(testValues).Should().BeTrue();

                ((string)page.Radio.Value).Should().BeNull();

                for (var index = 0; index < testValues.Length; index++)
                {
                    page.Radio.Value = testValues[index];

                    page = page.Submit.Click();

                    ((string)page.Radio.Value).Should().Be(testValues[index]);
                }

                page.Radio.Value = null;

                page = page.Submit.Click();

                ((string)page.Radio.Value).Should().BeNull();
            }
        }

        /// <summary>
        ///     Runs a test for radio buttons on page model.
        /// </summary>
        [TestMethod]
        public void RadioButtonsOnPageModelTest()
        {
            var testValues = new[]
            {
                "First", "Second", "Third", "Fourth", "Fifth"
            };

            using (var browser = new Browser())
            {
                var page = browser.GoTo<FormIndexPage>();

                page.Result.TraceResults();

                page.Radio.Values.SequenceEqual(testValues).Should().BeTrue();

                page.Radio.Value.Should().BeNull();

                for (var index = 0; index < testValues.Length; index++)
                {
                    page.Radio.Value = testValues[index];

                    page = page.Submit.Click<FormIndexPage>();

                    page.Radio.Value.Should().Be(testValues[index]);
                }

                page.Radio.Value = null;

                page = page.Submit.Click<FormIndexPage>();

                page.Radio.Value.Should().BeNull();
            }
        }
    }
}