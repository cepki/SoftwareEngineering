using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JoinINN.Repository;

namespace JoinINN.Controllers
{
    public class HomeController : Controller
    {
        private readonly SocialGroupsRepository socRep = new SocialGroupsRepository();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            var groups = socRep.GetUserWithThisId(2);
            socRep.AddNewGroup();

            return View();
        }
    }
}
