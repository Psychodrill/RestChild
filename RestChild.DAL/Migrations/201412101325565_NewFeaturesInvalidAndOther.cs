namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewFeaturesInvalidAndOther : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BenefitGroupInvalid",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Applicant", "ForeginSeria", c => c.String(maxLength: 1000));
            AddColumn("dbo.Applicant", "ForeginNumber", c => c.String(maxLength: 1000));
            AddColumn("dbo.Applicant", "ForeginSubjectIssue", c => c.String());
            AddColumn("dbo.Applicant", "ForeginDateOfIssue", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Applicant", "ForeginDateEnd", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Applicant", "ForeginTypeId", c => c.Long());
            AddColumn("dbo.Child", "IsIncludeInInteragencySecondary", c => c.Boolean());
            AddColumn("dbo.Child", "IsApprovedInInteragencySecondary", c => c.Boolean());
            AddColumn("dbo.Child", "IsInvalid", c => c.Boolean());
            AddColumn("dbo.Child", "BenefitGroupInvalidId", c => c.Long());
            AddColumn("dbo.InteragencyRequest", "IsSecondaryRequest", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Applicant", "ForeginTypeId");
            CreateIndex("dbo.Child", "BenefitGroupInvalidId");
            AddForeignKey("dbo.Child", "BenefitGroupInvalidId", "dbo.BenefitGroupInvalid", "Id");
            AddForeignKey("dbo.Applicant", "ForeginTypeId", "dbo.DocumentType", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applicant", "ForeginTypeId", "dbo.DocumentType");
            DropForeignKey("dbo.Child", "BenefitGroupInvalidId", "dbo.BenefitGroupInvalid");
            DropIndex("dbo.Child", new[] { "BenefitGroupInvalidId" });
            DropIndex("dbo.Applicant", new[] { "ForeginTypeId" });
            DropColumn("dbo.InteragencyRequest", "IsSecondaryRequest");
            DropColumn("dbo.Child", "BenefitGroupInvalidId");
            DropColumn("dbo.Child", "IsInvalid");
            DropColumn("dbo.Child", "IsApprovedInInteragencySecondary");
            DropColumn("dbo.Child", "IsIncludeInInteragencySecondary");
            DropColumn("dbo.Applicant", "ForeginTypeId");
            DropColumn("dbo.Applicant", "ForeginDateEnd");
            DropColumn("dbo.Applicant", "ForeginDateOfIssue");
            DropColumn("dbo.Applicant", "ForeginSubjectIssue");
            DropColumn("dbo.Applicant", "ForeginNumber");
            DropColumn("dbo.Applicant", "ForeginSeria");
            DropTable("dbo.BenefitGroupInvalid");
        }
    }
}
