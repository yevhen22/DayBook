namespace DayBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class daybookmodelfixes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DayBookModels",
                c => new
                    {
                        DayBookModelId = c.Int(nullable: false, identity: true),
                        DayRecord = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.DayBookModelId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DayBookModels", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.DayBookModels", new[] { "ApplicationUserId" });
            DropTable("dbo.DayBookModels");
        }
    }
}
