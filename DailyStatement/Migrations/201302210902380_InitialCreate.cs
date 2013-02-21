namespace DailyStatement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Account = c.String(),
                        Password = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        LastLoginDate = c.DateTime(),
                        Rank = c.String(maxLength: 50, unicode: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 150),
                        RecvNotify = c.Boolean(nullable: false),
                        Activity = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.DailyInfoes",
                c => new
                    {
                        DailyInfoId = c.Int(nullable: false, identity: true),
                        ProjectNo = c.String(nullable: false),
                        Customer = c.String(nullable: false, maxLength: 100),
                        CreateDate = c.DateTime(nullable: false),
                        WorkingHours = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Category_WorkCategoryId = c.Int(),
                        Employee_EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DailyInfoId)
                .ForeignKey("dbo.WorkCategories", t => t.Category_WorkCategoryId)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .Index(t => t.Category_WorkCategoryId)
                .Index(t => t.Employee_EmployeeId);
            
            CreateTable(
                "dbo.WorkCategories",
                c => new
                    {
                        WorkCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.WorkCategoryId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.DailyInfoes", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.DailyInfoes", new[] { "Category_WorkCategoryId" });
            DropForeignKey("dbo.DailyInfoes", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.DailyInfoes", "Category_WorkCategoryId", "dbo.WorkCategories");
            DropTable("dbo.WorkCategories");
            DropTable("dbo.DailyInfoes");
            DropTable("dbo.Employees");
        }
    }
}
