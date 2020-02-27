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
    public class IntervalsController : Controller
    {
        IRepository<Interval> intervalRepo = new IntervalRepository();
        IRepository<LogInterval> logRepo = new LogRepository();

        // GET: Intervals
        public ActionResult Index()
        {
            return View(intervalRepo.GetAll());
        }

        // GET: Intervals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interval interval = intervalRepo.Get((int)id); 
            if (interval == null)
            {
                return HttpNotFound();
            }
            LogInterval log = new LogInterval(DateTime.Now, "DETAILS", interval.ID);
            log.BeginDate = interval.BeginDate;
            log.EndDate = interval.EndDate;
            logRepo.Add(log);
            logRepo.Save();
            return View(interval);
        }

        // GET: Intervals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Intervals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BeginDate,EndDate")] Interval interval)
        {
            if (ModelState.IsValid)
            {
                intervalRepo.Add(interval);
                intervalRepo.Save();
                LogInterval log = new LogInterval(DateTime.Now, "CREATE", interval.ID);
                log.BeginDate = interval.BeginDate;
                log.EndDate = interval.EndDate;
                logRepo.Add(log);
                logRepo.Save();
                return RedirectToAction("Index");
            }

            return View(interval);
        }

        // GET: Intervals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interval interval = intervalRepo.Get((int)id);
            if (interval == null)
            {
                return HttpNotFound();
            }
            LogInterval log = new LogInterval(DateTime.Now, "REQUEST EDIT", interval.ID);
            log.BeginDate = interval.BeginDate;
            log.EndDate = interval.EndDate;
            logRepo.Add(log);
            logRepo.Save();
            return View(interval);
        }

        // POST: Intervals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BeginDate,EndDate")] Interval interval)
        {
            if (ModelState.IsValid)
            {
                intervalRepo.Update(interval);
                LogInterval log = new LogInterval(DateTime.Now, "EDIT", interval.ID);
                log.BeginDate = interval.BeginDate;
                log.EndDate = interval.EndDate;
                logRepo.Add(log);
                logRepo.Save();
                return RedirectToAction("Index");
            }
            return View(interval);
        }

        // GET: Intervals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interval interval = intervalRepo.Get((int)id);
            if (interval == null)
            {
                return HttpNotFound();
            }
            LogInterval log = new LogInterval(DateTime.Now, "REQUEST DELETE", interval.ID);
            log.BeginDate = interval.BeginDate;
            log.EndDate = interval.EndDate;
            logRepo.Add(log);
            logRepo.Save();
            return View(interval);
        }

        // POST: Intervals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Interval interval = intervalRepo.Get((int)id);
            intervalRepo.Remove(interval);
            LogInterval log = new LogInterval(DateTime.Now, "DELETE", interval.ID);
            log.BeginDate = interval.BeginDate;
            log.EndDate = interval.EndDate;
            logRepo.Add(log);
            logRepo.Save();
            return RedirectToAction("Index");
        }

        // GET: Intervals/Select
        public ActionResult Select()
        {
            LogInterval log = new LogInterval(DateTime.Now, "SELECT", 0);
            logRepo.Add(log);
            logRepo.Save();

            return View();
        }

        // POST: Intervals/Select
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Select([Bind(Include = "ID,BeginDate,EndDate")] Interval interval)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("SelectResult", interval);
            }

            return View(interval);
        }

        // GET: Intervals/SelestResult
        public ActionResult SelectResult(Interval interval)
        {
            var selectInterval = intervalRepo.GetAll().Where(x => (interval.BeginDate >= x.BeginDate && interval.BeginDate <= x.EndDate) ||
                    (interval.EndDate >= x.BeginDate && interval.EndDate <= x.EndDate)).Distinct();
            LogInterval log = new LogInterval(DateTime.Now, "SELECT RESULT", interval.ID);
            log.BeginDate = interval.BeginDate;
            log.EndDate = interval.EndDate;
            logRepo.Add(log);
            logRepo.Save();
            return View(selectInterval);
        }
    }
}
