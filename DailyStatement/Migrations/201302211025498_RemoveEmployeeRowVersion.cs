namespace DailyStatement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveEmployeeRowVersion : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.DailyInfoes", name: "EmployeeId", newName: "Employee_EmployeeId");
            DropColumn("dbo.Employees", "RowVersion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            RenameColumn(table: "dbo.DailyInfoes", name: "Employee_EmployeeId", newName: "EmployeeId");
        }
    }
}
