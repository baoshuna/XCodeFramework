using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XCodeFramework.Web.Core.Mvc;

namespace XCodeFramework.Web.Controllers.Test
{
    public class TestController : BaseController
    {
        // GET: Test
        public ActionResult Index()
        {
            var x = 1;
            var y = 2;
            return View();
        }
    }
}