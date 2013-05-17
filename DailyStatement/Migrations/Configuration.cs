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
            if (context.Employees.Count() == 0)
            {
                context.Employees.AddOrUpdate(new Employee
                {
                    EmployeeId = 1,
                    Account = "admin",
                    Password = "F50102B11D7088627E8C6DC39C0DFC7C181D2D0B",
                    Name = "�w�]�޲z��(Admin)",
                    Email = "admin@winspection.com",
                    RecvNotify = false,
                    Activity = true,
                    Rank = new Rank { RankId = 1, Name = "�W�ź޲z��" },
                    CreateDate = DateTime.Now,
                    LastLoginDate = null
                });
            }

            if (context.Categories.Count() == 0)
            {
                context.Categories.AddOrUpdate(
                    new WorkCategory { WorkCategoryId = 1, Name = "�˾�" },
                    new WorkCategory { WorkCategoryId = 2, Name = "����" },
                    new WorkCategory { WorkCategoryId = 3, Name = "����" },
                    new WorkCategory { WorkCategoryId = 4, Name = "DEMO" },
                    new WorkCategory { WorkCategoryId = 5, Name = "KOM" },
                    new WorkCategory { WorkCategoryId = 6, Name = "�禬" },
                    new WorkCategory { WorkCategoryId = 7, Name = "�Ш|�V�m" },
                    new WorkCategory { WorkCategoryId = 8, Name = "��o" },
                    new WorkCategory { WorkCategoryId = 9, Name = "�]�p�W��" },
                    new WorkCategory { WorkCategoryId = 10, Name = "��L" },
                    new WorkCategory { WorkCategoryId = 11, Name = "�Ʈ�" },
                    new WorkCategory { WorkCategoryId = 12, Name = "�ո�" }
                );
            }

            if (context.Ranks.Count() == 0)
            {
                context.Ranks.AddOrUpdate(
                    //new Rank { RankId = 1, Name = "�W�ź޲z��" },
                    new Rank { RankId = 2, Name = "�@��޲z��" },
                    new Rank { RankId = 3, Name = "�u�{�v" },
                    new Rank { RankId = 4, Name = "�U�z" },
                    new Rank { RankId = 5, Name = "�~��" },
                    new Rank { RankId = 6, Name = "�|�p" }
                );
            }

            // context.Database.ExecuteSqlCommand("CREATE UNIQUE IX_Project_ProjectNo ON Projects(ProjectNo)");
        }
    }
}
