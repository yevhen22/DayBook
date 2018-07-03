namespace DayBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changestype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DeleteUserModels", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DeleteUserModels", "UserId", c => c.Int(nullable: false));
        }
    }
}
