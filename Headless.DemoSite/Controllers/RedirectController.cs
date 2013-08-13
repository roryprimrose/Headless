using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Headless.DemoSite.Controllers
{
    using System.Web.Routing;

    public class RedirectController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Temporary()
        {
            return RedirectToAction("About", "Home");
        }

        public ActionResult Permanent()
        {
            return RedirectToActionPermanent("About", "Home");
        }

        public ActionResult External()
        {
            return Redirect("https://google.com");
        }
    }
}
