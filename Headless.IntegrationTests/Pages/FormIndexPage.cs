namespace Headless.IntegrationTests.Pages
{
    using System;
    using System.Linq;

    /// <summary>
    ///     The <see cref="FormIndexPage" />
    ///     class is used to wrap the form test page.
    /// </summary>
    public class FormIndexPage : HtmlPage
    {
        /// <summary>
        ///     Gets the color.
        /// </summary>
        /// <value>
        ///     The color.
        /// </value>
        public HtmlInput Color
        {
            get
            {
                return Find<HtmlInput>().FindByName("Color");
            }
        }

        /// <summary>
        ///     Gets the date.
        /// </summary>
        /// <value>
        ///     The date.
        /// </value>
        public HtmlInput Date
        {
            get
            {
                return Find<HtmlInput>().FindByName("Date");
            }
        }

        /// <summary>
        ///     Gets the date time.
        /// </summary>
        /// <value>
        ///     The date time.
        /// </value>
        public HtmlInput DateTime
        {
            get
            {
                return Find<HtmlInput>().FindByName("DateTime");
            }
        }

        /// <summary>
        ///     Gets the date time local.
        /// </summary>
        /// <value>
        ///     The date time local.
        /// </value>
        public HtmlInput DateTimeLocal
        {
            get
            {
                return Find<HtmlInput>().FindByName("DateTimeLocal");
            }
        }

        /// <summary>
        ///     Gets the email.
        /// </summary>
        /// <value>
        ///     The email.
        /// </value>
        public HtmlInput Email
        {
            get
            {
                return Find<HtmlInput>().FindByName("Email");
            }
        }

        /// <summary>
        ///     Gets the text.
        /// </summary>
        /// <value>
        ///     The text.
        /// </value>
        public HtmlInput Hidden
        {
            get
            {
                return Find<HtmlInput>().FindByName("Hidden");
            }
        }

        /// <summary>
        ///     Gets the location.
        /// </summary>
        public override Uri Location
        {
            get
            {
                return Form.Index;
            }
        }

        /// <summary>
        ///     Gets the month.
        /// </summary>
        /// <value>
        ///     The month.
        /// </value>
        public HtmlInput Month
        {
            get
            {
                return Find<HtmlInput>().FindByName("Month");
            }
        }

        /// <summary>
        ///     Gets the number.
        /// </summary>
        /// <value>
        ///     The number.
        /// </value>
        public HtmlInput Number
        {
            get
            {
                return Find<HtmlInput>().FindByName("Number");
            }
        }

        /// <summary>
        ///     Gets the password.
        /// </summary>
        /// <value>
        ///     The password.
        /// </value>
        public HtmlInput Password
        {
            get
            {
                return Find<HtmlInput>().FindByName("Password");
            }
        }

        /// <summary>
        ///     Gets the radio.
        /// </summary>
        /// <value>
        ///     The radio.
        /// </value>
        public HtmlRadioButton Radio
        {
            get
            {
                return Find<HtmlRadioButton>().FindByName("Radio");
            }
        }

        /// <summary>
        ///     Gets the range.
        /// </summary>
        /// <value>
        ///     The range.
        /// </value>
        public HtmlInput Range
        {
            get
            {
                return Find<HtmlInput>().FindByName("Range");
            }
        }

        /// <summary>
        ///     Gets the search.
        /// </summary>
        /// <value>
        ///     The search.
        /// </value>
        public HtmlInput Search
        {
            get
            {
                return Find<HtmlInput>().FindByName("Search");
            }
        }

        /// <summary>
        ///     Gets the submit.
        /// </summary>
        /// <value>
        ///     The submit.
        /// </value>
        public HtmlButton Submit
        {
            get
            {
                return Find<HtmlButton>().All().Single();
            }
        }

        /// <summary>
        ///     Gets the tel.
        /// </summary>
        /// <value>
        ///     The tel.
        /// </value>
        public HtmlInput Tel
        {
            get
            {
                return Find<HtmlInput>().FindByName("Tel");
            }
        }

        /// <summary>
        ///     Gets the text.
        /// </summary>
        /// <value>
        ///     The text.
        /// </value>
        public HtmlInput Text
        {
            get
            {
                return Find<HtmlInput>().FindByName("Text");
            }
        }

        /// <summary>
        ///     Gets the text block.
        /// </summary>
        /// <value>
        ///     The text block.
        /// </value>
        public HtmlInput TextBlock
        {
            get
            {
                return Find<HtmlInput>().FindByName("TextBlock");
            }
        }

        /// <summary>
        ///     Gets the time.
        /// </summary>
        /// <value>
        ///     The time.
        /// </value>
        public HtmlInput Time
        {
            get
            {
                return Find<HtmlInput>().FindByName("Time");
            }
        }

        /// <summary>
        ///     Gets the toggle.
        /// </summary>
        /// <value>
        ///     The toggle.
        /// </value>
        public HtmlCheckBox Toggle
        {
            get
            {
                return Find<HtmlCheckBox>().FindByName("Toggle");
            }
        }

        /// <summary>
        ///     Gets the URL.
        /// </summary>
        /// <value>
        ///     The URL.
        /// </value>
        public HtmlInput Url
        {
            get
            {
                return Find<HtmlInput>().FindByName("Url");
            }
        }

        /// <summary>
        ///     Gets the week.
        /// </summary>
        /// <value>
        ///     The week.
        /// </value>
        public HtmlInput Week
        {
            get
            {
                return Find<HtmlInput>().FindByName("Week");
            }
        }
    }
}