namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppicantCamperMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicant", "IsApplicantCamper", c => c.Boolean(nullable: false));
            AddColumn("dbo.Applicant", "IsAgent", c => c.Boolean(nullable: false));
            AddColumn("dbo.Applicant", "ApplicantCamperId", c => c.Long());
            CreateIndex("dbo.Applicant", "ApplicantCamperId");
            AddForeignKey("dbo.Applicant", "ApplicantCamperId", "dbo.Child", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applicant", "ApplicantCamperId", "dbo.Child");
            DropIndex("dbo.Applicant", new[] { "ApplicantCamperId" });
            DropColumn("dbo.Applicant", "ApplicantCamperId");
            DropColumn("dbo.Applicant", "IsAgent");
            DropColumn("dbo.Applicant", "IsApplicantCamper");
        }
    }
}
