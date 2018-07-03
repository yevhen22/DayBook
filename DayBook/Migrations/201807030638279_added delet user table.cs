namespace DayBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddeletusertable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeleteUserModels",
                c => new
                    {
                        DeleteUserModelId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        AddedUserTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DeleteUserModelId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DeleteUserModels");
        }
    }
}
