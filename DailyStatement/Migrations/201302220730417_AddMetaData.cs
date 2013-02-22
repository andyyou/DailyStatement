namespace DailyStatement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMetaData : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Account", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Password", c => c.String());
            AlterColumn("dbo.Employees", "Account", c => c.String());
        }
    }
}
