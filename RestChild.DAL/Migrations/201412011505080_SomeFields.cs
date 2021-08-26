namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BtiDistrict", "Givz", c => c.Long(nullable: false));
            AddColumn("dbo.BtiDistrict", "IsVisible", c => c.Boolean(nullable: false));
            AddColumn("dbo.BtiRegion", "Givz", c => c.Long(nullable: false));
            AddColumn("dbo.BtiRegion", "IsVisible", c => c.Boolean(nullable: false));
            AddColumn("dbo.Applicant", "IsAccomp", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Account", "DateCreate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Account", "DateUpdate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Agent", "DocumentDateOfIssue", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Agent", "ProxyDateOfIssure", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Agent", "ProxyEndDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Applicant", "DocumentDateOfIssue", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Attendant", "DocumentDateOfIssue", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Child", "DocumentDateOfIssue", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Child", "DateOfBirth", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Child", "BenefitDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Child", "BenefitEndDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Child", "BenefitDateOfIssure", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Child", "ForeginDateOfIssue", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Child", "ForeginDateEnd", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Child", "BenefitRequestDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Child", "BenefitAnswerDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ExchangeBaseRegistry", "SendDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ExchangeBaseRegistry", "ResponseDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Request", "DateRequest", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Request", "UpdateDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.RequestCurrentPeriod", "DateFirstStage", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.RequestCurrentPeriod", "DateSecondStage", c => c.DateTime(precision: 7, storeType: "datetime2"));
			RenameColumn("dbo.ExchangeUTS", "DateCreate", "DateCreate2");
			AddColumn("dbo.ExchangeUTS", "DateCreate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
			Sql("Update dbo.ExchangeUTS set DateCreate = DateCreate2");
			DropColumn("dbo.ExchangeUTS", "DateCreate2");
            AlterColumn("dbo.HistoryInteragencyRequest", "OperationDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.InteragencyRequest", "RequsetDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.InteragencyRequest", "AnswerDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.InteragencyRequest", "CreateDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.HistoryRequest", "OperationDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HistoryRequest", "OperationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.InteragencyRequest", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.InteragencyRequest", "AnswerDate", c => c.DateTime());
            AlterColumn("dbo.InteragencyRequest", "RequsetDate", c => c.DateTime());
            AlterColumn("dbo.HistoryInteragencyRequest", "OperationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ExchangeUTS", "DateCreate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RequestCurrentPeriod", "DateSecondStage", c => c.DateTime());
            AlterColumn("dbo.RequestCurrentPeriod", "DateFirstStage", c => c.DateTime());
            AlterColumn("dbo.Request", "UpdateDate", c => c.DateTime());
            AlterColumn("dbo.Request", "DateRequest", c => c.DateTime());
            AlterColumn("dbo.ExchangeBaseRegistry", "ResponseDate", c => c.DateTime());
            AlterColumn("dbo.ExchangeBaseRegistry", "SendDate", c => c.DateTime());
            AlterColumn("dbo.Child", "BenefitAnswerDate", c => c.DateTime());
            AlterColumn("dbo.Child", "BenefitRequestDate", c => c.DateTime());
            AlterColumn("dbo.Child", "ForeginDateEnd", c => c.DateTime());
            AlterColumn("dbo.Child", "ForeginDateOfIssue", c => c.DateTime());
            AlterColumn("dbo.Child", "BenefitDateOfIssure", c => c.DateTime());
            AlterColumn("dbo.Child", "BenefitEndDate", c => c.DateTime());
            AlterColumn("dbo.Child", "BenefitDate", c => c.DateTime());
            AlterColumn("dbo.Child", "DateOfBirth", c => c.DateTime());
            AlterColumn("dbo.Child", "DocumentDateOfIssue", c => c.DateTime());
            AlterColumn("dbo.Attendant", "DocumentDateOfIssue", c => c.DateTime());
            AlterColumn("dbo.Applicant", "DocumentDateOfIssue", c => c.DateTime());
            AlterColumn("dbo.Agent", "ProxyEndDate", c => c.DateTime());
            AlterColumn("dbo.Agent", "ProxyDateOfIssure", c => c.DateTime());
            AlterColumn("dbo.Agent", "DocumentDateOfIssue", c => c.DateTime());
            AlterColumn("dbo.Account", "DateUpdate", c => c.DateTime());
            AlterColumn("dbo.Account", "DateCreate", c => c.DateTime());
            DropColumn("dbo.Applicant", "IsAccomp");
            DropColumn("dbo.BtiRegion", "IsVisible");
            DropColumn("dbo.BtiRegion", "Givz");
            DropColumn("dbo.BtiDistrict", "IsVisible");
            DropColumn("dbo.BtiDistrict", "Givz");
        }
    }
}
