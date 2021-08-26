namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlacementTourMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tour", "Memo", c => c.String());
            AddColumn("dbo.Tour", "MemoFile", c => c.String(maxLength: 1000));
            AddColumn("dbo.Tour", "MemoLink", c => c.String(maxLength: 1000));
            AddColumn("dbo.Request", "Commentary", c => c.String());
            AddColumn("dbo.AddonServices", "AgeFrom", c => c.Int());
            AddColumn("dbo.AddonServices", "AgeTo", c => c.Int());
            AddColumn("dbo.AddonServices", "ByDefault", c => c.Boolean(nullable: false));
            AddColumn("dbo.AddonServices", "OnlyWithRequest", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ticket", "AddonServicesId", c => c.Long());
            CreateIndex("dbo.Ticket", "AddonServicesId");
            AddForeignKey("dbo.Ticket", "AddonServicesId", "dbo.AddonServices", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ticket", "AddonServicesId", "dbo.AddonServices");
            DropIndex("dbo.Ticket", new[] { "AddonServicesId" });
            DropColumn("dbo.Ticket", "AddonServicesId");
            DropColumn("dbo.AddonServices", "OnlyWithRequest");
            DropColumn("dbo.AddonServices", "ByDefault");
            DropColumn("dbo.AddonServices", "AgeTo");
            DropColumn("dbo.AddonServices", "AgeFrom");
            DropColumn("dbo.Request", "Commentary");
            DropColumn("dbo.Tour", "MemoLink");
            DropColumn("dbo.Tour", "MemoFile");
            DropColumn("dbo.Tour", "Memo");
        }
    }
}
