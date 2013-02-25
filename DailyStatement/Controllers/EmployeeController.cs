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
using DailyStatement.ViewModel;

namespace DailyStatement.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private DailyStatementContext db = new DailyStatementContext();

        private List<RankInfoForEmployee> _ranks = new List<RankInfoForEmployee>()
        {
            new RankInfoForEmployee() { RankId = 1, Name = "超級管理員" },
            new RankInfoForEmployee() { RankId = 2, Name = "一般管理員" },
            new RankInfoForEmployee() { RankId = 3, Name = "一般人員" }
        };

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

                if (String.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("Index", "Daily");
                }
                else
                {
                    return RedirectToAction(returnUrl);
                }
            }

            return View();
        }

        // 執行登出
        public ActionResult Logout()
        {
            // 清除表單驗證的 Cookies
            FormsAuthentication.SignOut();

            // 清除所有曾經寫入過的 Session 資料
            Session.Clear();

            return RedirectToAction("Login", "Employee");
        }

        // 顯示帳號列表頁面
        public ActionResult Index()
        {
            return View();
        }

        // 顯示帳號建立頁面
        public ActionResult Create()
        {
            ViewData["Ranks"] = new SelectList(_ranks, "RankId", "Name", 3);

            return View();
        }

        // 執行帳號建立
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // 顯示編輯帳號頁面
        public ActionResult Edit(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewData["Ranks"] = new SelectList(_ranks, "RankId", "Name", "");

            return View(employee);
        }

        // 執行編輯帳號
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // 檢查帳號是否已存在
        public ActionResult CheckAccountDup(string Account)
        {
            var employee = db.Employees.Where(e => e.Account == Account).FirstOrDefault();

            if (employee != null)
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }

        // 檢查電子郵件是否已存在
        public ActionResult CheckEmailDup(string Email)
        {
            var employee = db.Employees.Where(e => e.Email == Email).FirstOrDefault();

            if (employee != null)
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }
    }            
}