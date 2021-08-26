namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComercialPaymentAndPyerMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoomRatesPrice", "RoomRatesId", "dbo.RoomRates");
            DropIndex("dbo.RoomRatesPrice", new[] { "RoomRatesId" });
            CreateTable(
                "dbo.TourAccommodation",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateFrom = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateTo = c.DateTime(precision: 7, storeType: "datetime2"),
                        YearOfRestId = c.Long(),
                        HotelId = c.Long(),
                        TourId = c.Long(),
                        SubjectOfRestId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.HotelId)
                .ForeignKey("dbo.SubjectOfRest", t => t.SubjectOfRestId)
                .ForeignKey("dbo.YearOfRest", t => t.YearOfRestId)
                .ForeignKey("dbo.Tour", t => t.TourId)
                .Index(t => t.YearOfRestId)
                .Index(t => t.HotelId)
                .Index(t => t.TourId)
                .Index(t => t.SubjectOfRestId);
            
            CreateTable(
                "dbo.CounselorTestSubject",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1000),
                        QuestionCount = c.Int(),
                        IsArchive = c.Boolean(nullable: false),
                        CounselorTestId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CounselorTest", t => t.CounselorTestId)
                .Index(t => t.CounselorTestId);
            
            CreateTable(
                "dbo.CategoryIncident",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsArchive = c.Boolean(nullable: false),
                        DateCreate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TourTransport",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        AgeFrom = c.Int(),
                        AgeTo = c.Int(),
                        Price = c.Decimal(precision: 32, scale: 4),
                        PriceInternal = c.Decimal(precision: 32, scale: 4),
                        TourId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tour", t => t.TourId)
                .Index(t => t.TourId);
            
            AddColumn("dbo.Tour", "CityId", c => c.Long());
            AddColumn("dbo.Applicant", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Calculation", "Number", c => c.String(maxLength: 1000));
            AddColumn("dbo.Calculation", "Description", c => c.String());
            AddColumn("dbo.DiningOptions", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.RoomRates", "AgeFrom", c => c.Int());
            AddColumn("dbo.RoomRates", "AgeTo", c => c.Int());
            AddColumn("dbo.RoomRates", "SubjectOfRestId", c => c.Long());
            AddColumn("dbo.RoomRates", "TourAccommodationId", c => c.Long());
            AddColumn("dbo.Request", "ForIndex", c => c.Boolean(nullable: false));
            AddColumn("dbo.DiscountCard", "HistoryLinkId", c => c.Long());
            AddColumn("dbo.DiscountCard", "StateId", c => c.Long());
            AddColumn("dbo.Discount", "Unlimited", c => c.Boolean(nullable: false));
            AddColumn("dbo.Discount", "FileOrLinkId", c => c.Long());
            AddColumn("dbo.FileOrLink", "IsMain", c => c.Boolean(nullable: false));
            AddColumn("dbo.Payment", "Payer", c => c.String(maxLength: 1000));
            AddColumn("dbo.Payment", "Source", c => c.String(maxLength: 1000));
            AddColumn("dbo.ListOfChilds", "ForIndex", c => c.Boolean(nullable: false));
            AddColumn("dbo.TourVolume", "TourAccommodationId", c => c.Long());
            AddColumn("dbo.CounselorTestQuestion", "CounselorTestSubjectId", c => c.Long());
            AddColumn("dbo.TrainingCounselorsTest", "IsLastAttempt", c => c.Boolean(nullable: false));
            AddColumn("dbo.TrainingCounselorsGroupTest", "CountAttempts", c => c.Int());
            AddColumn("dbo.TrainingCounselorsGroupTest", "IsCountLimited", c => c.Boolean(nullable: false));
            AddColumn("dbo.TrainingCounselors", "ForSite", c => c.Boolean(nullable: false));
            AddColumn("dbo.BoutJournal", "ForSite", c => c.Boolean(nullable: false));
            AddColumn("dbo.BoutJournal", "CategoryIncidentId", c => c.Long());
            AlterColumn("dbo.Tour", "DateIncome", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Tour", "DateOutcome", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.RoomRates", "Price", c => c.Decimal(precision: 32, scale: 4));
            CreateIndex("dbo.Tour", "CityId");
            CreateIndex("dbo.RoomRates", "SubjectOfRestId");
            CreateIndex("dbo.RoomRates", "TourAccommodationId");
            CreateIndex("dbo.TourVolume", "TourAccommodationId");
            CreateIndex("dbo.DiscountCard", "HistoryLinkId");
            CreateIndex("dbo.DiscountCard", "StateId");
            CreateIndex("dbo.Discount", "FileOrLinkId");
            CreateIndex("dbo.CounselorTestQuestion", "CounselorTestSubjectId");
            CreateIndex("dbo.BoutJournal", "CategoryIncidentId");
            AddForeignKey("dbo.RoomRates", "SubjectOfRestId", "dbo.SubjectOfRest", "Id");
            AddForeignKey("dbo.TourVolume", "TourAccommodationId", "dbo.TourAccommodation", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RoomRates", "TourAccommodationId", "dbo.TourAccommodation", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DiscountCard", "HistoryLinkId", "dbo.HistoryLink", "Id");
            AddForeignKey("dbo.DiscountCard", "StateId", "dbo.StateMachineState", "Id");
            AddForeignKey("dbo.Discount", "FileOrLinkId", "dbo.FileOrLink", "Id");
            AddForeignKey("dbo.CounselorTestQuestion", "CounselorTestSubjectId", "dbo.CounselorTestSubject", "Id");
            AddForeignKey("dbo.BoutJournal", "CategoryIncidentId", "dbo.CategoryIncident", "Id");
            AddForeignKey("dbo.Tour", "CityId", "dbo.City", "Id");
            DropTable("dbo.RoomRatesPrice");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RoomRatesPrice",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 32, scale: 4),
                        DateFrom = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateTo = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        PriceInternal = c.Decimal(precision: 32, scale: 4),
                        RoomRatesId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.TourTransport", "TourId", "dbo.Tour");
            DropForeignKey("dbo.TourAccommodation", "TourId", "dbo.Tour");
            DropForeignKey("dbo.Tour", "CityId", "dbo.City");
            DropForeignKey("dbo.BoutJournal", "CategoryIncidentId", "dbo.CategoryIncident");
            DropForeignKey("dbo.CounselorTestQuestion", "CounselorTestSubjectId", "dbo.CounselorTestSubject");
            DropForeignKey("dbo.CounselorTestSubject", "CounselorTestId", "dbo.CounselorTest");
            DropForeignKey("dbo.Discount", "FileOrLinkId", "dbo.FileOrLink");
            DropForeignKey("dbo.DiscountCard", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.DiscountCard", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.RoomRates", "TourAccommodationId", "dbo.TourAccommodation");
            DropForeignKey("dbo.TourAccommodation", "YearOfRestId", "dbo.YearOfRest");
            DropForeignKey("dbo.TourVolume", "TourAccommodationId", "dbo.TourAccommodation");
            DropForeignKey("dbo.TourAccommodation", "SubjectOfRestId", "dbo.SubjectOfRest");
            DropForeignKey("dbo.TourAccommodation", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.RoomRates", "SubjectOfRestId", "dbo.SubjectOfRest");
            DropIndex("dbo.TourTransport", new[] { "TourId" });
            DropIndex("dbo.BoutJournal", new[] { "CategoryIncidentId" });
            DropIndex("dbo.CounselorTestQuestion", new[] { "CounselorTestSubjectId" });
            DropIndex("dbo.CounselorTestSubject", new[] { "CounselorTestId" });
            DropIndex("dbo.Discount", new[] { "FileOrLinkId" });
            DropIndex("dbo.DiscountCard", new[] { "StateId" });
            DropIndex("dbo.DiscountCard", new[] { "HistoryLinkId" });
            DropIndex("dbo.TourVolume", new[] { "TourAccommodationId" });
            DropIndex("dbo.TourAccommodation", new[] { "SubjectOfRestId" });
            DropIndex("dbo.TourAccommodation", new[] { "TourId" });
            DropIndex("dbo.TourAccommodation", new[] { "HotelId" });
            DropIndex("dbo.TourAccommodation", new[] { "YearOfRestId" });
            DropIndex("dbo.RoomRates", new[] { "TourAccommodationId" });
            DropIndex("dbo.RoomRates", new[] { "SubjectOfRestId" });
            DropIndex("dbo.Tour", new[] { "CityId" });
            AlterColumn("dbo.RoomRates", "Price", c => c.Decimal(nullable: false, precision: 32, scale: 4));
            AlterColumn("dbo.Tour", "DateOutcome", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Tour", "DateIncome", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.BoutJournal", "CategoryIncidentId");
            DropColumn("dbo.BoutJournal", "ForSite");
            DropColumn("dbo.TrainingCounselors", "ForSite");
            DropColumn("dbo.TrainingCounselorsGroupTest", "IsCountLimited");
            DropColumn("dbo.TrainingCounselorsGroupTest", "CountAttempts");
            DropColumn("dbo.TrainingCounselorsTest", "IsLastAttempt");
            DropColumn("dbo.CounselorTestQuestion", "CounselorTestSubjectId");
            DropColumn("dbo.TourVolume", "TourAccommodationId");
            DropColumn("dbo.ListOfChilds", "ForIndex");
            DropColumn("dbo.Payment", "Source");
            DropColumn("dbo.Payment", "Payer");
            DropColumn("dbo.FileOrLink", "IsMain");
            DropColumn("dbo.Discount", "FileOrLinkId");
            DropColumn("dbo.Discount", "Unlimited");
            DropColumn("dbo.DiscountCard", "StateId");
            DropColumn("dbo.DiscountCard", "HistoryLinkId");
            DropColumn("dbo.Request", "ForIndex");
            DropColumn("dbo.RoomRates", "TourAccommodationId");
            DropColumn("dbo.RoomRates", "SubjectOfRestId");
            DropColumn("dbo.RoomRates", "AgeTo");
            DropColumn("dbo.RoomRates", "AgeFrom");
            DropColumn("dbo.DiningOptions", "IsActive");
            DropColumn("dbo.Calculation", "Description");
            DropColumn("dbo.Calculation", "Number");
            DropColumn("dbo.Applicant", "IsDeleted");
            DropColumn("dbo.Tour", "CityId");
            DropTable("dbo.TourTransport");
            DropTable("dbo.CategoryIncident");
            DropTable("dbo.CounselorTestSubject");
            DropTable("dbo.TourAccommodation");
            CreateIndex("dbo.RoomRatesPrice", "RoomRatesId");
            AddForeignKey("dbo.RoomRatesPrice", "RoomRatesId", "dbo.RoomRates", "Id");
        }
    }
}
