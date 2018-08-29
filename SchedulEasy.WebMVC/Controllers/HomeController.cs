using Microsoft.AspNet.Identity;
using SchedulEasy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchedulEasy.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var svc = CreateCalendarService();
            var model = svc.GetYearCalendar();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private CalendarService CreateCalendarService()
        {
            var userID = User.Identity.GetUserId();
            var service = new CalendarService(userID);
            return service;
        }
    }
}