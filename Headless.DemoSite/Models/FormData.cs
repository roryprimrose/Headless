namespace Headless.DemoSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    ///     The <see cref="FormData" />
    ///     class is used for testing form interations.
    /// </summary>
    public class FormData
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FormData" /> class.
        /// </summary>
        public FormData()
        {
            List = new Collection<Selection>();
        }

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
        ///     Gets the data set values.
        /// </summary>
        /// <value>
        ///     The data set values.
        /// </value>
        public IEnumerable<Selection> DataSetValues
        {
            get
            {
                var values = Enum.GetValues(typeof(Selection));

                return values.Cast<Selection>();
            }
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
        ///     Gets or sets the drop down.
        /// </summary>
        /// <value>
        ///     The drop down.
        /// </value>
        public Selection? DropDown
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
        ///     Gets or sets the list.
        /// </summary>
        /// <value>
        ///     The list.
        /// </value>
        public Collection<Selection> List
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
        ///     Gets or sets the radio.
        /// </summary>
        /// <value>
        ///     The radio.
        /// </value>
        public Selection? Radio
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
        ///     Gets or sets the text block.
        /// </summary>
        /// <value>
        ///     The text block.
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
        ///     Gets or sets a value indicating whether this <see cref="FormData" /> is toggle.
        /// </summary>
        /// <value>
        ///     <c>true</c> if toggle; otherwise, <c>false</c>.
        /// </value>
        public bool Toggle
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