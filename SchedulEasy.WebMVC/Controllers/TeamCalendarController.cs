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
    public class TeamCalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Index(int? month, int? year, int teamID)
        {
            var service = CreateTeamCalendarService();
            if (service.AuthorizeUser(teamID))
            {
                if (month == null || year == null)
                {
                    var model = service.GetTeamCalendar(DateTime.Now.Month, DateTime.Now.Year, teamID);
                    return View(model);
                }
                else
                {
                    var model = service.GetTeamCalendar(Convert.ToInt32(month), Convert.ToInt32(year), teamID);
                    return View(model);
                }
            }
            else
            {
                return RedirectToAction("Index", "Team");
            }
        }

        private TeamCalendarService CreateTeamCalendarService()
        {
            var userID = User.Identity.GetUserId();
            var service = new TeamCalendarService(userID);
            return service;
        }
    }
}