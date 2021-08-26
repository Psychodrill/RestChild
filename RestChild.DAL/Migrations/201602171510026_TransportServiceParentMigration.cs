namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransportServiceParentMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeOfService", "NeedTransport", c => c.Boolean(nullable: false));
            AddColumn("dbo.AddonServices", "TourTransportId", c => c.Long());
            AddColumn("dbo.AddonServices", "LinkServiceId", c => c.Long());
            CreateIndex("dbo.AddonServices", "TourTransportId");
            CreateIndex("dbo.AddonServices", "LinkServiceId");
            AddForeignKey("dbo.AddonServices", "LinkServiceId", "dbo.AddonServices", "Id");
            AddForeignKey("dbo.AddonServices", "TourTransportId", "dbo.TourTransport", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AddonServices", "TourTransportId", "dbo.TourTransport");
            DropForeignKey("dbo.AddonServices", "LinkServiceId", "dbo.AddonServices");
            DropIndex("dbo.AddonServices", new[] { "LinkServiceId" });
            DropIndex("dbo.AddonServices", new[] { "TourTransportId" });
            DropColumn("dbo.AddonServices", "LinkServiceId");
            DropColumn("dbo.AddonServices", "TourTransportId");
            DropColumn("dbo.TypeOfService", "NeedTransport");
        }
    }
}
