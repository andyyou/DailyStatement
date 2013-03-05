namespace DailyStatement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRankTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ranks",
                c => new
                    {
                        RankId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.RankId);
            
            AddColumn("dbo.Employees", "Rank_RankId", c => c.Int());
            AddForeignKey("dbo.Employees", "Rank_RankId", "dbo.Ranks", "RankId");
            CreateIndex("dbo.Employees", "Rank_RankId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Employees", new[] { "Rank_RankId" });
            DropForeignKey("dbo.Employees", "Rank_RankId", "dbo.Ranks");
            DropColumn("dbo.Employees", "Rank_RankId");
            DropTable("dbo.Ranks");
        }
    }
}
