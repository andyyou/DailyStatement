namespace DailyStatement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriceToProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "Price");
        }
    }
}
