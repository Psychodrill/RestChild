namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganizationInIntegracyRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InteragencyRequest", "OrganizationId", c => c.Long());
            CreateIndex("dbo.InteragencyRequest", "OrganizationId");
            AddForeignKey("dbo.InteragencyRequest", "OrganizationId", "dbo.Organization", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InteragencyRequest", "OrganizationId", "dbo.Organization");
            DropIndex("dbo.InteragencyRequest", new[] { "OrganizationId" });
            DropColumn("dbo.InteragencyRequest", "OrganizationId");
        }
    }
}
