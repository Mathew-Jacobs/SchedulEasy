namespace SchedulEasy.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Team", "OwnerID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Team", "OwnerID", c => c.Guid(nullable: false));
        }
    }
}
