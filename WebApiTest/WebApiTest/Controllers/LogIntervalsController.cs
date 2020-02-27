using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DLL.Context;
using DLL.Models;
using DLL.Repositories;

namespace WebApiTest.Controllers
{
    public class LogIntervalsController : Controller
    {
        IRepository<Interval> intervalRepo = new IntervalRepository();
        IRepository<LogInterval> logRepo = new LogRepository();

        // GET: LogIntervals
        public ActionResult Index()
        {
            return View(logRepo.GetAll());
        }

        // GET: LogIntervals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogInterval logInterval = logRepo.Get((int)id); 
            if (logInterval == null)
            {
                return HttpNotFound();
            }
            return View(logInterval);
        }

        // GET: LogIntervals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LogIntervals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DateChange,TypeChange,Interval_ID")] LogInterval logInterval)
        {
            if (ModelState.IsValid)
            {
                logRepo.Add(logInterval);
                logRepo.Save();
                return RedirectToAction("Index");
            }

            return View(logInterval);
        }

        // GET: LogIntervals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogInterval logInterval = logRepo.Get((int)id); 
            if (logInterval == null)
            {
                return HttpNotFound();
            }
            return View(logInterval);
        }

        // POST: LogIntervals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DateChange,TypeChange,Interval_ID")] LogInterval logInterval)
        {
            if (ModelState.IsValid)
            {
                logRepo.Update(logInterval);
                logRepo.Save();
                return RedirectToAction("Index");
            }
            return View(logInterval);
        }

        // GET: LogIntervals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogInterval logInterval = logRepo.Get((int)id);
            if (logInterval == null)
            {
                return HttpNotFound();
            }
            return View(logInterval);
        }

        // POST: LogIntervals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LogInterval logInterval = logRepo.Get((int)id);
            logRepo.Remove(logInterval);
            logRepo.Save();
            return RedirectToAction("Index");
        }
    }
}
