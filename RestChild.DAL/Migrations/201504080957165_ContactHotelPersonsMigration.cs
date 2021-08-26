namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactHotelPersonsMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HotelContactPerson",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 1000),
                        FirstName = c.String(nullable: false, maxLength: 1000),
                        MiddleName = c.String(nullable: false, maxLength: 1000),
                        Position = c.String(maxLength: 1000),
                        Phone = c.String(nullable: false, maxLength: 1000),
                        HotelId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.HotelId)
                .Index(t => t.HotelId);
            
            AddColumn("dbo.Hotels", "TakeChildUp3Years", c => c.Boolean(nullable: false));
            AddColumn("dbo.Hotels", "OtherLeisure", c => c.String());
            AddColumn("dbo.TypeOfRooms", "MaximumCount", c => c.Int(nullable: false));
            AddColumn("dbo.TypeOfRooms", "RoomSize", c => c.Decimal(precision: 22, scale: 8));
            AddColumn("dbo.TypeOfRooms", "RoomSizePerPerson", c => c.Decimal(precision: 22, scale: 8));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HotelContactPerson", "HotelId", "dbo.Hotels");
            DropIndex("dbo.HotelContactPerson", new[] { "HotelId" });
            DropColumn("dbo.TypeOfRooms", "RoomSizePerPerson");
            DropColumn("dbo.TypeOfRooms", "RoomSize");
            DropColumn("dbo.TypeOfRooms", "MaximumCount");
            DropColumn("dbo.Hotels", "OtherLeisure");
            DropColumn("dbo.Hotels", "TakeChildUp3Years");
            DropTable("dbo.HotelContactPerson");
        }
    }
}
