namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFunctioningTypeAndHotelPlacement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FunctioningType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HotelPlacement",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Hotels", "AgeFrom", c => c.Int());
            AddColumn("dbo.Hotels", "AgeTo", c => c.Int());
            AddColumn("dbo.Hotels", "DistanceFromCenter", c => c.Int());
            AddColumn("dbo.Hotels", "DistanceFromBeach", c => c.Int());
            AddColumn("dbo.Hotels", "FunctioningTypeId", c => c.Long());
            AddColumn("dbo.Hotels", "HotelPlacementId", c => c.Long());
            CreateIndex("dbo.Hotels", "FunctioningTypeId");
            CreateIndex("dbo.Hotels", "HotelPlacementId");
            AddForeignKey("dbo.Hotels", "FunctioningTypeId", "dbo.FunctioningType", "Id");
            AddForeignKey("dbo.Hotels", "HotelPlacementId", "dbo.HotelPlacement", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Hotels", "HotelPlacementId", "dbo.HotelPlacement");
            DropForeignKey("dbo.Hotels", "FunctioningTypeId", "dbo.FunctioningType");
            DropIndex("dbo.Hotels", new[] { "HotelPlacementId" });
            DropIndex("dbo.Hotels", new[] { "FunctioningTypeId" });
            DropColumn("dbo.Hotels", "HotelPlacementId");
            DropColumn("dbo.Hotels", "FunctioningTypeId");
            DropColumn("dbo.Hotels", "DistanceFromBeach");
            DropColumn("dbo.Hotels", "DistanceFromCenter");
            DropColumn("dbo.Hotels", "AgeTo");
            DropColumn("dbo.Hotels", "AgeFrom");
            DropTable("dbo.HotelPlacement");
            DropTable("dbo.FunctioningType");
        }
    }
}
