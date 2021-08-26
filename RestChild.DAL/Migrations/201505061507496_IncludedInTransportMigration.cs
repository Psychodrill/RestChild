namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncludedInTransportMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bout", "IncludedInTransport", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bout", "IncludedInTransport");
        }
    }
}
