namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Release20200909Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlaceOfRestTypeOfRest",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TypeOfRestId = c.Long(),
                        PlaceOfRestId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeOfRest", t => t.TypeOfRestId)
                .ForeignKey("dbo.PlaceOfRest", t => t.PlaceOfRestId)
                .Index(t => t.TypeOfRestId)
                .Index(t => t.PlaceOfRestId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlaceOfRestTypeOfRest", "PlaceOfRestId", "dbo.PlaceOfRest");
            DropForeignKey("dbo.PlaceOfRestTypeOfRest", "TypeOfRestId", "dbo.TypeOfRest");
            DropIndex("dbo.PlaceOfRestTypeOfRest", new[] { "EidSendStatus" });
            DropIndex("dbo.PlaceOfRestTypeOfRest", new[] { "Eid" });
            DropIndex("dbo.PlaceOfRestTypeOfRest", new[] { "PlaceOfRestId" });
            DropIndex("dbo.PlaceOfRestTypeOfRest", new[] { "TypeOfRestId" });
            DropTable("dbo.PlaceOfRestTypeOfRest");
        }
    }
}
