﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DailyStatement.Models;
using DailyStatement.ViewModel;
using KendoGridBinder;

namespace DailyStatement.Controllers
{
    [Authorize]
    public class DailyController : Controller
    {
        private DailyStatementContext db = new DailyStatementContext();

        //
        // GET: /Daily/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Grid(KendoGridRequest request)
        {
            // Sample here: https://github.com/rwhitmire/KendoGridBinder
            db.Configuration.ProxyCreationEnabled = false;

            var emp = db.Employees.Where(e => e.Account == User.Identity.Name).FirstOrDefault();

            List<DailyInfoForIndex> dailies = new List<DailyInfoForIndex>();
            if (emp.Rank == "3")
            {
                dailies = (from d in db.Dailies.Include("WorkCategory")
                           where d.EmployeeId == emp.EmployeeId
                               select new DailyInfoForIndex
                               {
                                   DailyInfoId = d.DailyInfoId,
                                   CreateDate = d.CreateDate,
                                   EmployeeId = d.DailyInfoId,
                                   Customer = d.Customer,
                                   ProjectNo = d.ProjectNo,
                                   WorkContent = d.WorkContent,
                                   WorkingHours = d.WorkingHours,
                                   WorkCategory = d.WorkCategory.Name
                               }).ToList();
            }
            else
            {
                dailies = (from d in db.Dailies.Include("WorkCategory")
                               select new DailyInfoForIndex
                               {
                                   DailyInfoId = d.DailyInfoId,
                                   CreateDate = d.CreateDate,
                                   EmployeeId = d.DailyInfoId,
                                   Customer = d.Customer,
                                   ProjectNo = d.ProjectNo,
                                   WorkContent = d.WorkContent,
                                   WorkingHours = d.WorkingHours,
                                   WorkCategory = d.WorkCategory.Name
                               }).ToList();
            }
            
            //var d = new List<DailyInfo> { 
            //    new DailyInfo{ CreateDate = DateTime.Now, Customer="哈哈哈我破關了", DailyInfoId=1, EmployeeId=1, ProjectNo="000001", WorkContent="Fuck 快讓我破關", WorkingHours=10 },
            //    new DailyInfo{ CreateDate = DateTime.Now, Customer="哈哈哈我破關了", DailyInfoId=2, EmployeeId=1, ProjectNo="000002", WorkContent="Fuck 快讓我破關", WorkingHours=10 },
            //    new DailyInfo{ CreateDate = DateTime.Now, Customer="哈哈哈我破關了", DailyInfoId=3, EmployeeId=1, ProjectNo="000003", WorkContent="Fuck 快讓我破關", WorkingHours=10 },
            //    new DailyInfo{ CreateDate = DateTime.Now, Customer="哈哈哈我破關了", DailyInfoId=4, EmployeeId=1, ProjectNo="000004", WorkContent="Fuck 快讓我破關", WorkingHours=10 },
            //    new DailyInfo{ CreateDate = DateTime.Now, Customer="哈哈哈我破關了", DailyInfoId=5, EmployeeId=1, ProjectNo="000005", WorkContent="Fuck 快讓我破關", WorkingHours=10 },
            //    new DailyInfo{ CreateDate = DateTime.Now, Customer="哈哈哈我破關了", DailyInfoId=6, EmployeeId=1, ProjectNo="000006", WorkContent="Fuck 快讓我破關", WorkingHours=10 }
            //};

            //var dailies = new List<DailyInfo>();
            //foreach (var daily in db.Dailies)
            //{
            //    DailyInfo d = new DailyInfo();
            //    d.CreateDate = daily.CreateDate;
            //    d.Customer = daily.Customer;
            //    d.DailyInfoId = daily.DailyInfoId;
            //    // d.Employee = daily.Employee;
            //    d.EmployeeId = daily.EmployeeId;
            //    d.ProjectNo = daily.ProjectNo;
            //    d.RowVersion = daily.RowVersion;
            //    d.WorkCategory = daily.WorkCategory;
            //    d.WorkCategoryId = daily.WorkCategoryId;
            //    d.WorkContent = daily.WorkContent;
            //    d.WorkingHours = daily.WorkingHours;
            //    dailies.Add(d);
            //}

            var grid = new KendoGrid<DailyInfoForIndex>(request, dailies);
            return Json(grid, JsonRequestBehavior.AllowGet);

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
            ViewData["Categories"] = new SelectList(db.Categories.ToList(), "WorkCategoryId", "Name", "");
            return View();
        }

        //
        // POST: /Daily/Create

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "EmployeeId")] DailyInfo dailyinfo)
        {
            dailyinfo.Employee = db.Employees.Where(e => e.Account == User.Identity.Name).FirstOrDefault();
            
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

            ViewData["Categories"] = new SelectList(db.Categories.ToList(), "WorkCategoryId", "Name", "");
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