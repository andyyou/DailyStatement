using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KendoGridBinder;
using DailyStatement.Models;

namespace DailyStatement.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private DailyStatementContext db = new DailyStatementContext();

        // 密碼雜湊所需的 Salt 亂數值
        private string pwSalt = "qFgaQahNRE8v4oKzSMn2lWurfdVun5T6RW6G";

        // 顯示登入頁面
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        // 執行登入
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string account, string password, string returnUrl)
        {
            if (ValidateUser(account, password))
            {
                FormsAuthentication.SetAuthCookie(account, false);

                return RedirectToAction("Index", "Daily");
            }

            return View();
        }

        //
        // GET: /Employee/
        
        public ActionResult Index()
        {
            //return View(db.Employees.ToList());
            return View();
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "CreateDate")]Employee employee)
        {
            if (ModelState.IsValid)
            {
                // Encrypt password by SHA1 with salt
                employee.Password = GetHashPassword(employee.Password);
                employee.CreateDate = DateTime.Now;

                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// 取得經過雜湊加密後的密碼 
        /// </summary>
        /// <param name="password">明文密碼</param>
        /// <returns>加密密碼</returns>
        private string GetHashPassword(string password)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(pwSalt + password, "SHA1");
        }

        // 進行會員驗證
        private bool ValidateUser(string account, string password)
        {
            string hash_pw = GetHashPassword(password);

            var employee = (from m in db.Employees
                            where m.Account == account && m.Password == hash_pw
                            select m).FirstOrDefault();

            // 如果 employee 物件不為 null 則代表會員的帳號、密碼輸入正確
            if (employee != null)
            {
                if (employee.Activity == true)
                {
                    return true;
                }
                else
                {
                    ModelState.AddModelError("", "您的帳號已停用，請聯絡管理員！");
                    return false;
                }
            }
            else
            {
                ModelState.AddModelError("", "您輸入的帳號或密碼錯誤");
                return false;
            }
        }

        // 回傳所有帳號相關資料
        public JsonResult Grid(KendoGridRequest request)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var employees = db.Employees.ToList();
            var grid = new KendoGrid<Employee>(request, employees);

            return Json(grid);
        }
    }
}