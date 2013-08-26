namespace Headless.DemoSite.Controllers
{
    using System.Collections.Generic;
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