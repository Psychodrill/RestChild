namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContractRelationMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DirectoryFlights", "OrganizationId", "dbo.Organization");
            DropIndex("dbo.DirectoryFlights", new[] { "OrganizationId" });
            AddColumn("dbo.DirectoryFlights", "ContractId", c => c.Long());
            AddColumn("dbo.Contract", "OnRest", c => c.Boolean(nullable: false));
            AddColumn("dbo.Contract", "OnTransport", c => c.Boolean(nullable: false));
            CreateIndex("dbo.DirectoryFlights", "ContractId");
            AddForeignKey("dbo.DirectoryFlights", "ContractId", "dbo.Contract", "Id");
            DropColumn("dbo.DirectoryFlights", "OrganizationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DirectoryFlights", "OrganizationId", c => c.Long());
            DropForeignKey("dbo.DirectoryFlights", "ContractId", "dbo.Contract");
            DropIndex("dbo.DirectoryFlights", new[] { "ContractId" });
            DropColumn("dbo.Contract", "OnTransport");
            DropColumn("dbo.Contract", "OnRest");
            DropColumn("dbo.DirectoryFlights", "ContractId");
            CreateIndex("dbo.DirectoryFlights", "OrganizationId");
            AddForeignKey("dbo.DirectoryFlights", "OrganizationId", "dbo.Organization", "Id");
        }
    }
}
