namespace Headless.DemoSite.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Headless.DemoSite.Models;

    /// <summary>
    ///     The <see cref="FormController" />
    ///     class provides the controller implementation for the form views.
    /// </summary>
    public class FormController : Controller
    {
        /// <summary>
        ///     The files get action method.
        /// </summary>
        /// <returns>
        ///     An <see cref="ActionResult" /> value.
        /// </returns>
        [HttpGet]
        public ActionResult Files()
        {
            var data = new FileData();

            return View(data);
        }

        /// <summary>
        /// The post action for the file data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="files">The files.</param>
        /// <returns>
        /// An <see cref="ActionResult" /> value.
        /// </returns>
        [HttpPost]
        public ActionResult Files(FileData data, IEnumerable<HttpPostedFileBase> files)
        {
            var fileData = files.Where(x => x != null).ToList();

            data.FileCount = fileData.Count;
            data.PostedFiles = new List<FileMetadata>();

            foreach (var file in fileData)
            {
                var metadata = new FileMetadata();

                using (var reader = new StreamReader(file.InputStream))
                {
                    metadata.Content = reader.ReadToEnd();
                }

                metadata.FileName = file.FileName;
                metadata.ContentType = file.ContentType;

                data.PostedFiles.Add(metadata);
            }

            return View(data);
        }

        /// <summary>
        ///     The default page.
        /// </summary>
        /// <returns>An <see cref="ActionResult" /> value.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var form = new FormData();

            return View(form);
        }

        /// <summary>
        /// The default page.
        /// </summary>
        /// <param name="form">
        /// The form.
        /// </param>
        /// <returns>
        /// An <see cref="ActionResult"/> value.
        /// </returns>
        [HttpPost]
        public ActionResult Index(FormData form)
        {
            return View(form);
        }
    }
}