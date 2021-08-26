namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lok20178ApplicantAddressMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicant", "AddressId", c => c.Long());
            AddColumn("dbo.Applicant", "StatusByChildId", c => c.Long());
            AddColumn("dbo.Request", "BankCorr", c => c.String(maxLength: 1000));
            AddColumn("dbo.Request", "BankLastName", c => c.String(maxLength: 1000));
            AddColumn("dbo.Request", "BankFirstName", c => c.String(maxLength: 1000));
            AddColumn("dbo.Request", "BankMiddleName", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Request", "StatusApplicant", c => c.String(maxLength: 1000));
            CreateIndex("dbo.Applicant", "AddressId");
            CreateIndex("dbo.Applicant", "StatusByChildId");
            AddForeignKey("dbo.Applicant", "AddressId", "dbo.Address", "Id");
            AddForeignKey("dbo.Applicant", "StatusByChildId", "dbo.StatusByChild", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applicant", "StatusByChildId", "dbo.StatusByChild");
            DropForeignKey("dbo.Applicant", "AddressId", "dbo.Address");
            DropIndex("dbo.Applicant", new[] { "StatusByChildId" });
            DropIndex("dbo.Applicant", new[] { "AddressId" });
            AlterColumn("dbo.Request", "StatusApplicant", c => c.Long());
            DropColumn("dbo.Request", "BankMiddleName");
            DropColumn("dbo.Request", "BankFirstName");
            DropColumn("dbo.Request", "BankLastName");
            DropColumn("dbo.Request", "BankCorr");
            DropColumn("dbo.Applicant", "StatusByChildId");
            DropColumn("dbo.Applicant", "AddressId");
        }
    }
}
