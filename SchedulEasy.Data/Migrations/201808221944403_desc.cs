namespace SchedulEasy.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class desc : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BusyDay", "Description", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BusyDay", "Description", c => c.String(maxLength: 15));
        }
    }
}
