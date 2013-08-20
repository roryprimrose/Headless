namespace Headless.DemoSite.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     The <see cref="FormData" />
    ///     class is used for testing form interations.
    /// </summary>
    public class FormData
    {
        /// <summary>
        ///     Gets or sets the color.
        /// </summary>
        /// <value>
        ///     The color.
        /// </value>
        public string Color
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the date.
        /// </summary>
        /// <value>
        ///     The date.
        /// </value>
        public string Date
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the date time.
        /// </summary>
        /// <value>
        ///     The date time.
        /// </value>
        public string DateTime
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the date time local.
        /// </summary>
        /// <value>
        ///     The date time local.
        /// </value>
        public string DateTimeLocal
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        /// <value>
        ///     The email.
        /// </value>
        [DataType(DataType.EmailAddress)]
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the hidden.
        /// </summary>
        /// <value>
        ///     The hidden.
        /// </value>
        public string Hidden
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the month.
        /// </summary>
        /// <value>
        ///     The month.
        /// </value>
        public string Month
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the number.
        /// </summary>
        /// <value>
        ///     The number.
        /// </value>
        public int Number
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        /// <value>
        ///     The password.
        /// </value>
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the range.
        /// </summary>
        /// <value>
        ///     The range.
        /// </value>
        public string Range
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the search.
        /// </summary>
        /// <value>
        ///     The search.
        /// </value>
        public string Search
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the tel.
        /// </summary>
        /// <value>
        ///     The tel.
        /// </value>
        public string Tel
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        /// <value>
        ///     The text.
        /// </value>
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the text block.
        /// </summary>
        /// <value>
        /// The text block.
        /// </value>
        [DataType(DataType.MultilineText)]
        public string TextBlock
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the time.
        /// </summary>
        /// <value>
        ///     The time.
        /// </value>
        public string Time
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the URL.
        /// </summary>
        /// <value>
        ///     The URL.
        /// </value>
        public string Url
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the week.
        /// </summary>
        /// <value>
        ///     The week.
        /// </value>
        public string Week
        {
            get;
            set;
        }
    }
}