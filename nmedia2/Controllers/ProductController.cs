using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmedia2.Controllers
{
    public class ProductController : Controller
    {
        // GET: product
        [Route("Product")]
        public ActionResult Product()
        {
            return View();
        }
    }
}