namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlockPlacesMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HotelBlock",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateFrom = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateTo = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Count = c.Int(nullable: false),
                        RowVersion = c.Binary(),
                        TypeOfRoomsId = c.Long(),
                        HotelId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.HotelId)
                .ForeignKey("dbo.TypeOfRooms", t => t.TypeOfRoomsId)
                .Index(t => t.TypeOfRoomsId)
                .Index(t => t.HotelId);
            
            CreateTable(
                "dbo.HotelBlockDate",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Count = c.Int(nullable: false),
                        Free = c.Int(nullable: false),
                        RowVersion = c.Binary(),
                        TypeOfRoomsId = c.Long(),
                        HotelId = c.Long(),
                        BlockId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.HotelId)
                .ForeignKey("dbo.TypeOfRooms", t => t.TypeOfRoomsId)
                .ForeignKey("dbo.HotelBlock", t => t.BlockId)
                .Index(t => t.TypeOfRoomsId)
                .Index(t => t.HotelId)
                .Index(t => t.BlockId);
            
            CreateTable(
                "dbo.ServiceBlock",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateFrom = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateTo = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Count = c.Int(nullable: false),
                        RowVersion = c.Binary(),
                        AddonServicesId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AddonServices", t => t.AddonServicesId)
                .Index(t => t.AddonServicesId);
            
            CreateTable(
                "dbo.ServiceBlockDate",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Count = c.Int(nullable: false),
                        Free = c.Int(nullable: false),
                        RowVersion = c.Binary(),
                        BlockId = c.Long(),
                        AddonServicesId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AddonServices", t => t.AddonServicesId)
                .ForeignKey("dbo.ServiceBlock", t => t.BlockId)
                .Index(t => t.BlockId)
                .Index(t => t.AddonServicesId);
            
            CreateTable(
                "dbo.TypePriceCalculation",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SearchFormSetting",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FormCode = c.String(nullable: false, maxLength: 1000),
                        Settings = c.String(),
                        AccountId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .Index(t => t.AccountId);
            
            AddColumn("dbo.TypeOfRest", "HiddenMainService", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfService", "CuratorId", c => c.Long());
            AddColumn("dbo.Tour", "MinPrepaymentAmount", c => c.Decimal(precision: 32, scale: 4));
            AddColumn("dbo.Tour", "NotFixedDate", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tour", "DurationHour", c => c.Int());
            AddColumn("dbo.Tour", "DurationDay", c => c.Int());
            AddColumn("dbo.Tour", "DurationMonth", c => c.Int());
            AddColumn("dbo.Tour", "DurationYear", c => c.Int());
            AddColumn("dbo.Child", "Infant", c => c.Boolean(nullable: false));
            AddColumn("dbo.AddonServices", "Hidden", c => c.Boolean(nullable: false));
            AddColumn("dbo.AddonServices", "NotFixedDate", c => c.Boolean(nullable: false));
            AddColumn("dbo.AddonServices", "TypePriceCalculationId", c => c.Long());
            AddColumn("dbo.AddonServicesPrice", "DateFrom", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddonServicesPrice", "DateTo", c => c.DateTime(precision: 7, storeType: "datetime2"));
            CreateIndex("dbo.TypeOfService", "CuratorId");
            CreateIndex("dbo.AddonServices", "TypePriceCalculationId");
            AddForeignKey("dbo.TypeOfService", "CuratorId", "dbo.Account", "Id");
            AddForeignKey("dbo.AddonServices", "TypePriceCalculationId", "dbo.TypePriceCalculation", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SearchFormSetting", "AccountId", "dbo.Account");
            DropForeignKey("dbo.AddonServices", "TypePriceCalculationId", "dbo.TypePriceCalculation");
            DropForeignKey("dbo.ServiceBlock", "AddonServicesId", "dbo.AddonServices");
            DropForeignKey("dbo.ServiceBlockDate", "BlockId", "dbo.ServiceBlock");
            DropForeignKey("dbo.ServiceBlockDate", "AddonServicesId", "dbo.AddonServices");
            DropForeignKey("dbo.HotelBlock", "TypeOfRoomsId", "dbo.TypeOfRooms");
            DropForeignKey("dbo.HotelBlock", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.HotelBlockDate", "BlockId", "dbo.HotelBlock");
            DropForeignKey("dbo.HotelBlockDate", "TypeOfRoomsId", "dbo.TypeOfRooms");
            DropForeignKey("dbo.HotelBlockDate", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.TypeOfService", "CuratorId", "dbo.Account");
            DropIndex("dbo.SearchFormSetting", new[] { "AccountId" });
            DropIndex("dbo.ServiceBlockDate", new[] { "AddonServicesId" });
            DropIndex("dbo.ServiceBlockDate", new[] { "BlockId" });
            DropIndex("dbo.ServiceBlock", new[] { "AddonServicesId" });
            DropIndex("dbo.AddonServices", new[] { "TypePriceCalculationId" });
            DropIndex("dbo.HotelBlockDate", new[] { "BlockId" });
            DropIndex("dbo.HotelBlockDate", new[] { "HotelId" });
            DropIndex("dbo.HotelBlockDate", new[] { "TypeOfRoomsId" });
            DropIndex("dbo.HotelBlock", new[] { "HotelId" });
            DropIndex("dbo.HotelBlock", new[] { "TypeOfRoomsId" });
            DropIndex("dbo.TypeOfService", new[] { "CuratorId" });
            DropColumn("dbo.AddonServicesPrice", "DateTo");
            DropColumn("dbo.AddonServicesPrice", "DateFrom");
            DropColumn("dbo.AddonServices", "TypePriceCalculationId");
            DropColumn("dbo.AddonServices", "NotFixedDate");
            DropColumn("dbo.AddonServices", "Hidden");
            DropColumn("dbo.Child", "Infant");
            DropColumn("dbo.Tour", "DurationYear");
            DropColumn("dbo.Tour", "DurationMonth");
            DropColumn("dbo.Tour", "DurationDay");
            DropColumn("dbo.Tour", "DurationHour");
            DropColumn("dbo.Tour", "NotFixedDate");
            DropColumn("dbo.Tour", "MinPrepaymentAmount");
            DropColumn("dbo.TypeOfService", "CuratorId");
            DropColumn("dbo.TypeOfRest", "HiddenMainService");
            DropTable("dbo.SearchFormSetting");
            DropTable("dbo.TypePriceCalculation");
            DropTable("dbo.ServiceBlockDate");
            DropTable("dbo.ServiceBlock");
            DropTable("dbo.HotelBlockDate");
            DropTable("dbo.HotelBlock");
        }
    }
}
