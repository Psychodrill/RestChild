namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatesInServicesMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddonServicesLink", "DateFrom", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddonServicesLink", "DateTo", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddonServices", "DateFrom", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddonServices", "DateTo", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.RoomRates", "IsAddonPlace", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoomRates", "IsAddonPlace");
            DropColumn("dbo.AddonServices", "DateTo");
            DropColumn("dbo.AddonServices", "DateFrom");
            DropColumn("dbo.AddonServicesLink", "DateTo");
            DropColumn("dbo.AddonServicesLink", "DateFrom");
        }
    }
}
