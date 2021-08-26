namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class TypeOfRestMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeOfRest", "NeedAccomodation", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfRest", "NeedBookingDate", c => c.Boolean(nullable: false));
            AddColumn("dbo.Applicant", "AddonPhone", c => c.String(maxLength: 1000));
            AddColumn("dbo.Child", "DocumentTypeCertOfBirthId", c => c.Long());
            AddColumn("dbo.BenefitType", "SameBenefitId", c => c.Long());
            AddColumn("dbo.Request", "SiteUser", c => c.String(maxLength: 200));
            AddColumn("dbo.Request", "HistoryLinkId", c => c.Long());
            AddColumn("dbo.RequestFile", "RemoteSave", c => c.Boolean(nullable: false));
            AddColumn("dbo.RequestFile", "Description", c => c.String(maxLength: 1000));
            CreateIndex("dbo.BenefitType", "SameBenefitId");
            CreateIndex("dbo.Child", "DocumentTypeCertOfBirthId");
            CreateIndex("dbo.Request", "HistoryLinkId");
			CreateIndex("dbo.Request", "SiteUser");
			AddForeignKey("dbo.BenefitType", "SameBenefitId", "dbo.BenefitType", "Id");
            AddForeignKey("dbo.Request", "HistoryLinkId", "dbo.HistoryLink", "Id");
            AddForeignKey("dbo.Child", "DocumentTypeCertOfBirthId", "dbo.DocumentType", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Child", "DocumentTypeCertOfBirthId", "dbo.DocumentType");
            DropForeignKey("dbo.Request", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.BenefitType", "SameBenefitId", "dbo.BenefitType");
            DropIndex("dbo.Request", new[] { "HistoryLinkId" });
            DropIndex("dbo.Child", new[] { "DocumentTypeCertOfBirthId" });
            DropIndex("dbo.BenefitType", new[] { "SameBenefitId" });
			DropIndex("dbo.Request", new[] { "SiteUser" });
			DropColumn("dbo.RequestFile", "Description");
            DropColumn("dbo.RequestFile", "RemoteSave");
            DropColumn("dbo.Request", "HistoryLinkId");
            DropColumn("dbo.Request", "SiteUser");
            DropColumn("dbo.BenefitType", "SameBenefitId");
            DropColumn("dbo.Child", "DocumentTypeCertOfBirthId");
            DropColumn("dbo.Applicant", "AddonPhone");
            DropColumn("dbo.TypeOfRest", "NeedBookingDate");
            DropColumn("dbo.TypeOfRest", "NeedAccomodation");
        }
    }
}
