namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotForSiteMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddonServices", "NotForSite", c => c.Boolean(nullable: false));
            AddColumn("dbo.Hotels", "NotForSite", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tour", "NotForSite", c => c.Boolean(nullable: false));
            AddColumn("dbo.Request", "IsApplicantOrganization", c => c.Boolean(nullable: false));
            AddColumn("dbo.Request", "ApplicantOrganizationId", c => c.Long());
            CreateIndex("dbo.Request", "ApplicantOrganizationId");
            AddForeignKey("dbo.Request", "ApplicantOrganizationId", "dbo.Organization", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Request", "ApplicantOrganizationId", "dbo.Organization");
            DropIndex("dbo.Request", new[] { "ApplicantOrganizationId" });
            DropColumn("dbo.Request", "ApplicantOrganizationId");
            DropColumn("dbo.Request", "IsApplicantOrganization");
            DropColumn("dbo.Tour", "NotForSite");
            DropColumn("dbo.Hotels", "NotForSite");
            DropColumn("dbo.AddonServices", "NotForSite");
        }
    }
}
