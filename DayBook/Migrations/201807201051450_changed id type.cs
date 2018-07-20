namespace DayBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedidtype : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ImageModels", new[] { "DayBookModel_DayBookModelId" });
            DropColumn("dbo.ImageModels", "DayBookModelId");
            RenameColumn(table: "dbo.ImageModels", name: "DayBookModel_DayBookModelId", newName: "DayBookModelId");
            AlterColumn("dbo.ImageModels", "DayBookModelId", c => c.Int(nullable: false));
            CreateIndex("dbo.ImageModels", "DayBookModelId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ImageModels", new[] { "DayBookModelId" });
            AlterColumn("dbo.ImageModels", "DayBookModelId", c => c.String());
            RenameColumn(table: "dbo.ImageModels", name: "DayBookModelId", newName: "DayBookModel_DayBookModelId");
            AddColumn("dbo.ImageModels", "DayBookModelId", c => c.String());
            CreateIndex("dbo.ImageModels", "DayBookModel_DayBookModelId");
        }
    }
}
