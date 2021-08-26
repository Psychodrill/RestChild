namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TradeUnProfToChldMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Request", "InternalCommentary", c => c.String());
            AddColumn("dbo.TradeUnionCamper", "TradeUnionOrganizationId", c => c.Long());
            CreateIndex("dbo.TradeUnionCamper", "TradeUnionOrganizationId");
            AddForeignKey("dbo.TradeUnionCamper", "TradeUnionOrganizationId", "dbo.Organization", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TradeUnionCamper", "TradeUnionOrganizationId", "dbo.Organization");
            DropIndex("dbo.TradeUnionCamper", new[] { "TradeUnionOrganizationId" });
            DropColumn("dbo.TradeUnionCamper", "TradeUnionOrganizationId");
            DropColumn("dbo.Request", "InternalCommentary");
        }
    }
}
