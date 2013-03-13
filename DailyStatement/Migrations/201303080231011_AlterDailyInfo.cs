namespace DailyStatement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterDailyInfo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DailyInfoes", "ProjectNo", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DailyInfoes", "ProjectNo", c => c.String(nullable: false));
        }
    }
}
