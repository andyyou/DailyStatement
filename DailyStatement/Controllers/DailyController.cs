﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DailyStatement.Models;
using DailyStatement.ViewModel;
using KendoGridBinder;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Data.SqlClient;
using CrystalDecisions.Shared;

namespace DailyStatement.Controllers
{
    [Authorize]
    public class DailyController : Controller
    {
        private DailyStatementContext db = new DailyStatementContext();
        //
        // GET: /Daily/

        [Authorize(Roles = "超級管理員,一般管理員,一般人員")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "超級管理員,一般管理員,一般人員")]
        public JsonResult Grid(KendoGridRequest request)
        {
            // Sample here: https://github.com/rwhitmire/KendoGridBinder
            db.Configuration.ProxyCreationEnabled = false;

            var emp = db.Employees.Include("Rank").Where(e => e.Account == User.Identity.Name).FirstOrDefault();

            List<DailyInfoForIndex> dailies = new List<DailyInfoForIndex>();
            if (emp.Rank.Name == "一般人員")
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
                                   WorkCategory = d.WorkCategory.Name,
                                   EmployeeName = d.Employee.Name
                               }).OrderByDescending(d => d.CreateDate).ToList();
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
                                   WorkCategory = d.WorkCategory.Name,
                                   EmployeeName = d.Employee.Name
                               }).OrderByDescending(d => d.CreateDate).ToList();
            }

            var grid = new KendoGrid<DailyInfoForIndex>(request, dailies);
            return Json(grid);
        }

        //
        // GET: /Daily/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    DailyInfo dailyinfo = db.Dailies.Find(id);
        //    if (dailyinfo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dailyinfo);
        //}

        //
        // GET: /Daily/Create

        [Authorize(Roles = "超級管理員,一般管理員,一般人員")]
        public ActionResult Create()
        {
            ViewData["Categories"] = new SelectList(db.Categories.ToList(), "WorkCategoryId", "Name", "");
            if (!User.IsInRole("一般人員"))
            {
                int empId = db.Employees.Where(e => e.Account == User.Identity.Name).FirstOrDefault().EmployeeId;
                ViewData["EmployeeList"] = new SelectList(db.Employees.ToList(), "EmployeeId", "Name", empId);
            }

            return View();
        }

        //
        // POST: /Daily/Create

        [HttpPost]
        [Authorize(Roles = "超級管理員,一般管理員,一般人員")]
        public ActionResult Create(string ProjectNo, int? WorkCategoryId, string Customer, string WorkContent, DateTime CreateDate, int WorkingHours, int? EmployeeList)
        {
            DailyInfo dailyinfo = new DailyInfo();
            dailyinfo.ProjectNo = ProjectNo;
            dailyinfo.WorkCategoryId = WorkCategoryId;
            dailyinfo.Customer = Customer;
            dailyinfo.WorkContent = WorkContent;
            dailyinfo.CreateDate = CreateDate;
            dailyinfo.WorkingHours = WorkingHours;
            dailyinfo.Employee = db.Employees.Where(e => (EmployeeList == null && e.Account == User.Identity.Name) || (EmployeeList != null && e.EmployeeId == EmployeeList)).FirstOrDefault();

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

        [Authorize(Roles = "超級管理員,一般管理員,一般人員")]
        public ActionResult Edit(int id = 0)
        {
            DailyInfo dailyinfo = db.Dailies.Find(id);

            ViewBag.CurrentEmployeeId = dailyinfo.EmployeeId;

            if (dailyinfo == null)
            {
                return HttpNotFound();
            }

            ViewData["Categories"] = new SelectList(db.Categories.ToList(), "WorkCategoryId", "Name", "");
            if (!User.IsInRole("一般人員"))
            {
                ViewData["EmployeeList"] = new SelectList(db.Employees.ToList(), "EmployeeId", "Name", dailyinfo.EmployeeId);
            }
            
            return View(dailyinfo);
        }

        //
        // POST: /Daily/Edit/5

        [HttpPost]
        [Authorize(Roles = "超級管理員,一般管理員,一般人員")]
        public ActionResult Edit(int DailyInfoId, string ProjectNo, int? WorkCategoryId, string Customer, string WorkContent, DateTime CreateDate, int WorkingHours, int? EmployeeList)
        {
            DailyInfo dailyinfo = db.Dailies.Where(d => d.DailyInfoId == DailyInfoId).FirstOrDefault();
            dailyinfo.ProjectNo = ProjectNo;
            dailyinfo.WorkCategoryId = WorkCategoryId;
            dailyinfo.Customer = Customer;
            dailyinfo.WorkContent = WorkContent;
            dailyinfo.CreateDate = CreateDate;
            dailyinfo.WorkingHours = WorkingHours;
            if (EmployeeList != null)
            {
                dailyinfo.EmployeeId = Convert.ToInt32(EmployeeList);
            }

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
        [Authorize(Roles = "超級管理員,一般管理員,一般人員")]
        public ActionResult DeleteConfirmed(int id)
        {
            DailyInfo dailyinfo = db.Dailies.Find(id);
            db.Dailies.Remove(dailyinfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "超級管理員,一般管理員,一般人員")]
        public ActionResult EmployeeList()
        {
            return View(db.Employees.ToList());
        }


        // UNDONE: 目前先不採用
        [HttpPost]
        [Authorize(Roles = "超級管理員,一般管理員,一般人員")]
        public JsonResult GetEmployeeNameList(KendoGridRequest request)
        {
            db.Configuration.ProxyCreationEnabled = false;

            if (User.IsInRole("一般人員"))
            {
                return Json("{}");
            }
            else
            {
                var employee = db.Employees.ToList();
                var grid = new KendoGrid<Employee>(request, employee);
                return Json(grid);
            }
        }

        [Authorize(Roles = "超級管理員,一般管理員,一般人員,助理")]
        public ActionResult ReportSearch()
        {
            List<SelectListItem> months = new List<SelectListItem>();
            for (int i = 1; i < 13; i++)
            {
                months.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            ViewBag.Months = new SelectList(months, "Text", "Value");

            ViewBag.Employee = new SelectList(db.Employees, "EmployeeId", "Name", UserId(User.Identity.Name));
            return View();
        }


        [ValidateAntiForgeryToken]
        [Authorize(Roles = "超級管理員,一般管理員,一般人員,助理")]
        public ActionResult ReportWeekForSingle(int employeeId, DateTime fromDate, DateTime toDate )
        {
            string query = String.Format(@"Select A.ProjectNo + ' - ' +
                                        (Select top 1 B.customer From DailyInfoes B where B.ProjectNo = A.ProjectNo) as [WorkName],
                                        SUM(CASE (DATEPART(Weekday, [CreateDate])) WHEN '1' THEN WorkingHours ELSE 0 END) AS [Sunday] ,
                                        SUM(CASE (DATEPART(Weekday, [CreateDate])) WHEN '2' THEN WorkingHours ELSE 0 END) AS [Monday] ,
                                        SUM(CASE (DATEPART(Weekday, [CreateDate])) WHEN '3' THEN WorkingHours ELSE 0 END) AS [Tuesday] ,
                                        SUM(CASE (DATEPART(Weekday, [CreateDate])) WHEN '4' THEN WorkingHours ELSE 0 END) AS [Wednesday] ,
                                        SUM(CASE (DATEPART(Weekday, [CreateDate])) WHEN '5' THEN WorkingHours ELSE 0 END) AS [Thursday] ,
                                        SUM(CASE (DATEPART(Weekday, [CreateDate])) WHEN '6' THEN WorkingHours ELSE 0 END) AS [Friday] ,
                                        SUM(CASE (DATEPART(Weekday, [CreateDate])) WHEN '7' THEN WorkingHours ELSE 0 END) AS [Saturday]
                                        From DailyInfoes A
	                                    Where EmployeeId = {0}
	                                    AND CreateDate Between '{1}' AND '{2}'
	                                    Group By EmployeeId, ProjectNo", employeeId, fromDate.ToShortDateString(), toDate.ToShortDateString());
            var report = db.Database.SqlQuery<WeekReportOfSingle>(query).ToList();

            ViewBag.TotalOfAll = (db.Dailies.Where(d => d.EmployeeId == employeeId && (d.CreateDate >= fromDate && d.CreateDate <= toDate)).Count()>0)?db.Dailies.Where(d => d.EmployeeId == employeeId && (d.CreateDate >= fromDate && d.CreateDate <= toDate)).Select(d => d.WorkingHours).Sum():0;
            ViewBag.EmployeeId = employeeId;
            ViewBag.EmployeeName = db.Employees.Where(e => e.EmployeeId == employeeId).SingleOrDefault().Name;
            CultureInfo ci = CultureInfo.CurrentCulture;
            int weekNum = ci.Calendar.GetWeekOfYear(fromDate, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            ViewBag.WeekNum = weekNum;
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            ViewBag.Date = fromDate.ToString("yyyy年MM月dd日") + "~" + toDate.ToString("yyyy年MM月dd日");
            var reportList = db.Dailies.Where(d => d.EmployeeId == employeeId && (d.CreateDate >= fromDate && d.CreateDate <= toDate)).Select(d => new { Date = d.CreateDate, Content = d.WorkContent });

            if (reportList != null)
            {
                ViewBag.Monday = new List<string>();
                ViewBag.Tuesday = new List<string>();
                ViewBag.Wednesday = new List<string>();
                ViewBag.Thursday = new List<string>();
                ViewBag.Friday = new List<string>();
                ViewBag.Overtime = new List<string>();
                foreach (var i in reportList)
                {
                    switch (i.Date.DayOfWeek.ToString())
                    { 
                        case "Monday":
                            ViewBag.Monday.Add(i.Content);
                            break;
                        case "Tuesday":
                            ViewBag.Tuesday.Add(i.Content);
                            break;
                        case "Wednesday":
                            ViewBag.Wednesday.Add(i.Content);
                            break;
                        case "Thursday":
                            ViewBag.Thursday.Add(i.Content);
                            break;
                        case "Friday":
                            ViewBag.Friday.Add(i.Content);
                            break;
                        default:
                            ViewBag.Overtime.Add(i.Content);
                            break;
                    }
                }
            }

            return View(report);
        }

        [Authorize(Roles = "超級管理員,一般管理員,一般人員,助理")]
        public ActionResult GenerateWeekReport(int employeeId, string weekOfYear, DateTime fromDate, DateTime toDate)
        {
            try
            {
                ReportDocument rpt = new ReportDocument();
                rpt.Load(Server.MapPath("~/Report/WeekReport.rpt"));

                DailyStatementDS ds = new DailyStatementDS();

                string conn = System.Configuration.ConfigurationManager.ConnectionStrings["DailyStatementContext"].ConnectionString;
                string condition = String.Format("SELECT * FROM [DailyStatement].[dbo].[DailyInfoes] WHERE [EmployeeId] = {0} AND ([CreateDate] >= '{1}' AND [CreateDate] <= '{2}')", employeeId, fromDate.ToShortDateString(), toDate.ToShortDateString());
                SqlDataAdapter da = new SqlDataAdapter(condition, conn);
                da.Fill(ds.DailyInfoes);
                condition = String.Format("SELECT * FROM [DailyStatement].[dbo].[Employees] WHERE [EmployeeId] = {0}", employeeId);
                da = new SqlDataAdapter(condition, conn);
                da.Fill(ds.Employees);
                // Due to SetParameterValue always return error, so use datatable to store parameter
                ds.Tables["Parameter"].Rows.Add(employeeId, fromDate, toDate, weekOfYear);
                
                rpt.SetDataSource(ds);

                //CrystalDecisions.Shared.TableLogOnInfo dbLoginInfo = new CrystalDecisions.Shared.TableLogOnInfo();
                //System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();
                //builder.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DailyStatementContext"].ConnectionString;

                //foreach (CrystalDecisions.CrystalReports.Engine.Table table in rpt.Database.Tables)
                //{
                //    dbLoginInfo.ConnectionInfo.ServerName = builder["Data Source"].ToString();
                //    dbLoginInfo.ConnectionInfo.DatabaseName = builder["Initial Catalog"].ToString();
                //    dbLoginInfo.ConnectionInfo.UserID = builder["User ID"].ToString();
                //    dbLoginInfo.ConnectionInfo.Password = builder["Password"].ToString();
                //    table.ApplyLogOnInfo(dbLoginInfo);
                //}

                Stream stream = rpt.ExportToStream(ExportFormatType.PortableDocFormat);
                string fileName = String.Format("{0}{1}{2}.pdf", employeeId, fromDate, toDate);
                return File(stream, "application/pdf");
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }

        [Authorize(Roles = "超級管理員,一般管理員,一般人員,助理")]
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [Authorize(Roles = "超級管理員,一般管理員,一般人員,助理")]
        private int UserId(string account)
        {
            var emp = db.Employees.Where(e => e.Account == account).SingleOrDefault();
            if (emp == null)
            {
                return 0;
            }
            return emp.EmployeeId;
        }
    }
}