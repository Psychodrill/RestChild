using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestChild.Web.Controllers
{
    public class HelpPageController : BaseController
    {
        // GET: HelpPage
        public ActionResult Index()
        {
            return View();
        }
    }
}
