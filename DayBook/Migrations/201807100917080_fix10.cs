namespace DayBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DayBookModels", "DayRecord", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DayBookModels", "DayRecord", c => c.String());
        }
    }
}
