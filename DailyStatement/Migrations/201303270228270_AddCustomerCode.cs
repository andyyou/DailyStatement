namespace DailyStatement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "CustomerCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "CustomerCode");
        }
    }
}
