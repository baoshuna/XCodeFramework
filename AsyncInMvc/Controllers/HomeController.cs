using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AsyncInMvc.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public async Task<ActionResult> Index()
        {

            await Gaga();
            return View();
        }

        private Task Gaga()
        {
            var xx = 1;
            return Task.FromResult((object)null);
        }
    }
}