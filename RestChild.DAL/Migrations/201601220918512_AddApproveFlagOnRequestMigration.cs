namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApproveFlagOnRequestMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Beneficiaries",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Applicant", "BenefitRequestComment", c => c.String());
            AddColumn("dbo.Applicant", "IsIncludeInInteragency", c => c.Boolean(nullable: false));
            AddColumn("dbo.Applicant", "IsApprovedInInteragency", c => c.Boolean());
            AddColumn("dbo.Applicant", "IsIncludeInInteragencySecondary", c => c.Boolean(nullable: false));
            AddColumn("dbo.Applicant", "IsApprovedInInteragencySecondary", c => c.Boolean());
            AddColumn("dbo.Applicant", "IsInvalid", c => c.Boolean(nullable: false));
            AddColumn("dbo.Applicant", "BenefitApprove", c => c.Boolean(nullable: false));
            AddColumn("dbo.Applicant", "BenefitApproveRequestDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Applicant", "BenefitApproveComment", c => c.String());
            AddColumn("dbo.Applicant", "BenefitApproveHtml", c => c.String());
            AddColumn("dbo.Applicant", "BenefitRequestNumber", c => c.String(maxLength: 1000));
            AddColumn("dbo.Applicant", "BenefitRequestDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Applicant", "BenefitAnswerNumber", c => c.String(maxLength: 1000));
            AddColumn("dbo.Applicant", "BenefitAnswerDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Applicant", "BenefitAnswerComment", c => c.String());
            AddColumn("dbo.ExchangeBaseRegistry", "ApplicantId", c => c.Long());
            AddColumn("dbo.AddonServicesLink", "Approved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Request", "BeneficiariesId", c => c.Long());
            AddColumn("dbo.RequestAccommodation", "Approved", c => c.Boolean(nullable: false));
            AddColumn("dbo.RequestAccommodationLink", "Approved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ticket", "Approved", c => c.Boolean(nullable: false));
            AddColumn("dbo.TicketLink", "Approved", c => c.Boolean(nullable: false));
            AddColumn("dbo.InteragencyRequestResult", "ApplicantId", c => c.Long());
            CreateIndex("dbo.ExchangeBaseRegistry", "ApplicantId");
            CreateIndex("dbo.Request", "BeneficiariesId");
            CreateIndex("dbo.InteragencyRequestResult", "ApplicantId");
            AddForeignKey("dbo.Request", "BeneficiariesId", "dbo.Beneficiaries", "Id");
            AddForeignKey("dbo.ExchangeBaseRegistry", "ApplicantId", "dbo.Applicant", "Id");
            AddForeignKey("dbo.InteragencyRequestResult", "ApplicantId", "dbo.Applicant", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InteragencyRequestResult", "ApplicantId", "dbo.Applicant");
            DropForeignKey("dbo.ExchangeBaseRegistry", "ApplicantId", "dbo.Applicant");
            DropForeignKey("dbo.Request", "BeneficiariesId", "dbo.Beneficiaries");
            DropIndex("dbo.InteragencyRequestResult", new[] { "ApplicantId" });
            DropIndex("dbo.Request", new[] { "BeneficiariesId" });
            DropIndex("dbo.ExchangeBaseRegistry", new[] { "ApplicantId" });
            DropColumn("dbo.InteragencyRequestResult", "ApplicantId");
            DropColumn("dbo.TicketLink", "Approved");
            DropColumn("dbo.Ticket", "Approved");
            DropColumn("dbo.RequestAccommodationLink", "Approved");
            DropColumn("dbo.RequestAccommodation", "Approved");
            DropColumn("dbo.Request", "BeneficiariesId");
            DropColumn("dbo.AddonServicesLink", "Approved");
            DropColumn("dbo.ExchangeBaseRegistry", "ApplicantId");
            DropColumn("dbo.Applicant", "BenefitAnswerComment");
            DropColumn("dbo.Applicant", "BenefitAnswerDate");
            DropColumn("dbo.Applicant", "BenefitAnswerNumber");
            DropColumn("dbo.Applicant", "BenefitRequestDate");
            DropColumn("dbo.Applicant", "BenefitRequestNumber");
            DropColumn("dbo.Applicant", "BenefitApproveHtml");
            DropColumn("dbo.Applicant", "BenefitApproveComment");
            DropColumn("dbo.Applicant", "BenefitApproveRequestDate");
            DropColumn("dbo.Applicant", "BenefitApprove");
            DropColumn("dbo.Applicant", "IsInvalid");
            DropColumn("dbo.Applicant", "IsApprovedInInteragencySecondary");
            DropColumn("dbo.Applicant", "IsIncludeInInteragencySecondary");
            DropColumn("dbo.Applicant", "IsApprovedInInteragency");
            DropColumn("dbo.Applicant", "IsIncludeInInteragency");
            DropColumn("dbo.Applicant", "BenefitRequestComment");
            DropTable("dbo.Beneficiaries");
        }
    }
}
