namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HotelOrganizationMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hotels", "OrganizationId", c => c.Long());
            CreateIndex("dbo.Hotels", "OrganizationId");
            AddForeignKey("dbo.Hotels", "OrganizationId", "dbo.Organization", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Hotels", "OrganizationId", "dbo.Organization");
            DropIndex("dbo.Hotels", new[] { "OrganizationId" });
            DropColumn("dbo.Hotels", "OrganizationId");
        }
    }
}
