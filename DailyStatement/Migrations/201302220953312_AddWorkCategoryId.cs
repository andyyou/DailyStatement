namespace DailyStatement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWorkCategoryId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.DailyInfoes", name: "Category_WorkCategoryId", newName: "WorkCategoryId");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.DailyInfoes", name: "WorkCategoryId", newName: "Category_WorkCategoryId");
        }
    }
}
