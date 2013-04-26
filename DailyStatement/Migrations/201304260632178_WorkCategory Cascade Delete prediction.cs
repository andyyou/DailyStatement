namespace DailyStatement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkCategoryCascadeDeleteprediction : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Predictions", "WorkCategory_WorkCategoryId", "dbo.WorkCategories");
            DropIndex("dbo.Predictions", new[] { "WorkCategory_WorkCategoryId" });
            AlterColumn("dbo.Predictions", "WorkCategory_WorkCategoryId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Predictions", "WorkCategory_WorkCategoryId", "dbo.WorkCategories", "WorkCategoryId", cascadeDelete: true);
            CreateIndex("dbo.Predictions", "WorkCategory_WorkCategoryId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Predictions", new[] { "WorkCategory_WorkCategoryId" });
            DropForeignKey("dbo.Predictions", "WorkCategory_WorkCategoryId", "dbo.WorkCategories");
            AlterColumn("dbo.Predictions", "WorkCategory_WorkCategoryId", c => c.Int());
            CreateIndex("dbo.Predictions", "WorkCategory_WorkCategoryId");
            AddForeignKey("dbo.Predictions", "WorkCategory_WorkCategoryId", "dbo.WorkCategories", "WorkCategoryId");
        }
    }
}
