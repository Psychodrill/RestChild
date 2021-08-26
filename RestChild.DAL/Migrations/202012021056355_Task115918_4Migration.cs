namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task115918_4Migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MonitoringSmallLeisureInfoData", "MonitoringSmallLeisureInfoId", "dbo.MonitoringSmallLeisureInfo");
            DropForeignKey("dbo.MonitoringSmallLeisureInfoData", "GBUId", "dbo.MonitoringGBU");
            DropIndex("dbo.MonitoringSmallLeisureInfoData", new[] { "MonitoringSmallLeisureInfoId" });
            DropIndex("dbo.MonitoringSmallLeisureInfoData", new[] { "GBUId" });
            CreateTable(
                "dbo.MonitoringSmallLeisureInfoGBU",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LastUploadData = c.DateTime(precision: 7, storeType: "datetime2"),
                        MonitoringSmallLeisureInfoId = c.Long(),
                        GBUId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MonitoringSmallLeisureInfo", t => t.MonitoringSmallLeisureInfoId)
                .ForeignKey("dbo.MonitoringGBU", t => t.GBUId)
                .Index(t => t.MonitoringSmallLeisureInfoId)
                .Index(t => t.GBUId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            AddColumn("dbo.MonitoringSmallLeisureInfoData", "MonitoringSmallLeisureInfoGBUId", c => c.Long());
            CreateIndex("dbo.MonitoringSmallLeisureInfoData", "MonitoringSmallLeisureInfoGBUId");
            AddForeignKey("dbo.MonitoringSmallLeisureInfoData", "MonitoringSmallLeisureInfoGBUId", "dbo.MonitoringSmallLeisureInfoGBU", "Id");
            DropColumn("dbo.MonitoringSmallLeisureInfoData", "LastUploadData");
            DropColumn("dbo.MonitoringSmallLeisureInfoData", "MonitoringSmallLeisureInfoId");
            DropColumn("dbo.MonitoringSmallLeisureInfoData", "GBUId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MonitoringSmallLeisureInfoData", "GBUId", c => c.Long());
            AddColumn("dbo.MonitoringSmallLeisureInfoData", "MonitoringSmallLeisureInfoId", c => c.Long());
            AddColumn("dbo.MonitoringSmallLeisureInfoData", "LastUploadData", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropForeignKey("dbo.MonitoringSmallLeisureInfoGBU", "GBUId", "dbo.MonitoringGBU");
            DropForeignKey("dbo.MonitoringSmallLeisureInfoData", "MonitoringSmallLeisureInfoGBUId", "dbo.MonitoringSmallLeisureInfoGBU");
            DropForeignKey("dbo.MonitoringSmallLeisureInfoGBU", "MonitoringSmallLeisureInfoId", "dbo.MonitoringSmallLeisureInfo");
            DropIndex("dbo.MonitoringSmallLeisureInfoData", new[] { "MonitoringSmallLeisureInfoGBUId" });
            DropIndex("dbo.MonitoringSmallLeisureInfoGBU", new[] { "EidSendStatus" });
            DropIndex("dbo.MonitoringSmallLeisureInfoGBU", new[] { "Eid" });
            DropIndex("dbo.MonitoringSmallLeisureInfoGBU", new[] { "GBUId" });
            DropIndex("dbo.MonitoringSmallLeisureInfoGBU", new[] { "MonitoringSmallLeisureInfoId" });
            DropColumn("dbo.MonitoringSmallLeisureInfoData", "MonitoringSmallLeisureInfoGBUId");
            DropTable("dbo.MonitoringSmallLeisureInfoGBU");
            CreateIndex("dbo.MonitoringSmallLeisureInfoData", "GBUId");
            CreateIndex("dbo.MonitoringSmallLeisureInfoData", "MonitoringSmallLeisureInfoId");
            AddForeignKey("dbo.MonitoringSmallLeisureInfoData", "GBUId", "dbo.MonitoringGBU", "Id");
            AddForeignKey("dbo.MonitoringSmallLeisureInfoData", "MonitoringSmallLeisureInfoId", "dbo.MonitoringSmallLeisureInfo", "Id");
        }
    }
}
