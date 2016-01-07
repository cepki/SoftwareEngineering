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
            using (var context = new JoinINN.Infrastructure.GroupsDb())
            {
                context.Visitors.First().NumberOfVisits++;
                context.SaveChanges();
            }

            return View();
        }
    }
}
