namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TypeOfRestHaveService : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeOfRest", "HaveAddonService", c => c.Boolean(nullable: false));
            AddColumn("dbo.TourVolume", "EndBooking", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TourVolume", "EndBooking");
            DropColumn("dbo.TypeOfRest", "HaveAddonService");
        }
    }
}
