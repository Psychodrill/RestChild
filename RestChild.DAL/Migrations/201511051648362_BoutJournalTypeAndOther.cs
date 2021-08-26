namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class BoutJournalTypeAndOther : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoutJournalType",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.OrganizationTypeOfTransport",
                c => new
                    {
                        Organization_Id = c.Long(nullable: false),
                        TypeOfTransport_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Organization_Id, t.TypeOfTransport_Id })
                .ForeignKey("dbo.Organization", t => t.Organization_Id, cascadeDelete: true)
                .ForeignKey("dbo.TypeOfTransport", t => t.TypeOfTransport_Id, cascadeDelete: true)
                .Index(t => t.Organization_Id)
                .Index(t => t.TypeOfTransport_Id);

            AddColumn("dbo.Organization", "EkisSourcePk", c => c.Long());
            AddColumn("dbo.Organization", "EkisExternalPk", c => c.Long());
            AddColumn("dbo.Organization", "EkisStatus", c => c.Int());
            AddColumn("dbo.Organization", "EkisGuid", c => c.Guid());
            AddColumn("dbo.Organization", "IsVedOrganization", c => c.Boolean());
            AddColumn("dbo.Organization", "IsСontractor", c => c.Boolean());
            AddColumn("dbo.Organization", "IsTransport", c => c.Boolean());
            AddColumn("dbo.Organization", "TargetOrganizationPk", c => c.Long());
            AddColumn("dbo.Counselors", "RepresentativesOrganizations", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ticket", "OrganizationId", c => c.Long());
            AddColumn("dbo.DirectoryFlights", "OrganizationId", c => c.Long());
            AddColumn("dbo.ListOfChilds", "Responsible", c => c.String(maxLength: 1000));
            AddColumn("dbo.ListOfChilds", "ResponsiblePhone", c => c.String(maxLength: 1000));
            AddColumn("dbo.LinkToPeople", "DeliveredParents", c => c.Boolean(nullable: false));
            AddColumn("dbo.BoutJournal", "BoutJournalTypeId", c => c.Long());
            CreateIndex("dbo.Ticket", "OrganizationId");
            CreateIndex("dbo.DirectoryFlights", "OrganizationId");
            CreateIndex("dbo.BoutJournal", "BoutJournalTypeId");
            AddForeignKey("dbo.DirectoryFlights", "OrganizationId", "dbo.Organization", "Id");
            AddForeignKey("dbo.Ticket", "OrganizationId", "dbo.Organization", "Id");
            AddForeignKey("dbo.BoutJournal", "BoutJournalTypeId", "dbo.BoutJournalType", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.OrganizationTypeOfTransport", "TypeOfTransport_Id", "dbo.TypeOfTransport");
            DropForeignKey("dbo.OrganizationTypeOfTransport", "Organization_Id", "dbo.Organization");
            DropForeignKey("dbo.BoutJournal", "BoutJournalTypeId", "dbo.BoutJournalType");
            DropForeignKey("dbo.Ticket", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.DirectoryFlights", "OrganizationId", "dbo.Organization");
            DropIndex("dbo.OrganizationTypeOfTransport", new[] { "TypeOfTransport_Id" });
            DropIndex("dbo.OrganizationTypeOfTransport", new[] { "Organization_Id" });
            DropIndex("dbo.BoutJournal", new[] { "BoutJournalTypeId" });
            DropIndex("dbo.DirectoryFlights", new[] { "OrganizationId" });
            DropIndex("dbo.Ticket", new[] { "OrganizationId" });
            DropColumn("dbo.BoutJournal", "BoutJournalTypeId");
            DropColumn("dbo.LinkToPeople", "DeliveredParents");
            DropColumn("dbo.ListOfChilds", "ResponsiblePhone");
            DropColumn("dbo.ListOfChilds", "Responsible");
            DropColumn("dbo.DirectoryFlights", "OrganizationId");
            DropColumn("dbo.Ticket", "OrganizationId");
            DropColumn("dbo.Counselors", "RepresentativesOrganizations");
            DropColumn("dbo.Organization", "TargetOrganizationPk");
            DropColumn("dbo.Organization", "IsTransport");
            DropColumn("dbo.Organization", "IsСontractor");
            DropColumn("dbo.Organization", "IsVedOrganization");
            DropColumn("dbo.Organization", "EkisGuid");
            DropColumn("dbo.Organization", "EkisStatus");
            DropColumn("dbo.Organization", "EkisExternalPk");
            DropColumn("dbo.Organization", "EkisSourcePk");
            DropTable("dbo.OrganizationTypeOfTransport");
            DropTable("dbo.BoutJournalType");
        }
    }
}
