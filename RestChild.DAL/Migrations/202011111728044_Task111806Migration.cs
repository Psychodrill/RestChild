namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task111806Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TradeUnionPersonCheck",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PersonCheckType = c.Long(nullable: false),
                        IsProcessed = c.Boolean(nullable: false, defaultValue: false),
                        NotActual = c.Boolean(nullable: false, defaultValue: false),
                        PersonId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Person", t => t.PersonId)
                .Index(t => t.PersonId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.PersonTradeUnionPersonCheck",
                c => new
                    {
                        Person_Id = c.Long(nullable: false),
                        TradeUnionPersonCheck_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Person_Id, t.TradeUnionPersonCheck_Id })
                .ForeignKey("dbo.Person", t => t.Person_Id, cascadeDelete: true)
                .ForeignKey("dbo.TradeUnionPersonCheck", t => t.TradeUnionPersonCheck_Id, cascadeDelete: true)
                .Index(t => t.Person_Id)
                .Index(t => t.TradeUnionPersonCheck_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonTradeUnionPersonCheck", "TradeUnionPersonCheck_Id", "dbo.TradeUnionPersonCheck");
            DropForeignKey("dbo.PersonTradeUnionPersonCheck", "Person_Id", "dbo.Person");
            DropForeignKey("dbo.TradeUnionPersonCheck", "PersonId", "dbo.Person");
            DropIndex("dbo.PersonTradeUnionPersonCheck", new[] { "TradeUnionPersonCheck_Id" });
            DropIndex("dbo.PersonTradeUnionPersonCheck", new[] { "Person_Id" });
            DropIndex("dbo.TradeUnionPersonCheck", new[] { "EidSendStatus" });
            DropIndex("dbo.TradeUnionPersonCheck", new[] { "Eid" });
            DropIndex("dbo.TradeUnionPersonCheck", new[] { "PersonId" });
            DropTable("dbo.PersonTradeUnionPersonCheck");
            DropTable("dbo.TradeUnionPersonCheck");
        }
    }
}
