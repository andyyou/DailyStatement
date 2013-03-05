using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using DailyStatement.Models;
using DailyStatement.ViewModel;

namespace DailyStatement.App_Code
{
    public class CustomRoleProvider : RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string account)
        {
            //throw new NotImplementedException();
            List<string> users = new List<string>();

            using (DailyStatementContext _db = new DailyStatementContext())
            {
                try
                {
                    var usersInRole = from emp in _db.Employees.Include("Rank")
                                      where emp.Rank.Name == roleName && emp.Account == account
                                      select emp;

                    if (usersInRole != null)
                    {
                        foreach (var userInRole in usersInRole)
                        {
                            users.Add(userInRole.Account);
                        }
                    }
                }
                catch
                {
                }
            }

            return users.ToArray();
        }

        /// <summary>
        /// 取得所有角色
        /// </summary>
        /// <returns></returns>
        public override string[] GetAllRoles()
        {
            //throw new NotImplementedException();
            List<string> roles = new List<string>();

            using (DailyStatementContext _db = new DailyStatementContext())
            {
                try
                {
                    var dbRoles = from rank in _db.Ranks
                                  select rank;

                    foreach (var role in dbRoles)
                    {
                        roles.Add(role.Name);
                    }
                }
                catch
                {
                }
            }

            return roles.ToArray();
        }

        /// <summary>
        /// 取得指定帳號擁有的所有角色
        /// </summary>
        /// <param name="account">帳號</param>
        /// <returns></returns>
        public override string[] GetRolesForUser(string account)
        {
            ////throw new NotImplementedException();
            List<string> roles = new List<string>();

            using (DailyStatementContext _db = new DailyStatementContext())
            {
                try
                {
                    var dbRoles = from emp in _db.Employees.Include("Rank")
                                  where emp.Account == account
                                  select emp;

                    foreach (var role in dbRoles)
                    {
                        roles.Add(role.Rank.Name);
                    }

                }
                catch
                {
                }
            }

            return roles.ToArray();
        }

        /// <summary>
        /// 取得指定角色名稱下所有的帳號
        /// </summary>
        /// <param name="roleName">角色名稱</param>
        /// <returns></returns>
        public override string[] GetUsersInRole(string roleName)
        {
            //throw new NotImplementedException();
            List<string> users = new List<string>();

            using (DailyStatementContext _db = new DailyStatementContext())
            {
                try
                {
                    var usersInRole = from emp in _db.Employees.Include("Rank")
                                      where emp.Rank.Name == roleName
                                      select emp;

                    if (usersInRole != null)
                    {
                        foreach (var userInRole in usersInRole)
                        {
                            users.Add(userInRole.Account);
                        }
                    }
                }
                catch
                {
                }
            }

            return users.ToArray();
        }

        /// <summary>
        /// 檢查帳號是否屬於指定的角色
        /// </summary>
        /// <param name="account">帳號</param>
        /// <param name="roleName">角色名稱</param>
        /// <returns></returns>
        public override bool IsUserInRole(string account, string roleName)
        {
            //throw new NotImplementedException();
            bool isValid = false;

            using (DailyStatementContext _db = new DailyStatementContext())
            {
                try
                {
                    var usersInRole = from emp in _db.Employees.Include("Rank")
                                      where emp.Account == account && emp.Rank.Name == roleName
                                      select emp;

                    if (usersInRole != null)
                    {
                        isValid = true;
                    }
                }
                catch
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 檢查角色是否存在
        /// </summary>
        /// <param name="roleName">角色名稱</param>
        /// <returns></returns>
        public override bool RoleExists(string roleName)
        {
            //throw new NotImplementedException();
            bool isValid = false;

            using (DailyStatementContext _db = new DailyStatementContext())
            {
                // check if role exits
                if (_db.Ranks.Any(r => r.Name == roleName))
                {
                    isValid = true;
                }
            }

            return isValid;
        }
    }
}