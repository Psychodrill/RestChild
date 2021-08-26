namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganizationEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organization", "EntityId", c => c.Long());
            CreateIndex("dbo.Organization", "EntityId");
            AddForeignKey("dbo.Organization", "EntityId", "dbo.Organization", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Organization", "EntityId", "dbo.Organization");
            DropIndex("dbo.Organization", new[] { "EntityId" });
            DropColumn("dbo.Organization", "EntityId");
        }
    }
}
