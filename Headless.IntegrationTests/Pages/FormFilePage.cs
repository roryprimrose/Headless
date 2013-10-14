namespace Headless.IntegrationTests.Pages
{
    using System;

    /// <summary>
    ///     The <see cref="FormFilePage" />
    ///     class is used to wrap the form file test page.
    /// </summary>
    public class FormFilePage : HtmlPage
    {
        /// <summary>
        ///     Gets the file1.
        /// </summary>
        /// <value>
        ///     The file1.
        /// </value>
        public HtmlFile File1
        {
            get
            {
                return Find<HtmlFile>().ById("files1");
            }
        }

        /// <summary>
        ///     Gets the file2.
        /// </summary>
        /// <value>
        ///     The file2.
        /// </value>
        public HtmlFile File2
        {
            get
            {
                return Find<HtmlFile>().ById("files2");
            }
        }

        /// <summary>
        ///     Gets the file3.
        /// </summary>
        /// <value>
        ///     The file3.
        /// </value>
        public HtmlFile File3
        {
            get
            {
                return Find<HtmlFile>().ById("files3");
            }
        }

        /// <summary>
        ///     Gets the file count.
        /// </summary>
        /// <value>
        ///     The file count.
        /// </value>
        public AnyHtmlElement FileCount
        {
            get
            {
                return Find<AnyHtmlElement>().ById("FileCount");
            }
        }

        /// <summary>
        ///     Gets the posted files.
        /// </summary>
        /// <value>
        ///     The posted files.
        /// </value>
        public HtmlTable PostedFiles
        {
            get
            {
                return Find<HtmlTable>().Singular();
            }
        }

        /// <summary>
        ///     Gets some data.
        /// </summary>
        /// <value>
        ///     Some data.
        /// </value>
        public HtmlInput SomeData
        {
            get
            {
                return Find<HtmlInput>().ByName("SomeData");
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
                return Find<HtmlButton>().Singular();
            }
        }

        /// <summary>
        ///     Gets the location.
        /// </summary>
        public override Uri TargetLocation
        {
            get
            {
                return Form.Files;
            }
        }
    }
}