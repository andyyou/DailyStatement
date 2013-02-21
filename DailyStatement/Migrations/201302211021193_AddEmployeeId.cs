namespace DailyStatement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployeeId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.DailyInfoes", name: "Employee_EmployeeId", newName: "EmployeeId");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.DailyInfoes", name: "EmployeeId", newName: "Employee_EmployeeId");
        }
    }
}
