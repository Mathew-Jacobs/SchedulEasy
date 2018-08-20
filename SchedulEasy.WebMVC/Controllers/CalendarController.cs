using Microsoft.AspNet.Identity;
using SchedulEasy.Models;
using SchedulEasy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchedulEasy.WebMVC.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {

        // GET: Calendar
        public ActionResult Index(int ? month, int ? year)
        {
            var service = CreateCalendarService();
            if (month == null || year == null)
            {
                var model = service.GetCalendar(DateTime.Now.Month, DateTime.Now.Year);
                return View(model);
            }
            else
            {
                var model = service.GetCalendar(Convert.ToInt32(month), Convert.ToInt32(year));
                return View(model);
            }
        }

        private CalendarService CreateCalendarService()
        {
            var userID = User.Identity.GetUserId();
            var service = new CalendarService(userID);
            return service;
        }
    }
}