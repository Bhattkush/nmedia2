using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmedia2.Controllers
{
    public class BlogController : Controller
    {
        // GET: blog
        [Route("Blog")]

        public ActionResult Blog()
        {
            return View();
        }

        // GET: Ai-Education-System
        [Route("Blog/Ai-Education-System")]

        public ActionResult aiedu()
        {
            return View("Ai-Education-System");
        }
        [Route("Blog/WhatsApp-Chatbot-for-Academic-Institutions")]
        public ActionResult whatsapp()
        {
            return View();
        }

        [Route("Blog/How-to-choose-best-school-management-software")]
        public ActionResult school()
        {
            return View();
        }
        [Route("Blog/Benefits-of-online-fees-collection-for-Educational-Institutions")]
        public ActionResult details()
        {
            return View();
        }
        [Route("Blog/attendance-management-system")]
        public ActionResult attandance()
        {
            return View();
        }

        [Route("Blog/hostel-management-software")]
        public ActionResult hostel()
        {
            return View();
        }
        [Route("Blog/page2")]
        public ActionResult page()
        {
            return View();
        }
        [Route("Blog/Education-ERP-Software")]
        public ActionResult erp()
        {
            return View();
        }
        [Route("Blog/Attendance-System-Using-Face-Detection")]
        public ActionResult face()
        {
            return View();
        }
    }
}
