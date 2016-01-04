using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JoinINN.Controllers
{
    public class HomeController : Controller
    {
        public readonly Service service = new Service();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            var groups = service.GetAllGroups();

            return View();
        }
    }
}
