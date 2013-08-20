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
        public HtmlTextElement Color
        {
            get
            {
                return Find<HtmlTextElement>().FindByName("Color");
            }
        }

        /// <summary>
        ///     Gets the date.
        /// </summary>
        /// <value>
        ///     The date.
        /// </value>
        public HtmlTextElement Date
        {
            get
            {
                return Find<HtmlTextElement>().FindByName("Date");
            }
        }

        /// <summary>
        ///     Gets the date time.
        /// </summary>
        /// <value>
        ///     The date time.
        /// </value>
        public HtmlTextElement DateTime
        {
            get
            {
                return Find<HtmlTextElement>().FindByName("DateTime");
            }
        }

        /// <summary>
        ///     Gets the date time local.
        /// </summary>
        /// <value>
        ///     The date time local.
        /// </value>
        public HtmlTextElement DateTimeLocal
        {
            get
            {
                return Find<HtmlTextElement>().FindByName("DateTimeLocal");
            }
        }

        /// <summary>
        ///     Gets the email.
        /// </summary>
        /// <value>
        ///     The email.
        /// </value>
        public HtmlTextElement Email
        {
            get
            {
                return Find<HtmlTextElement>().FindByName("Email");
            }
        }

        /// <summary>
        ///     Gets the text.
        /// </summary>
        /// <value>
        ///     The text.
        /// </value>
        public HtmlTextElement Hidden
        {
            get
            {
                return Find<HtmlTextElement>().FindByName("Hidden");
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
        public HtmlTextElement Month
        {
            get
            {
                return Find<HtmlTextElement>().FindByName("Month");
            }
        }

        /// <summary>
        ///     Gets the number.
        /// </summary>
        /// <value>
        ///     The number.
        /// </value>
        public HtmlTextElement Number
        {
            get
            {
                return Find<HtmlTextElement>().FindByName("Number");
            }
        }

        /// <summary>
        ///     Gets the password.
        /// </summary>
        /// <value>
        ///     The password.
        /// </value>
        public HtmlTextElement Password
        {
            get
            {
                return Find<HtmlTextElement>().FindByName("Password");
            }
        }

        /// <summary>
        ///     Gets the range.
        /// </summary>
        /// <value>
        ///     The range.
        /// </value>
        public HtmlTextElement Range
        {
            get
            {
                return Find<HtmlTextElement>().FindByName("Range");
            }
        }

        /// <summary>
        ///     Gets the search.
        /// </summary>
        /// <value>
        ///     The search.
        /// </value>
        public HtmlTextElement Search
        {
            get
            {
                return Find<HtmlTextElement>().FindByName("Search");
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
        public HtmlTextElement Tel
        {
            get
            {
                return Find<HtmlTextElement>().FindByName("Tel");
            }
        }

        /// <summary>
        ///     Gets the text.
        /// </summary>
        /// <value>
        ///     The text.
        /// </value>
        public HtmlTextElement Text
        {
            get
            {
                return Find<HtmlTextElement>().FindByName("Text");
            }
        }

        /// <summary>
        ///     Gets the text block.
        /// </summary>
        /// <value>
        ///     The text block.
        /// </value>
        public HtmlTextElement TextBlock
        {
            get
            {
                return Find<HtmlTextElement>().FindByName("TextBlock");
            }
        }

        /// <summary>
        ///     Gets the time.
        /// </summary>
        /// <value>
        ///     The time.
        /// </value>
        public HtmlTextElement Time
        {
            get
            {
                return Find<HtmlTextElement>().FindByName("Time");
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
        public HtmlTextElement Url
        {
            get
            {
                return Find<HtmlTextElement>().FindByName("Url");
            }
        }

        /// <summary>
        ///     Gets the week.
        /// </summary>
        /// <value>
        ///     The week.
        /// </value>
        public HtmlTextElement Week
        {
            get
            {
                return Find<HtmlTextElement>().FindByName("Week");
            }
        }
    }
}