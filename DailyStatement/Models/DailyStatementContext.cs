﻿using System.Data.Entity;

namespace DailyStatement.Models
{
    public class DailyStatementContext : DbContext
    {
        // 您可以將自訂程式碼新增到這個檔案。變更不會遭到覆寫。
        // 
        // 如果您要 Entity Framework 每次在您變更模型結構描述時
        // 自動卸除再重新產生資料庫，請將下列
        // 程式碼新增到 Global.asax 檔案的 Application_Start 方法中。
        // 注意: 這將隨著每次模型變更而損毀並重新建立您的資料庫。
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<DailyStatement.Models.DailyStatementContext>());

        public DailyStatementContext() : base("name=DailyStatementContext")
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<WorkCategory> Categories { get; set; }
        public DbSet<DailyInfo> Dailies { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DailyInfoConfigurations());
            modelBuilder.Configurations.Add(new WorkCategoryConfigurations());
            modelBuilder.Configurations.Add(new EmployeeConfigurations());
            modelBuilder.Configurations.Add(new RankConfigurations());
            modelBuilder.Configurations.Add(new ProjectConfigurations());
            modelBuilder.Configurations.Add(new PredictionConfigurations());
        }
    }
}
