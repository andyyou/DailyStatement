using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DailyStatement.Models;

namespace DailyStatement.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private DailyStatementContext db = new DailyStatementContext();

        // 密碼雜湊所需的 Salt 亂數值
        private string pwSalt = "qFgaQahNRE8v4oKzSMn2lWurfdVun5T6RW6G";

        // 顯示會員登入頁面
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        // 執行會員登入
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

        //
        // GET: /Employee/
        
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        //
        // GET: /Employee/Details/5

        public ActionResult Details(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
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
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                // Encrypt password by SHA1 with salt
                employee.Password = GetHashPassword(employee.Password);

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
                // Encrypt password by SHA1 with salt
                employee.Password = GetHashPassword(employee.Password);

                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        //
        // GET: /Employee/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    Employee employee = db.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employee);
        //}

        //
        // POST: /Employee/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Employee employee = db.Employees.Find(id);
        //    db.Employees.Remove(employee);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}