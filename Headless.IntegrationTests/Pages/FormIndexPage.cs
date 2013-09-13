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
        /// <inheritdoc />
        public override bool IsOn(Uri location, UriComponents compareWith)
        {
            var value = base.IsOn(location, compareWith);

            if (value)
            {
                return true;
            }

            var additionalAddress = new Uri(Config.BaseWebAddress, "form");

            // We need additional logic to test other routes that MVC could use
            return additionalAddress.IsBaseOf(location);
        }

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
                return Find<HtmlInput>().ByName("Color");
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
                return Find<HtmlInput>().ByName("Date");
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
                return Find<HtmlInput>().ByName("DateTime");
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
                return Find<HtmlInput>().ByName("DateTimeLocal");
            }
        }

        /// <summary>
        ///     Gets the drop down.
        /// </summary>
        /// <value>
        ///     The drop down.
        /// </value>
        public HtmlList DropDown
        {
            get
            {
                return Find<HtmlList>().ByName("DropDown");
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
                return Find<HtmlInput>().ByName("Email");
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
                return Find<HtmlInput>().ByName("Hidden");
            }
        }

        /// <summary>
        ///     Gets the list.
        /// </summary>
        /// <value>
        ///     The list.
        /// </value>
        public HtmlList List
        {
            get
            {
                return Find<HtmlList>().ByName("List");
            }
        }

        /// <summary>
        ///     Gets the location.
        /// </summary>
        public override Uri TargetLocation
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
                return Find<HtmlInput>().ByName("Month");
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
                return Find<HtmlInput>().ByName("Number");
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
                return Find<HtmlInput>().ByName("Password");
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
                return Find<HtmlRadioButton>().ByName("Radio");
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
                return Find<HtmlInput>().ByName("Range");
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
                return Find<HtmlInput>().ByName("Search");
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
                return Find<HtmlInput>().ByName("Tel");
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
                return Find<HtmlInput>().ByName("Text");
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
                return Find<HtmlInput>().ByName("TextBlock");
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
                return Find<HtmlInput>().ByName("Time");
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
                return Find<HtmlCheckBox>().ByName("Toggle");
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
                return Find<HtmlInput>().ByName("Url");
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
                return Find<HtmlInput>().ByName("Week");
            }
        }
    }
}