namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task139451Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TypeOfTransportInRequest",
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
            
            AddColumn("dbo.Request", "AdditionalTypeOfTransportInRequestId", c => c.Long());
            AddColumn("dbo.Request", "PriorityTypeOfTransportInRequestId", c => c.Long());
            CreateIndex("dbo.Request", "AdditionalTypeOfTransportInRequestId");
            CreateIndex("dbo.Request", "PriorityTypeOfTransportInRequestId");
            AddForeignKey("dbo.Request", "AdditionalTypeOfTransportInRequestId", "dbo.TypeOfTransportInRequest", "Id");
            AddForeignKey("dbo.Request", "PriorityTypeOfTransportInRequestId", "dbo.TypeOfTransportInRequest", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Request", "PriorityTypeOfTransportInRequestId", "dbo.TypeOfTransportInRequest");
            DropForeignKey("dbo.Request", "AdditionalTypeOfTransportInRequestId", "dbo.TypeOfTransportInRequest");
            DropIndex("dbo.TypeOfTransportInRequest", new[] { "EidSendStatus" });
            DropIndex("dbo.TypeOfTransportInRequest", new[] { "Eid" });
            DropIndex("dbo.Request", new[] { "PriorityTypeOfTransportInRequestId" });
            DropIndex("dbo.Request", new[] { "AdditionalTypeOfTransportInRequestId" });
            DropColumn("dbo.Request", "PriorityTypeOfTransportInRequestId");
            DropColumn("dbo.Request", "AdditionalTypeOfTransportInRequestId");
            DropTable("dbo.TypeOfTransportInRequest");
        }
    }
}
