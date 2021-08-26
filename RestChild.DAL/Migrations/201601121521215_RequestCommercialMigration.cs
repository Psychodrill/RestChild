namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class RequestCommercialMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Child", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.Applicant", "TicketId", "dbo.Ticket");
            DropIndex("dbo.Applicant", new[] { "TicketId" });
            DropIndex("dbo.Child", new[] { "TicketId" });
            CreateTable(
                "dbo.RequestAccommodation",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateFrom = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateTo = c.DateTime(precision: 7, storeType: "datetime2"),
                        Price = c.Decimal(precision: 32, scale: 4),
                        PriceInternal = c.Decimal(precision: 32, scale: 4),
                        DiningOptionsId = c.Long(),
                        TypeOfRoomsId = c.Long(),
                        TourVolumeId = c.Long(),
                        HotelsId = c.Long(),
                        RequestId = c.Long(),
                        RoomRatesId = c.Long(),
                        SubjectOfRestId = c.Long(),
                        AccommodationId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accommodation", t => t.AccommodationId)
                .ForeignKey("dbo.DiningOptions", t => t.DiningOptionsId)
                .ForeignKey("dbo.Hotels", t => t.HotelsId)
                .ForeignKey("dbo.RoomRates", t => t.RoomRatesId)
                .ForeignKey("dbo.SubjectOfRest", t => t.SubjectOfRestId)
                .ForeignKey("dbo.TourVolume", t => t.TourVolumeId)
                .ForeignKey("dbo.TypeOfRooms", t => t.TypeOfRoomsId)
                .ForeignKey("dbo.Request", t => t.RequestId)
                .Index(t => t.DiningOptionsId)
                .Index(t => t.TypeOfRoomsId)
                .Index(t => t.TourVolumeId)
                .Index(t => t.HotelsId)
                .Index(t => t.RequestId)
                .Index(t => t.RoomRatesId)
                .Index(t => t.SubjectOfRestId)
                .Index(t => t.AccommodationId);

            CreateTable(
                "dbo.RequestAccommodationLink",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DiningOptionsId = c.Long(),
                        RequestAccommodationId = c.Long(),
                        ApplicantId = c.Long(),
                        ChildId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicant", t => t.ApplicantId)
                .ForeignKey("dbo.Child", t => t.ChildId)
                .ForeignKey("dbo.DiningOptions", t => t.DiningOptionsId)
                .ForeignKey("dbo.RequestAccommodation", t => t.RequestAccommodationId)
                .Index(t => t.DiningOptionsId)
                .Index(t => t.RequestAccommodationId)
                .Index(t => t.ApplicantId)
                .Index(t => t.ChildId);

            CreateTable(
                "dbo.TicketLink",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Price = c.Decimal(precision: 32, scale: 4),
                        PriceInternal = c.Decimal(precision: 32, scale: 4),
                        Commentary = c.String(),
                        ApplicantId = c.Long(),
                        ChildId = c.Long(),
                        TicketId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicant", t => t.ApplicantId)
                .ForeignKey("dbo.Child", t => t.ChildId)
                .ForeignKey("dbo.Ticket", t => t.TicketId)
                .Index(t => t.ApplicantId)
                .Index(t => t.ChildId)
                .Index(t => t.TicketId);

            CreateTable(
                "dbo.CalculationRequestAccommodation",
                c => new
                    {
                        Calculation_Id = c.Long(nullable: false),
                        RequestAccommodation_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Calculation_Id, t.RequestAccommodation_Id })
                .ForeignKey("dbo.Calculation", t => t.Calculation_Id, cascadeDelete: true)
                .ForeignKey("dbo.RequestAccommodation", t => t.RequestAccommodation_Id, cascadeDelete: true)
                .Index(t => t.Calculation_Id)
                .Index(t => t.RequestAccommodation_Id);

            AddColumn("dbo.Request", "CityId", c => c.Long());
            AddColumn("dbo.Ticket", "DateOfArrival", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Ticket", "PlaceOfDeparture", c => c.String());
            AddColumn("dbo.Ticket", "PlaceOfArrival", c => c.String());
            AddColumn("dbo.Ticket", "RequestId", c => c.Long());
            AddColumn("dbo.Ticket", "CityOfDepartureId", c => c.Long());
            AddColumn("dbo.Ticket", "CityOfArrivalId", c => c.Long());
            CreateIndex("dbo.Request", "CityId");
            CreateIndex("dbo.Ticket", "RequestId");
            CreateIndex("dbo.Ticket", "CityOfDepartureId");
            CreateIndex("dbo.Ticket", "CityOfArrivalId");
            AddForeignKey("dbo.Request", "CityId", "dbo.City", "Id");
            AddForeignKey("dbo.Ticket", "CityOfArrivalId", "dbo.City", "Id");
            AddForeignKey("dbo.Ticket", "CityOfDepartureId", "dbo.City", "Id");
            AddForeignKey("dbo.Ticket", "RequestId", "dbo.Request", "Id");

			Sql("if (exists(select 1 from sysindexes where name='_dta_index_Applicant_5_661577395__K17_K27_K29_K42_1_2_3_4_5_6_7_8_9_10_11_12_13_14_15_16_18_19_20_21_22_23_24_25_26_28_30_31_')) drop index dbo.Applicant._dta_index_Applicant_5_661577395__K17_K27_K29_K42_1_2_3_4_5_6_7_8_9_10_11_12_13_14_15_16_18_19_20_21_22_23_24_25_26_28_30_31_");
			Sql("if (exists(select 1 from sysindexes where name='_dta_index_Applicant_5_661577395__K17_K1_K14_K27_K24_K29_2_3_4_5_6_7_8_9_10_11_12_13_15_16_18_19_20_21_22_23_25_26_28_30_31_32_')) drop index dbo.Applicant._dta_index_Applicant_5_661577395__K17_K1_K14_K27_K24_K29_2_3_4_5_6_7_8_9_10_11_12_13_15_16_18_19_20_21_22_23_25_26_28_30_31_32_");
			Sql("if (exists(select 1 from sysindexes where name='_dta_index_Applicant_5_661577395__K14_K27_K1_K17_K24_K29_2_3_4_5_6_7_8_9_10_11_12_13_15_16_18_19_20_21_22_23_25_26_28_30_31_32_')) drop index dbo.Applicant._dta_index_Applicant_5_661577395__K14_K27_K1_K17_K24_K29_2_3_4_5_6_7_8_9_10_11_12_13_15_16_18_19_20_21_22_23_25_26_28_30_31_32_");
			Sql("if (exists(select 1 from sysindexes where name='_dta_index_Child_5_757577737__K90_K23_K61_K82_K73_K1_K34_K65_2_3_4_5_6_7_8_9_10_11_12_13_14_15_16_17_18_19_20_21_22_24_25_26_')) drop index dbo.Child._dta_index_Child_5_757577737__K90_K23_K61_K82_K73_K1_K34_K65_2_3_4_5_6_7_8_9_10_11_12_13_14_15_16_17_18_19_20_21_22_24_25_26_");
			Sql("if (exists(select 1 from sysindexes where name='_dta_index_Child_5_757577737__K34_K73_K61_K65_K87_1_2_3_4_5_6_7_8_9_10_11_12_13_14_15_16_17_18_19_20_21_22_23_24_25_26_27_28_')) drop index dbo.Child._dta_index_Child_5_757577737__K34_K73_K61_K65_K87_1_2_3_4_5_6_7_8_9_10_11_12_13_14_15_16_17_18_19_20_21_22_23_24_25_26_27_28_");

			DropColumn("dbo.Applicant", "TicketId");
            DropColumn("dbo.Child", "TicketId");
        }

        public override void Down()
        {
            AddColumn("dbo.Child", "TicketId", c => c.Long());
            AddColumn("dbo.Applicant", "TicketId", c => c.Long());
            DropForeignKey("dbo.CalculationRequestAccommodation", "RequestAccommodation_Id", "dbo.RequestAccommodation");
            DropForeignKey("dbo.CalculationRequestAccommodation", "Calculation_Id", "dbo.Calculation");
            DropForeignKey("dbo.Ticket", "RequestId", "dbo.Request");
            DropForeignKey("dbo.TicketLink", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.TicketLink", "ChildId", "dbo.Child");
            DropForeignKey("dbo.TicketLink", "ApplicantId", "dbo.Applicant");
            DropForeignKey("dbo.Ticket", "CityOfDepartureId", "dbo.City");
            DropForeignKey("dbo.Ticket", "CityOfArrivalId", "dbo.City");
            DropForeignKey("dbo.RequestAccommodation", "RequestId", "dbo.Request");
            DropForeignKey("dbo.RequestAccommodation", "TypeOfRoomsId", "dbo.TypeOfRooms");
            DropForeignKey("dbo.RequestAccommodation", "TourVolumeId", "dbo.TourVolume");
            DropForeignKey("dbo.RequestAccommodation", "SubjectOfRestId", "dbo.SubjectOfRest");
            DropForeignKey("dbo.RequestAccommodation", "RoomRatesId", "dbo.RoomRates");
            DropForeignKey("dbo.RequestAccommodationLink", "RequestAccommodationId", "dbo.RequestAccommodation");
            DropForeignKey("dbo.RequestAccommodationLink", "DiningOptionsId", "dbo.DiningOptions");
            DropForeignKey("dbo.RequestAccommodationLink", "ChildId", "dbo.Child");
            DropForeignKey("dbo.RequestAccommodationLink", "ApplicantId", "dbo.Applicant");
            DropForeignKey("dbo.RequestAccommodation", "HotelsId", "dbo.Hotels");
            DropForeignKey("dbo.RequestAccommodation", "DiningOptionsId", "dbo.DiningOptions");
            DropForeignKey("dbo.RequestAccommodation", "AccommodationId", "dbo.Accommodation");
            DropForeignKey("dbo.Request", "CityId", "dbo.City");
            DropIndex("dbo.CalculationRequestAccommodation", new[] { "RequestAccommodation_Id" });
            DropIndex("dbo.CalculationRequestAccommodation", new[] { "Calculation_Id" });
            DropIndex("dbo.TicketLink", new[] { "TicketId" });
            DropIndex("dbo.TicketLink", new[] { "ChildId" });
            DropIndex("dbo.TicketLink", new[] { "ApplicantId" });
            DropIndex("dbo.Ticket", new[] { "CityOfArrivalId" });
            DropIndex("dbo.Ticket", new[] { "CityOfDepartureId" });
            DropIndex("dbo.Ticket", new[] { "RequestId" });
            DropIndex("dbo.RequestAccommodationLink", new[] { "ChildId" });
            DropIndex("dbo.RequestAccommodationLink", new[] { "ApplicantId" });
            DropIndex("dbo.RequestAccommodationLink", new[] { "RequestAccommodationId" });
            DropIndex("dbo.RequestAccommodationLink", new[] { "DiningOptionsId" });
            DropIndex("dbo.RequestAccommodation", new[] { "AccommodationId" });
            DropIndex("dbo.RequestAccommodation", new[] { "SubjectOfRestId" });
            DropIndex("dbo.RequestAccommodation", new[] { "RoomRatesId" });
            DropIndex("dbo.RequestAccommodation", new[] { "RequestId" });
            DropIndex("dbo.RequestAccommodation", new[] { "HotelsId" });
            DropIndex("dbo.RequestAccommodation", new[] { "TourVolumeId" });
            DropIndex("dbo.RequestAccommodation", new[] { "TypeOfRoomsId" });
            DropIndex("dbo.RequestAccommodation", new[] { "DiningOptionsId" });
            DropIndex("dbo.Request", new[] { "CityId" });
            DropColumn("dbo.Ticket", "CityOfArrivalId");
            DropColumn("dbo.Ticket", "CityOfDepartureId");
            DropColumn("dbo.Ticket", "RequestId");
            DropColumn("dbo.Ticket", "PlaceOfArrival");
            DropColumn("dbo.Ticket", "PlaceOfDeparture");
            DropColumn("dbo.Ticket", "DateOfArrival");
            DropColumn("dbo.Request", "CityId");
            DropTable("dbo.CalculationRequestAccommodation");
            DropTable("dbo.TicketLink");
            DropTable("dbo.RequestAccommodationLink");
            DropTable("dbo.RequestAccommodation");
            CreateIndex("dbo.Child", "TicketId");
            CreateIndex("dbo.Applicant", "TicketId");
            AddForeignKey("dbo.Applicant", "TicketId", "dbo.Ticket", "Id");
            AddForeignKey("dbo.Child", "TicketId", "dbo.Ticket", "Id");
        }
    }
}
