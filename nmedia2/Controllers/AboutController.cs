using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmedia2.Controllers
{
    public class AboutController : Controller
    {
        // GET: about
        [Route("About")]

        public ActionResult About()
        {
            return View();
        }
    }
}