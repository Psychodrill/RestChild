namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task137275_2Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TradeUnionCamperPrivilegePart",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            AddColumn("dbo.Organization", "OKATO", c => c.String(maxLength: 1000));
            AddColumn("dbo.Organization", "ESNSIType", c => c.String(maxLength: 1000));
            AddColumn("dbo.TradeUnionCamper", "CashbackEstimatedAmount", c => c.Decimal(precision: 32, scale: 4));
            AddColumn("dbo.TradeUnionCamper", "CashbackBaseEstimatedAmount", c => c.Decimal(precision: 32, scale: 4));
            AddColumn("dbo.TradeUnionCamper", "ContractDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.TradeUnionCamper", "ContractNumber", c => c.String(maxLength: 1000));
            AddColumn("dbo.TradeUnionCamper", "FactDateIn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.TradeUnionCamper", "FactDateOut", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.TradeUnionCamper", "CashbackRequested", c => c.Boolean());
            AddColumn("dbo.TradeUnionCamper", "PrivilegePartId", c => c.Long());
            CreateIndex("dbo.TradeUnionCamper", "PrivilegePartId");
            AddForeignKey("dbo.TradeUnionCamper", "PrivilegePartId", "dbo.TradeUnionCamperPrivilegePart", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TradeUnionCamper", "PrivilegePartId", "dbo.TradeUnionCamperPrivilegePart");
            DropIndex("dbo.TradeUnionCamperPrivilegePart", new[] { "EidSendStatus" });
            DropIndex("dbo.TradeUnionCamperPrivilegePart", new[] { "Eid" });
            DropIndex("dbo.TradeUnionCamper", new[] { "PrivilegePartId" });
            DropColumn("dbo.TradeUnionCamper", "PrivilegePartId");
            DropColumn("dbo.TradeUnionCamper", "CashbackRequested");
            DropColumn("dbo.TradeUnionCamper", "FactDateOut");
            DropColumn("dbo.TradeUnionCamper", "FactDateIn");
            DropColumn("dbo.TradeUnionCamper", "ContractNumber");
            DropColumn("dbo.TradeUnionCamper", "ContractDate");
            DropColumn("dbo.TradeUnionCamper", "CashbackBaseEstimatedAmount");
            DropColumn("dbo.TradeUnionCamper", "CashbackEstimatedAmount");
            DropColumn("dbo.Organization", "ESNSIType");
            DropColumn("dbo.Organization", "OKATO");
            DropTable("dbo.TradeUnionCamperPrivilegePart");
        }
    }
}
