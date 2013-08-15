namespace Headless.DemoSite.Controllers
{
    using System.Web.Mvc;
    using Headless.DemoSite.Models;

    /// <summary>
    ///     The <see cref="FormController" />
    ///     class provides the controller implementation for the form views.
    /// </summary>
    public class FormController : Controller
    {
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
        /// <param name="form">The form.</param>
        /// <returns>
        /// An <see cref="ActionResult" /> value.
        /// </returns>
        [HttpPost]
        public ActionResult Index(FormData form)
        {
            return View(form);
        }
    }
}