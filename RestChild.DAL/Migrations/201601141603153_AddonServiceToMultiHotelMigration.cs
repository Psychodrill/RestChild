namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddonServiceToMultiHotelMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AddonServices", "HotelsId", "dbo.Hotels");
            DropIndex("dbo.AddonServices", new[] { "HotelsId" });
            CreateTable(
                "dbo.AddonServicesHotels",
                c => new
                    {
                        AddonServices_Id = c.Long(nullable: false),
                        Hotels_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.AddonServices_Id, t.Hotels_Id })
                .ForeignKey("dbo.AddonServices", t => t.AddonServices_Id, cascadeDelete: true)
                .ForeignKey("dbo.Hotels", t => t.Hotels_Id, cascadeDelete: true)
                .Index(t => t.AddonServices_Id)
                .Index(t => t.Hotels_Id);

	        Sql(
		        "insert into dbo.AddonServicesHotels (AddonServices_Id, Hotels_Id) select Id as AddonServices_Id, HotelsId as Hotels_Id from dbo.AddonServices where HotelsId is not null");

            DropColumn("dbo.AddonServices", "HotelsId");
        }

        public override void Down()
        {
            AddColumn("dbo.AddonServices", "HotelsId", c => c.Long());
            DropForeignKey("dbo.AddonServicesHotels", "Hotels_Id", "dbo.Hotels");
            DropForeignKey("dbo.AddonServicesHotels", "AddonServices_Id", "dbo.AddonServices");
            DropIndex("dbo.AddonServicesHotels", new[] { "Hotels_Id" });
            DropIndex("dbo.AddonServicesHotels", new[] { "AddonServices_Id" });
            DropTable("dbo.AddonServicesHotels");
            CreateIndex("dbo.AddonServices", "HotelsId");
            AddForeignKey("dbo.AddonServices", "HotelsId", "dbo.Hotels", "Id");
        }
    }
}
