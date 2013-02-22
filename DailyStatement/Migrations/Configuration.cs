namespace DailyStatement.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DailyStatement.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<DailyStatement.Models.DailyStatementContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DailyStatement.Models.DailyStatementContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Employees.AddOrUpdate(
                new Employee { EmployeeId = 1, Account = "calvert", Password = "calverttest", CreateDate = DateTime.Now, LastLoginDate = DateTime.Now, Rank = "1", Name = "Calvert", Email = "Calvert@DomainName.com", RecvNotify = false, Activity = true },
                new Employee { EmployeeId = 2, Account = "andy", Password = "andytest", CreateDate = DateTime.Now, LastLoginDate = DateTime.Now, Rank = "2", Name = "Andy", Email = "Andy@DomainName.com", RecvNotify = false, Activity = true },
                new Employee { EmployeeId = 3, Account = "test", Password = "test", CreateDate = DateTime.Now, LastLoginDate = DateTime.Now, Rank = "3", Name = "Test", Email = "test@DomainName.world", RecvNotify = false, Activity = true }
            );
        }
    }
}
