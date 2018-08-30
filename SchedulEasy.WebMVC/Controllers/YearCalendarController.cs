using Microsoft.AspNet.Identity;
using SchedulEasy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchedulEasy.WebMVC.Controllers
{
    [Authorize]
    public class YearCalendarController : Controller
    {
        // GET: YearCalendar
        public ActionResult Index()
        {
            var svc = CreateCalendarService();
            var model = svc.GetYearCalendar();
            return View(model);
        }

        private CalendarService CreateCalendarService()
        {
            var userID = User.Identity.GetUserId();
            var service = new CalendarService(userID);
            return service;
        }
    }
}