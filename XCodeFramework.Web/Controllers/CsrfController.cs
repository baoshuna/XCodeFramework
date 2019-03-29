using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XCodeFramework.Web.Core.Mvc;

namespace XCodeFramework.Web.Controllers
{
    public class CsrfController : BaseController
    {
        // GET: Csrf
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult Notice(string notice)
        {
            ViewBag.Notice = notice;
            return View();
        }
    }
}