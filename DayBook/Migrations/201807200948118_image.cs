namespace DayBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ImageModels", "ImagePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ImageModels", "ImagePath", c => c.String());
        }
    }
}
