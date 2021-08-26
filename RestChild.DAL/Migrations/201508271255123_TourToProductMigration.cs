namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TourToProductMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddonServicesPaymentType",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DiscountCard",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CardNumber = c.String(nullable: false, maxLength: 1000),
                        CardHolder = c.String(maxLength: 1000),
                        DateFrom = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateTo = c.DateTime(precision: 7, storeType: "datetime2"),
                        Procent = c.Decimal(precision: 32, scale: 4),
                        Email = c.String(maxLength: 1000),
                        Phone = c.String(maxLength: 1000),
                        DateOfBirth = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Discount",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Procent = c.Decimal(precision: 32, scale: 4),
                        Name = c.String(nullable: false, maxLength: 1000),
                        Description = c.String(),
                        DateFrom = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateTo = c.DateTime(precision: 7, storeType: "datetime2"),
                        HistoryLinkId = c.Long(),
                        StateId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .ForeignKey("dbo.StateMachineState", t => t.StateId)
                .Index(t => t.HistoryLinkId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.DiscountRequest",
                c => new
                    {
                        Discount_Id = c.Long(nullable: false),
                        Request_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Discount_Id, t.Request_Id })
                .ForeignKey("dbo.Discount", t => t.Discount_Id, cascadeDelete: true)
                .ForeignKey("dbo.Request", t => t.Request_Id, cascadeDelete: true)
                .Index(t => t.Discount_Id)
                .Index(t => t.Request_Id);
            
            CreateTable(
                "dbo.DiscountTour",
                c => new
                    {
                        Discount_Id = c.Long(nullable: false),
                        Tour_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Discount_Id, t.Tour_Id })
                .ForeignKey("dbo.Discount", t => t.Discount_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tour", t => t.Tour_Id, cascadeDelete: true)
                .Index(t => t.Discount_Id)
                .Index(t => t.Tour_Id);
            
            CreateTable(
                "dbo.RequestTour",
                c => new
                    {
                        Request_Id = c.Long(nullable: false),
                        Tour_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Request_Id, t.Tour_Id })
                .ForeignKey("dbo.Request", t => t.Request_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tour", t => t.Tour_Id, cascadeDelete: true)
                .Index(t => t.Request_Id)
                .Index(t => t.Tour_Id);
            
            CreateTable(
                "dbo.ApplicantTour",
                c => new
                    {
                        Applicant_Id = c.Long(nullable: false),
                        Tour_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Applicant_Id, t.Tour_Id })
                .ForeignKey("dbo.Applicant", t => t.Applicant_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tour", t => t.Tour_Id, cascadeDelete: true)
                .Index(t => t.Applicant_Id)
                .Index(t => t.Tour_Id);
            
            AddColumn("dbo.Tour", "NotSelf", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tour", "CuratorId", c => c.Long());
            AddColumn("dbo.Tour", "ComplexItemId", c => c.Long());
            AddColumn("dbo.Tour", "LinkToFileId", c => c.Long());
            AddColumn("dbo.AddonServices", "PriceInternal", c => c.Decimal(nullable: false, precision: 32, scale: 4));
            AddColumn("dbo.AddonServices", "Volume", c => c.Int());
            AddColumn("dbo.AddonServices", "AddonServicesPaymentTypeId", c => c.Long());
            AddColumn("dbo.RoomRates", "PriceInternal", c => c.Decimal(precision: 32, scale: 4));
            AddColumn("dbo.RoomRatesPrice", "PriceInternal", c => c.Decimal(precision: 32, scale: 4));
            AddColumn("dbo.TypeOfService", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfService", "Single", c => c.Boolean(nullable: false));
            AddColumn("dbo.Child", "ToursId", c => c.Long());
            AddColumn("dbo.Request", "PriceInternal", c => c.Decimal(precision: 32, scale: 4));
            AddColumn("dbo.Request", "DiscountCardNumber", c => c.String(maxLength: 1000));
            AddColumn("dbo.Request", "DiscountProcent", c => c.Decimal(precision: 32, scale: 4));
            AddColumn("dbo.Request", "DiscountCardId", c => c.Long());
            AddColumn("dbo.OfferInRequest", "DateFrom", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OfferInRequest", "DateTo", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OfferInRequest", "Price", c => c.Decimal(precision: 32, scale: 4));
            AddColumn("dbo.OfferInRequest", "PriceInternal", c => c.Decimal(precision: 32, scale: 4));
            AddColumn("dbo.SubjectOfRest", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.SubjectOfRest", "ViewOnSite", c => c.Boolean(nullable: false));
            AddColumn("dbo.SubjectOfRest", "ViewOnMpgu", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Tour", "CuratorId");
            CreateIndex("dbo.Tour", "ComplexItemId");
            CreateIndex("dbo.Tour", "LinkToFileId");
            CreateIndex("dbo.Child", "ToursId");
            CreateIndex("dbo.AddonServices", "AddonServicesPaymentTypeId");
            CreateIndex("dbo.Request", "DiscountCardId");
            AddForeignKey("dbo.AddonServices", "AddonServicesPaymentTypeId", "dbo.AddonServicesPaymentType", "Id");
            AddForeignKey("dbo.Request", "DiscountCardId", "dbo.DiscountCard", "Id");
            AddForeignKey("dbo.Child", "ToursId", "dbo.Tour", "Id");
            AddForeignKey("dbo.Tour", "CuratorId", "dbo.Account", "Id");
            AddForeignKey("dbo.Tour", "LinkToFileId", "dbo.LinkToFile", "Id");
            AddForeignKey("dbo.Tour", "ComplexItemId", "dbo.Tour", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tour", "ComplexItemId", "dbo.Tour");
            DropForeignKey("dbo.Tour", "LinkToFileId", "dbo.LinkToFile");
            DropForeignKey("dbo.Tour", "CuratorId", "dbo.Account");
            DropForeignKey("dbo.ApplicantTour", "Tour_Id", "dbo.Tour");
            DropForeignKey("dbo.ApplicantTour", "Applicant_Id", "dbo.Applicant");
            DropForeignKey("dbo.Child", "ToursId", "dbo.Tour");
            DropForeignKey("dbo.RequestTour", "Tour_Id", "dbo.Tour");
            DropForeignKey("dbo.RequestTour", "Request_Id", "dbo.Request");
            DropForeignKey("dbo.DiscountTour", "Tour_Id", "dbo.Tour");
            DropForeignKey("dbo.DiscountTour", "Discount_Id", "dbo.Discount");
            DropForeignKey("dbo.Discount", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.DiscountRequest", "Request_Id", "dbo.Request");
            DropForeignKey("dbo.DiscountRequest", "Discount_Id", "dbo.Discount");
            DropForeignKey("dbo.Discount", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.Request", "DiscountCardId", "dbo.DiscountCard");
            DropForeignKey("dbo.AddonServices", "AddonServicesPaymentTypeId", "dbo.AddonServicesPaymentType");
            DropIndex("dbo.ApplicantTour", new[] { "Tour_Id" });
            DropIndex("dbo.ApplicantTour", new[] { "Applicant_Id" });
            DropIndex("dbo.RequestTour", new[] { "Tour_Id" });
            DropIndex("dbo.RequestTour", new[] { "Request_Id" });
            DropIndex("dbo.DiscountTour", new[] { "Tour_Id" });
            DropIndex("dbo.DiscountTour", new[] { "Discount_Id" });
            DropIndex("dbo.DiscountRequest", new[] { "Request_Id" });
            DropIndex("dbo.DiscountRequest", new[] { "Discount_Id" });
            DropIndex("dbo.Discount", new[] { "StateId" });
            DropIndex("dbo.Discount", new[] { "HistoryLinkId" });
            DropIndex("dbo.Request", new[] { "DiscountCardId" });
            DropIndex("dbo.AddonServices", new[] { "AddonServicesPaymentTypeId" });
            DropIndex("dbo.Child", new[] { "ToursId" });
            DropIndex("dbo.Tour", new[] { "LinkToFileId" });
            DropIndex("dbo.Tour", new[] { "ComplexItemId" });
            DropIndex("dbo.Tour", new[] { "CuratorId" });
            DropColumn("dbo.SubjectOfRest", "ViewOnMpgu");
            DropColumn("dbo.SubjectOfRest", "ViewOnSite");
            DropColumn("dbo.SubjectOfRest", "IsActive");
            DropColumn("dbo.OfferInRequest", "PriceInternal");
            DropColumn("dbo.OfferInRequest", "Price");
            DropColumn("dbo.OfferInRequest", "DateTo");
            DropColumn("dbo.OfferInRequest", "DateFrom");
            DropColumn("dbo.Request", "DiscountCardId");
            DropColumn("dbo.Request", "DiscountProcent");
            DropColumn("dbo.Request", "DiscountCardNumber");
            DropColumn("dbo.Request", "PriceInternal");
            DropColumn("dbo.Child", "ToursId");
            DropColumn("dbo.TypeOfService", "Single");
            DropColumn("dbo.TypeOfService", "IsActive");
            DropColumn("dbo.RoomRatesPrice", "PriceInternal");
            DropColumn("dbo.RoomRates", "PriceInternal");
            DropColumn("dbo.AddonServices", "AddonServicesPaymentTypeId");
            DropColumn("dbo.AddonServices", "Volume");
            DropColumn("dbo.AddonServices", "PriceInternal");
            DropColumn("dbo.Tour", "LinkToFileId");
            DropColumn("dbo.Tour", "ComplexItemId");
            DropColumn("dbo.Tour", "CuratorId");
            DropColumn("dbo.Tour", "NotSelf");
            DropTable("dbo.ApplicantTour");
            DropTable("dbo.RequestTour");
            DropTable("dbo.DiscountTour");
            DropTable("dbo.DiscountRequest");
            DropTable("dbo.Discount");
            DropTable("dbo.DiscountCard");
            DropTable("dbo.AddonServicesPaymentType");
        }
    }
}
