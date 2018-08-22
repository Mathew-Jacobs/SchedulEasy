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
    public class BusyDayController : Controller
    {
        // GET: BusyDay
        public ActionResult Index()
        {
            var service = CreateBusyDayService();
            var model = service.GetBusyDays();
            return View(model);
        }

        // GET
        public ActionResult Create(DateTime Busy)
        {
            var svc = CreateBusyDayService();
            var model = svc.GetCreateByDate(Busy);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BusyDayCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateBusyDayService();

            service.CreateBusyDay(model);

            return RedirectToAction("Index","Calendar");
        }


        public ActionResult Details(int id)
        {
            var svc = CreateBusyDayService();
            var model = svc.GetBusyDayByID(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateBusyDayService();
            var detail = service.GetBusyDayByID(id);
            var model =
                new BusyDayEdit
                {
                    BusyDayID = detail.BusyDayID,
                    Busy = detail.Busy,
                    BusyEnd = detail.BusyEnd,
                    Description = detail.Description
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BusyDayEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            
            if (model.BusyDayID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateBusyDayService();
            
            if (service.UpdateBusyDay(model))
            {
                return RedirectToAction("Index","Calendar");
            }
            
            ModelState.AddModelError("", "Your date could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateBusyDayService();
            var model = svc.GetBusyDayByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateBusyDayService();

            service.DeleteDay(id);

            return RedirectToAction("Index","Calendar");
        }

        public ActionResult CalendarTest()
        {
            return View();
        }

        private BusyDayService CreateBusyDayService()
        {
            var userID = User.Identity.GetUserId();
            var service = new BusyDayService(userID);
            return service;
        }
    }
}