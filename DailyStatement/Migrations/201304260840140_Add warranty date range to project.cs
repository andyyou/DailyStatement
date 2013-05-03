namespace DailyStatement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addwarrantydaterangetoproject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "WarrantyStart", c => c.DateTime());
            AddColumn("dbo.Projects", "WarrantyEnd", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "WarrantyEnd");
            DropColumn("dbo.Projects", "WarrantyStart");
        }
    }
}
