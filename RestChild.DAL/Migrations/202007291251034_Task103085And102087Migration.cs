namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task103085And102087Migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RequestTimeOfRest", "Request_Id", "dbo.Request");
            DropForeignKey("dbo.RequestTimeOfRest", "TimeOfRest_Id", "dbo.TimeOfRest");
            DropForeignKey("dbo.PlaceOfRestRequest", "PlaceOfRest_Id", "dbo.PlaceOfRest");
            DropForeignKey("dbo.PlaceOfRestRequest", "Request_Id", "dbo.Request");
            DropIndex("dbo.RequestTimeOfRest", new[] { "Request_Id" });
            DropIndex("dbo.RequestTimeOfRest", new[] { "TimeOfRest_Id" });
            DropIndex("dbo.PlaceOfRestRequest", new[] { "PlaceOfRest_Id" });
            DropIndex("dbo.PlaceOfRestRequest", new[] { "Request_Id" });
            CreateTable(
                "dbo.RequestsTimeOfRest",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        TimeOfRestId = c.Long(nullable: false),
                        RequestId = c.Long(nullable: false),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Request", t => t.RequestId)
                .ForeignKey("dbo.TimeOfRest", t => t.TimeOfRestId)
                .Index(t => t.TimeOfRestId)
                .Index(t => t.RequestId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.RequestPlaceOfRest",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        RequestId = c.Long(nullable: false),
                        PlaceOfRestId = c.Long(nullable: false),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Request", t => t.RequestId)
                .ForeignKey("dbo.PlaceOfRest", t => t.PlaceOfRestId)
                .Index(t => t.RequestId)
                .Index(t => t.PlaceOfRestId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.DocumentTypeTypeOfRest",
                c => new
                    {
                        DocumentType_Id = c.Long(nullable: false),
                        TypeOfRest_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.DocumentType_Id, t.TypeOfRest_Id })
                .ForeignKey("dbo.DocumentType", t => t.DocumentType_Id, cascadeDelete: true)
                .ForeignKey("dbo.TypeOfRest", t => t.TypeOfRest_Id, cascadeDelete: true)
                .Index(t => t.DocumentType_Id)
                .Index(t => t.TypeOfRest_Id);

            var dtt = DateTime.Now.Ticks;
            Sql($"INSERT INTO [RequestPlaceOfRest] ([Order], [RequestId], [PlaceOfRestId], [LastUpdateTick]) Select (Select Count(1) From [PlaceOfRestRequest] r1 Where r1.Request_Id = r.Request_Id And r1.PlaceOfRest_Id <= r.PlaceOfRest_Id), r.Request_Id, r.PlaceOfRest_Id, {dtt} From [PlaceOfRestRequest] r");
            dtt = DateTime.Now.Ticks;
            Sql($"INSERT INTO [RequestsTimeOfRest] ([Order], [RequestId], [TimeOfRestId], [LastUpdateTick]) Select (Select Count(1) From [RequestTimeOfRest] r1 Where r1.Request_Id = r.Request_Id And r1.TimeOfRest_Id <= r.TimeOfRest_Id), r.Request_Id, r.TimeOfRest_Id, {dtt} From [RequestTimeOfRest] r");

            DropTable("dbo.RequestTimeOfRest");
            DropTable("dbo.PlaceOfRestRequest");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PlaceOfRestRequest",
                c => new
                    {
                        PlaceOfRest_Id = c.Long(nullable: false),
                        Request_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlaceOfRest_Id, t.Request_Id });
            
            CreateTable(
                "dbo.RequestTimeOfRest",
                c => new
                    {
                        Request_Id = c.Long(nullable: false),
                        TimeOfRest_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Request_Id, t.TimeOfRest_Id });
            
            DropForeignKey("dbo.RequestPlaceOfRest", "PlaceOfRestId", "dbo.PlaceOfRest");
            DropForeignKey("dbo.RequestsTimeOfRest", "TimeOfRestId", "dbo.TimeOfRest");
            DropForeignKey("dbo.RequestsTimeOfRest", "RequestId", "dbo.Request");
            DropForeignKey("dbo.RequestPlaceOfRest", "RequestId", "dbo.Request");
            DropForeignKey("dbo.DocumentTypeTypeOfRest", "TypeOfRest_Id", "dbo.TypeOfRest");
            DropForeignKey("dbo.DocumentTypeTypeOfRest", "DocumentType_Id", "dbo.DocumentType");
            DropIndex("dbo.DocumentTypeTypeOfRest", new[] { "TypeOfRest_Id" });
            DropIndex("dbo.DocumentTypeTypeOfRest", new[] { "DocumentType_Id" });
            DropIndex("dbo.RequestPlaceOfRest", new[] { "EidSendStatus" });
            DropIndex("dbo.RequestPlaceOfRest", new[] { "Eid" });
            DropIndex("dbo.RequestPlaceOfRest", new[] { "PlaceOfRestId" });
            DropIndex("dbo.RequestPlaceOfRest", new[] { "RequestId" });
            DropIndex("dbo.RequestsTimeOfRest", new[] { "EidSendStatus" });
            DropIndex("dbo.RequestsTimeOfRest", new[] { "Eid" });
            DropIndex("dbo.RequestsTimeOfRest", new[] { "RequestId" });
            DropIndex("dbo.RequestsTimeOfRest", new[] { "TimeOfRestId" });
            DropTable("dbo.DocumentTypeTypeOfRest");
            CreateIndex("dbo.PlaceOfRestRequest", "Request_Id");
            CreateIndex("dbo.PlaceOfRestRequest", "PlaceOfRest_Id");
            CreateIndex("dbo.RequestTimeOfRest", "TimeOfRest_Id");
            CreateIndex("dbo.RequestTimeOfRest", "Request_Id");
            AddForeignKey("dbo.PlaceOfRestRequest", "Request_Id", "dbo.Request", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PlaceOfRestRequest", "PlaceOfRest_Id", "dbo.PlaceOfRest", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RequestTimeOfRest", "TimeOfRest_Id", "dbo.TimeOfRest", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RequestTimeOfRest", "Request_Id", "dbo.Request", "Id", cascadeDelete: true);

            Sql("INSERT INTO [PlaceOfRestRequest] ([Request_Id], [PlaceOfRest_Id]) Select [RequestId], [PlaceOfRestId] From [RequestPlaceOfRest]");
            Sql("INSERT INTO [RequestTimeOfRest] ([Request_Id], [TimeOfRest_Id]) Select [RequestId], [TimeOfRestId] From [RequestsTimeOfRest]");

            DropTable("dbo.RequestPlaceOfRest");
            DropTable("dbo.RequestsTimeOfRest");

        }
    }
}
