namespace DailyStatement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWorkContent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyInfoes", "WorkContent", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyInfoes", "WorkContent");
        }
    }
}
