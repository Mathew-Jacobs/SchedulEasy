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
    public class TeamController : Controller
    {
        // GET: Team
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();
            var service = new TeamService(userID);
            var model = service.GetTeams();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateTeamService();
            service.CreateTeam(model);

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var svc = CreateTeamService();
            var model = svc.GetTeamByID(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var svc = CreateTeamService();
            var detail = svc.GetTeamByID(id);
            var model =
                new TeamEdit
                {
                    TeamID = detail.TeamID,
                    Title = detail.Title,
                    Description = detail.Description
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TeamEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            
            if (model.TeamID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateTeamService();
            
            if (service.UpdateTeam(model))
            {
                TempData["SaveResult"] = "Your note was updated.";
                return RedirectToAction("Index");
            }
     
            ModelState.AddModelError("", "Your note could not be updated.");
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var svc = CreateTeamService();
            var model = svc.GetTeamByID(id);

            return View(model);
        }


        private TeamService CreateTeamService()
        {
            var userID = User.Identity.GetUserId();
            var service = new TeamService(userID);
            return service;
        }
    }
}