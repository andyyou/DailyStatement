namespace DailyStatement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyEmployeeTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "Rank_RankId", "dbo.Ranks");
            DropIndex("dbo.Employees", new[] { "Rank_RankId" });
            AddColumn("dbo.Employees", "RankId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Employees", "RankId", "dbo.Ranks", "RankId");
            CreateIndex("dbo.Employees", "RankId");
            DropColumn("dbo.Employees", "Rank");
            DropColumn("dbo.Employees", "Rank_RankId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Rank_RankId", c => c.Int());
            AddColumn("dbo.Employees", "Rank", c => c.String(maxLength: 50, unicode: false));
            DropIndex("dbo.Employees", new[] { "RankId" });
            DropForeignKey("dbo.Employees", "RankId", "dbo.Ranks");
            DropColumn("dbo.Employees", "RankId");
            CreateIndex("dbo.Employees", "Rank_RankId");
            AddForeignKey("dbo.Employees", "Rank_RankId", "dbo.Ranks", "RankId");
        }
    }
}
