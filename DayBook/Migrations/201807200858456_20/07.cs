namespace DayBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2007 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ImageModels", "DayBookModel_DayBookModelId", "dbo.DayBookModels");
            DropIndex("dbo.ImageModels", new[] { "DayBookModel_DayBookModelId" });
            AlterColumn("dbo.ImageModels", "DayBookModel_DayBookModelId", c => c.Int(nullable: false));
            CreateIndex("dbo.ImageModels", "DayBookModel_DayBookModelId");
            AddForeignKey("dbo.ImageModels", "DayBookModel_DayBookModelId", "dbo.DayBookModels", "DayBookModelId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImageModels", "DayBookModel_DayBookModelId", "dbo.DayBookModels");
            DropIndex("dbo.ImageModels", new[] { "DayBookModel_DayBookModelId" });
            AlterColumn("dbo.ImageModels", "DayBookModel_DayBookModelId", c => c.Int());
            CreateIndex("dbo.ImageModels", "DayBookModel_DayBookModelId");
            AddForeignKey("dbo.ImageModels", "DayBookModel_DayBookModelId", "dbo.DayBookModels", "DayBookModelId");
        }
    }
}
