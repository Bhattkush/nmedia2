using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmedia2.Controllers
{
    public class ServicesController : Controller
    {
        [Route("Services")]
        // GET: services
        public ActionResult Services()
        {
            return View();
        }
    }
}