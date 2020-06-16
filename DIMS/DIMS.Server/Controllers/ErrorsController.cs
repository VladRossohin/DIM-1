using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIMS.Server.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult NotFound()
        {
            return View("404");
        }

        public ActionResult Forbidden()
        {
            return View("403");
        }

        public ActionResult InternalServerError()
        {
            return View("500");
        }
    }
}