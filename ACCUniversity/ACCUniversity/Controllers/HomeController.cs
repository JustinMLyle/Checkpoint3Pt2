using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACCUniversity.Migrations;
using ACCUniversity.DAL;
using ACCUniversity.ViewModels;
using ACCUniversity.Models;
using System.Data.Entity.Migrations;

namespace ACCUniversity.Controllers
{
    public class HomeController : Controller
    {
        private SchoolContext context = new SchoolContext();
        public ActionResult Index()
        {

            
            return View();
        }

        public ActionResult About()
        {
            IQueryable<EnrollmentDateGroup> data = from student in context.Students
                                                   group student by student.EnrollmentDate into dateGroup
                                                   select new EnrollmentDateGroup()
                                                   {
                                                       EnrollmentDate = dateGroup.Key,
                                                       StudentCount = dateGroup.Count()
                                                   };
            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
            base.Dispose(disposing);
        }
    }
}