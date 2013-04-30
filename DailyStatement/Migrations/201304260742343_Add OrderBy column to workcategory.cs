namespace DailyStatement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderBycolumntoworkcategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkCategories", "OrderBy", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkCategories", "OrderBy");
        }
    }
}
