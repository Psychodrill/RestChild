namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkToChildAndApplicantAddService : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddonServicesLink", "ChildId", c => c.Long());
            AddColumn("dbo.AddonServicesLink", "ApplicantId", c => c.Long());
            CreateIndex("dbo.AddonServicesLink", "ChildId");
            CreateIndex("dbo.AddonServicesLink", "ApplicantId");
            AddForeignKey("dbo.AddonServicesLink", "ApplicantId", "dbo.Applicant", "Id");
            AddForeignKey("dbo.AddonServicesLink", "ChildId", "dbo.Child", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AddonServicesLink", "ChildId", "dbo.Child");
            DropForeignKey("dbo.AddonServicesLink", "ApplicantId", "dbo.Applicant");
            DropIndex("dbo.AddonServicesLink", new[] { "ApplicantId" });
            DropIndex("dbo.AddonServicesLink", new[] { "ChildId" });
            DropColumn("dbo.AddonServicesLink", "ApplicantId");
            DropColumn("dbo.AddonServicesLink", "ChildId");
        }
    }
}
