namespace SchedulEasy.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bla : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BusyDay", "Description", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BusyDay", "Description", c => c.String());
        }
    }
}
