namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CshedIntegrationMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestStatusCshedSendAndSignDocument",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DocumentPath = c.String(nullable: false, maxLength: 1000),
                        SignNeed = c.Boolean(nullable: false),
                        MpguStatusId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RequestStatusForMpgu", t => t.MpguStatusId)
                .Index(t => t.MpguStatusId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequestStatusCshedSendAndSignDocument", "MpguStatusId", "dbo.RequestStatusForMpgu");
            DropIndex("dbo.RequestStatusCshedSendAndSignDocument", new[] { "EidSendStatus" });
            DropIndex("dbo.RequestStatusCshedSendAndSignDocument", new[] { "Eid" });
            DropIndex("dbo.RequestStatusCshedSendAndSignDocument", new[] { "MpguStatusId" });
            DropTable("dbo.RequestStatusCshedSendAndSignDocument");
        }
    }
}
