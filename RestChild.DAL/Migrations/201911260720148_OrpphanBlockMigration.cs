namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrpphanBlockMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrganisatorCollaborator",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AditionalPhone = c.String(maxLength: 1000),
                        OrganisationPosition = c.String(maxLength: 1000),
                        WellnessOrganisationPositionId = c.Long(),
                        OrganisatonAddressId = c.Long(),
                        ApplicantId = c.Long(),
                        PositionId = c.Long(),
                        HistoryLinkId = c.Long(),
                        OrganisatonId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicant", t => t.ApplicantId)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .ForeignKey("dbo.Organization", t => t.OrganisatonId)
                .ForeignKey("dbo.OrphanageAddress", t => t.OrganisatonAddressId)
                .ForeignKey("dbo.OrganizationCollaboratorPostType", t => t.PositionId)
                .ForeignKey("dbo.TypeOfLinkPeople", t => t.WellnessOrganisationPositionId)
                .Index(t => t.WellnessOrganisationPositionId)
                .Index(t => t.OrganisatonAddressId)
                .Index(t => t.ApplicantId)
                .Index(t => t.PositionId)
                .Index(t => t.HistoryLinkId)
                .Index(t => t.OrganisatonId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.OrphanageAddress",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FencedArea = c.Boolean(nullable: false),
                        LargeParking = c.Boolean(nullable: false),
                        AddressId = c.Long(),
                        OrganisationId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.AddressId)
                .ForeignKey("dbo.Organization", t => t.OrganisationId)
                .Index(t => t.AddressId)
                .Index(t => t.OrganisationId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.Pupil",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateIn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateOut = c.DateTime(precision: 7, storeType: "datetime2"),
                        OrganisationOut = c.String(maxLength: 1000),
                        SchoolNotFound = c.Boolean(nullable: false),
                        SchoolName = c.String(maxLength: 1000),
                        OrphanageAddressId = c.Long(),
                        ChildId = c.Long(),
                        SchoolId = c.Long(),
                        LinkToFilesId = c.Long(),
                        HistoryLinkId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .ForeignKey("dbo.LinkToFile", t => t.LinkToFilesId)
                .ForeignKey("dbo.School", t => t.SchoolId)
                .ForeignKey("dbo.OrphanageAddress", t => t.OrphanageAddressId)
                .ForeignKey("dbo.Child", t => t.ChildId)
                .Index(t => t.OrphanageAddressId)
                .Index(t => t.ChildId)
                .Index(t => t.SchoolId)
                .Index(t => t.LinkToFilesId)
                .Index(t => t.HistoryLinkId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.OrganizationCollaboratorPostType",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            AddColumn("dbo.Organization", "Orphanage", c => c.Boolean());
            AddColumn("dbo.Organization", "HistoryLinkId", c => c.Long());
            CreateIndex("dbo.Organization", "HistoryLinkId");
            AddForeignKey("dbo.Organization", "HistoryLinkId", "dbo.HistoryLink", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrphanageAddress", "OrganisationId", "dbo.Organization");
            DropForeignKey("dbo.Pupil", "ChildId", "dbo.Child");
            DropForeignKey("dbo.OrganisatorCollaborator", "WellnessOrganisationPositionId", "dbo.TypeOfLinkPeople");
            DropForeignKey("dbo.OrganisatorCollaborator", "PositionId", "dbo.OrganizationCollaboratorPostType");
            DropForeignKey("dbo.OrganisatorCollaborator", "OrganisatonAddressId", "dbo.OrphanageAddress");
            DropForeignKey("dbo.Pupil", "OrphanageAddressId", "dbo.OrphanageAddress");
            DropForeignKey("dbo.Pupil", "SchoolId", "dbo.School");
            DropForeignKey("dbo.Pupil", "LinkToFilesId", "dbo.LinkToFile");
            DropForeignKey("dbo.Pupil", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.OrphanageAddress", "AddressId", "dbo.Address");
            DropForeignKey("dbo.OrganisatorCollaborator", "OrganisatonId", "dbo.Organization");
            DropForeignKey("dbo.OrganisatorCollaborator", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.OrganisatorCollaborator", "ApplicantId", "dbo.Applicant");
            DropForeignKey("dbo.Organization", "HistoryLinkId", "dbo.HistoryLink");
            DropIndex("dbo.OrganizationCollaboratorPostType", new[] { "EidSendStatus" });
            DropIndex("dbo.OrganizationCollaboratorPostType", new[] { "Eid" });
            DropIndex("dbo.Pupil", new[] { "EidSendStatus" });
            DropIndex("dbo.Pupil", new[] { "Eid" });
            DropIndex("dbo.Pupil", new[] { "HistoryLinkId" });
            DropIndex("dbo.Pupil", new[] { "LinkToFilesId" });
            DropIndex("dbo.Pupil", new[] { "SchoolId" });
            DropIndex("dbo.Pupil", new[] { "ChildId" });
            DropIndex("dbo.Pupil", new[] { "OrphanageAddressId" });
            DropIndex("dbo.OrphanageAddress", new[] { "EidSendStatus" });
            DropIndex("dbo.OrphanageAddress", new[] { "Eid" });
            DropIndex("dbo.OrphanageAddress", new[] { "OrganisationId" });
            DropIndex("dbo.OrphanageAddress", new[] { "AddressId" });
            DropIndex("dbo.OrganisatorCollaborator", new[] { "EidSendStatus" });
            DropIndex("dbo.OrganisatorCollaborator", new[] { "Eid" });
            DropIndex("dbo.OrganisatorCollaborator", new[] { "OrganisatonId" });
            DropIndex("dbo.OrganisatorCollaborator", new[] { "HistoryLinkId" });
            DropIndex("dbo.OrganisatorCollaborator", new[] { "PositionId" });
            DropIndex("dbo.OrganisatorCollaborator", new[] { "ApplicantId" });
            DropIndex("dbo.OrganisatorCollaborator", new[] { "OrganisatonAddressId" });
            DropIndex("dbo.OrganisatorCollaborator", new[] { "WellnessOrganisationPositionId" });
            DropIndex("dbo.Organization", new[] { "HistoryLinkId" });
            DropColumn("dbo.Organization", "HistoryLinkId");
            DropColumn("dbo.Organization", "Orphanage");
            DropTable("dbo.OrganizationCollaboratorPostType");
            DropTable("dbo.Pupil");
            DropTable("dbo.OrphanageAddress");
            DropTable("dbo.OrganisatorCollaborator");
        }
    }
}
