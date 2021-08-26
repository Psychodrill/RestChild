namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class FirstRequestCompanyFieldsMigaration : DbMigration
    {
        public override void Up()
        {
			Sql(@"IF EXISTS(SELECT 1 FROM sys.indexes
WHERE name='_dta_index_Request_5_917578307__K15_K6_K7_K37_K16_1_2_3_4_5_8_9_10_11_12_13_17_18_19_20_21_22_23_24_25_27_28_29_30_31_32_33_34_' AND object_id = OBJECT_ID('Request'))
begin
	drop index _dta_index_Request_5_917578307__K15_K6_K7_K37_K16_1_2_3_4_5_8_9_10_11_12_13_17_18_19_20_21_22_23_24_25_27_28_29_30_31_32_33_34_ on dbo.Request
end

IF EXISTS(SELECT 1 FROM sys.indexes
WHERE name='_dta_index_Request_5_917578307__K35_K17_K6_K7_K15_K1_2_3_4_5_8_9_10_11_12_13_16_18_19_20_21_22_23_24_25_27_28_29_30_31_32_33_' AND object_id = OBJECT_ID('Request'))
begin
	drop index _dta_index_Request_5_917578307__K35_K17_K6_K7_K15_K1_2_3_4_5_8_9_10_11_12_13_16_18_19_20_21_22_23_24_25_27_28_29_30_31_32_33_ on dbo.Request
end

IF EXISTS(SELECT 1 FROM sys.indexes
WHERE name='_dta_index_Request_5_917578307__K35_K6_K15_K7_K17_K1_2_3_4_5_8_9_10_11_12_13_16_18_19_20_21_22_23_24_25_27_28_29_30_31_32_33_' AND object_id = OBJECT_ID('Request'))
begin
	drop index _dta_index_Request_5_917578307__K35_K6_K15_K7_K17_K1_2_3_4_5_8_9_10_11_12_13_16_18_19_20_21_22_23_24_25_27_28_29_30_31_32_33_ on dbo.Request
end

IF EXISTS(SELECT 1 FROM sys.indexes
WHERE name='_dta_index_Request_5_917578307__K28_K35_K6_K7_K15_K17_K1_2_3_4_5_8_9_10_11_12_13_16_18_19_20_21_22_23_24_25_27_29_30_31_32_33_' AND object_id = OBJECT_ID('Request'))
begin
	drop index _dta_index_Request_5_917578307__K28_K35_K6_K7_K15_K17_K1_2_3_4_5_8_9_10_11_12_13_16_18_19_20_21_22_23_24_25_27_29_30_31_32_33_ on dbo.Request
end

if exists(select * from sys.stats where object_id = OBJECT_ID('Request') and name='_dta_stat_917578307_22_21')
begin
	drop statistics dbo.Request._dta_stat_917578307_22_21
end

if exists(select * from sys.stats where object_id = OBJECT_ID('Request') and name='_dta_stat_917578307_16_22_21')
begin
	drop statistics dbo.Request._dta_stat_917578307_16_22_21
end


if exists(select * from sys.stats where object_id = OBJECT_ID('Request') and name='_dta_stat_917578307_15_16_22_21')
begin
	drop statistics dbo.Request._dta_stat_917578307_15_16_22_21
end

if exists(select * from sys.stats where object_id = OBJECT_ID('Request') and name='_dta_stat_917578307_28_16_22_21_15')
begin
	drop statistics dbo.Request._dta_stat_917578307_28_16_22_21_15
end


if exists(select * from sys.stats where object_id = OBJECT_ID('Request') and name='_dta_stat_917578307_18_16_22_21_28_15')
begin
	drop statistics dbo.Request._dta_stat_917578307_18_16_22_21_28_15
end


if exists(select * from sys.stats where object_id = OBJECT_ID('Request') and name='_dta_stat_917578307_17_16_22_21_28_15_19')
begin
	drop statistics dbo.Request._dta_stat_917578307_17_16_22_21_28_15_19
end

if exists(select * from sys.stats where object_id = OBJECT_ID('Request') and name='_dta_stat_917578307_19_16_22_21_28_15_18_17')
begin
	drop statistics dbo.Request._dta_stat_917578307_19_16_22_21_28_15_18_17
end");

			DropForeignKey("dbo.Request", "PlaceOfRestAddonId", "dbo.PlaceOfRest");
            DropIndex("dbo.Request", new[] { "PlaceOfRestAddonId" });
            CreateTable(
                "dbo.Relative",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ChildId = c.Long(),
                        ApplicantId = c.Long(),
                        StatusByChildId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicant", t => t.ApplicantId)
                .ForeignKey("dbo.StatusByChild", t => t.StatusByChildId)
                .ForeignKey("dbo.Child", t => t.ChildId)
                .Index(t => t.ChildId)
                .Index(t => t.ApplicantId)
                .Index(t => t.StatusByChildId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);

            CreateTable(
                "dbo.RequestTimeOfRest",
                c => new
                    {
                        Request_Id = c.Long(nullable: false),
                        TimeOfRest_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Request_Id, t.TimeOfRest_Id })
                .ForeignKey("dbo.Request", t => t.Request_Id, cascadeDelete: true)
                .ForeignKey("dbo.TimeOfRest", t => t.TimeOfRest_Id, cascadeDelete: true)
                .Index(t => t.Request_Id)
                .Index(t => t.TimeOfRest_Id);

            CreateTable(
                "dbo.PlaceOfRestRequest",
                c => new
                    {
                        PlaceOfRest_Id = c.Long(nullable: false),
                        Request_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlaceOfRest_Id, t.Request_Id })
                .ForeignKey("dbo.PlaceOfRest", t => t.PlaceOfRest_Id, cascadeDelete: true)
                .ForeignKey("dbo.Request", t => t.Request_Id, cascadeDelete: true)
                .Index(t => t.PlaceOfRest_Id)
                .Index(t => t.Request_Id);

            AddColumn("dbo.PlaceOfRest", "ForMpgu", c => c.Boolean(nullable: false));
            AddColumn("dbo.PlaceOfRest", "ForSite", c => c.Boolean(nullable: false));
            AddColumn("dbo.PlaceOfRest", "NotForSelect", c => c.Boolean(nullable: false));
            AddColumn("dbo.PlaceOfRest", "GroupId", c => c.Long());
            AddColumn("dbo.YearOfRest", "DateFirstStageClose", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.YearOfRest", "DateSecondStageClose", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Applicant", "DocumentCode", c => c.String(maxLength: 1000));
            AddColumn("dbo.Child", "DocumentCode", c => c.String(maxLength: 1000));
            AddColumn("dbo.BenefitType", "ForAisoOnly", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfRest", "FirstRequestCompanySelect", c => c.Boolean(nullable: false));
            CreateIndex("dbo.PlaceOfRest", "GroupId");
            AddForeignKey("dbo.PlaceOfRest", "GroupId", "dbo.PlaceOfRest", "Id");
            DropColumn("dbo.Request", "PlaceOfRestAddonId");
        }

        public override void Down()
        {
            AddColumn("dbo.Request", "PlaceOfRestAddonId", c => c.Long());
            DropForeignKey("dbo.PlaceOfRestRequest", "Request_Id", "dbo.Request");
            DropForeignKey("dbo.PlaceOfRestRequest", "PlaceOfRest_Id", "dbo.PlaceOfRest");
            DropForeignKey("dbo.PlaceOfRest", "GroupId", "dbo.PlaceOfRest");
            DropForeignKey("dbo.Relative", "ChildId", "dbo.Child");
            DropForeignKey("dbo.Relative", "StatusByChildId", "dbo.StatusByChild");
            DropForeignKey("dbo.Relative", "ApplicantId", "dbo.Applicant");
            DropForeignKey("dbo.RequestTimeOfRest", "TimeOfRest_Id", "dbo.TimeOfRest");
            DropForeignKey("dbo.RequestTimeOfRest", "Request_Id", "dbo.Request");
            DropIndex("dbo.PlaceOfRestRequest", new[] { "Request_Id" });
            DropIndex("dbo.PlaceOfRestRequest", new[] { "PlaceOfRest_Id" });
            DropIndex("dbo.RequestTimeOfRest", new[] { "TimeOfRest_Id" });
            DropIndex("dbo.RequestTimeOfRest", new[] { "Request_Id" });
            DropIndex("dbo.Relative", new[] { "EidSendStatus" });
            DropIndex("dbo.Relative", new[] { "Eid" });
            DropIndex("dbo.Relative", new[] { "StatusByChildId" });
            DropIndex("dbo.Relative", new[] { "ApplicantId" });
            DropIndex("dbo.Relative", new[] { "ChildId" });
            DropIndex("dbo.PlaceOfRest", new[] { "GroupId" });
            DropColumn("dbo.TypeOfRest", "FirstRequestCompanySelect");
            DropColumn("dbo.BenefitType", "ForAisoOnly");
            DropColumn("dbo.Child", "DocumentCode");
            DropColumn("dbo.Applicant", "DocumentCode");
            DropColumn("dbo.YearOfRest", "DateSecondStageClose");
            DropColumn("dbo.YearOfRest", "DateFirstStageClose");
            DropColumn("dbo.PlaceOfRest", "GroupId");
            DropColumn("dbo.PlaceOfRest", "NotForSelect");
            DropColumn("dbo.PlaceOfRest", "ForSite");
            DropColumn("dbo.PlaceOfRest", "ForMpgu");
            DropTable("dbo.PlaceOfRestRequest");
            DropTable("dbo.RequestTimeOfRest");
            DropTable("dbo.Relative");
            CreateIndex("dbo.Request", "PlaceOfRestAddonId");
            AddForeignKey("dbo.Request", "PlaceOfRestAddonId", "dbo.PlaceOfRest", "Id");
        }
    }
}
