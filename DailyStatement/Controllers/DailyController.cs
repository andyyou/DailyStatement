using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DailyStatement.Models;
using KendoGridBinder;

namespace DailyStatement.Controllers
{
    public class DailyController : Controller
    {
        private DailyStatementContext db = new DailyStatementContext();

        //
        // GET: /Daily/

        public ActionResult Index()
        {
            return View(db.Dailies.ToList());
        }

        public JsonResult Grid(KendoGridRequest request, string keywd)
        {
            //var result = db.Dailies.ToList();
            //return Json(new KendoGrid<DailyInfo>(request, result));
            var result = SimMemberInfo.SimuDataStore.Where(o =>
            string.IsNullOrEmpty(keywd) || o.UserName.Contains(keywd));
            return Json(new KendoGrid<SimMemberInfo>(request, result));

        }

        //
        // GET: /Daily/Details/5

        public ActionResult Details(int id = 0)
        {
            DailyInfo dailyinfo = db.Dailies.Find(id);
            if (dailyinfo == null)
            {
                return HttpNotFound();
            }
            return View(dailyinfo);
        }

        //
        // GET: /Daily/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Daily/Create

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "EmployeeId")] DailyInfo dailyinfo)
        {
            dailyinfo.Employee = db.Employees.Where(e => e.EmployeeId == 1).FirstOrDefault();
            
            if (ModelState.IsValid)
            {
                db.Dailies.Add(dailyinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dailyinfo);
        }

        //
        // GET: /Daily/Edit/5

        public ActionResult Edit(int id = 0)
        {
            DailyInfo dailyinfo = db.Dailies.Find(id);

            ViewBag.CurrentEmployeeId = dailyinfo.EmployeeId;

            if (dailyinfo == null)
            {
                return HttpNotFound();
            }
            return View(dailyinfo);
        }

        //
        // POST: /Daily/Edit/5

        [HttpPost]
        public ActionResult Edit(DailyInfo dailyinfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dailyinfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dailyinfo);
        }

        //
        // GET: /Daily/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    DailyInfo dailyinfo = db.Dailies.Find(id);
        //    if (dailyinfo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dailyinfo);
        //}

        //
        // POST: /Daily/Delete/5
        
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DailyInfo dailyinfo = db.Dailies.Find(id);
            db.Dailies.Remove(dailyinfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}