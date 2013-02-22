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
            AutomaticMigrationsEnabled = false;
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
                new Employee { EmployeeId = 1, Account = "calvert", Password = "44B37596362D06938D38C9D350DDDE304A9AD6B8", CreateDate = DateTime.Now, LastLoginDate = DateTime.Now, Rank = "1", Name = "Calvert", Email = "Calvert@DomainName.com", RecvNotify = false, Activity = true },
                new Employee { EmployeeId = 2, Account = "andy", Password = "B4A14DECB3CDF70084D1E24C591D8EF8FF8EEB49", CreateDate = DateTime.Now, LastLoginDate = DateTime.Now, Rank = "2", Name = "Andy", Email = "Andy@DomainName.com", RecvNotify = false, Activity = true },
                new Employee { EmployeeId = 3, Account = "test", Password = "954BB128CEF07F50189E725ADCA40EBD0C0BBB83", CreateDate = DateTime.Now, LastLoginDate = DateTime.Now, Rank = "3", Name = "Test", Email = "test@DomainName.world", RecvNotify = false, Activity = true }
            );

            context.Categories.AddOrUpdate(
                new WorkCategory { WorkCategoryId = 1, Name = "裝機" },
                new WorkCategory { WorkCategoryId = 2, Name = "維修" },
                new WorkCategory { WorkCategoryId = 3, Name = "測試" },
                new WorkCategory { WorkCategoryId = 4, Name = "DEMO" },
                new WorkCategory { WorkCategoryId = 5, Name = "KOM" },
                new WorkCategory { WorkCategoryId = 6, Name = "驗收" },
                new WorkCategory { WorkCategoryId = 7, Name = "教育訓練" },
                new WorkCategory { WorkCategoryId = 8, Name = "其他" }
            );
        }
    }
}
