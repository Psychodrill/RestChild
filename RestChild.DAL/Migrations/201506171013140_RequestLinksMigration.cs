namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequestLinksMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestRoomRates",
                c => new
                    {
                        Request_Id = c.Long(nullable: false),
                        RoomRates_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Request_Id, t.RoomRates_Id })
                .ForeignKey("dbo.Request", t => t.Request_Id, cascadeDelete: true)
                .ForeignKey("dbo.RoomRates", t => t.RoomRates_Id, cascadeDelete: true)
                .Index(t => t.Request_Id)
                .Index(t => t.RoomRates_Id);
            
            AddColumn("dbo.Applicant", "RoomRatesId", c => c.Long());
            AddColumn("dbo.Child", "RoomRatesId", c => c.Long());
            AddColumn("dbo.AddonServicesLink", "DateCreate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddonServicesLink", "DateChange", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddonServicesLink", "HistoryLinkId", c => c.Long());
            AddColumn("dbo.AddonServicesLink", "AccountId", c => c.Long());
            AddColumn("dbo.AddonServicesLink", "StateId", c => c.Long());
            CreateIndex("dbo.Applicant", "RoomRatesId");
            CreateIndex("dbo.Child", "RoomRatesId");
            CreateIndex("dbo.AddonServicesLink", "HistoryLinkId");
            CreateIndex("dbo.AddonServicesLink", "AccountId");
            CreateIndex("dbo.AddonServicesLink", "StateId");
            AddForeignKey("dbo.AddonServicesLink", "AccountId", "dbo.Account", "Id");
            AddForeignKey("dbo.AddonServicesLink", "HistoryLinkId", "dbo.HistoryLink", "Id");
            AddForeignKey("dbo.AddonServicesLink", "StateId", "dbo.StateMachineState", "Id");
            AddForeignKey("dbo.Child", "RoomRatesId", "dbo.RoomRates", "Id");
            AddForeignKey("dbo.Applicant", "RoomRatesId", "dbo.RoomRates", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applicant", "RoomRatesId", "dbo.RoomRates");
            DropForeignKey("dbo.Child", "RoomRatesId", "dbo.RoomRates");
            DropForeignKey("dbo.RequestRoomRates", "RoomRates_Id", "dbo.RoomRates");
            DropForeignKey("dbo.RequestRoomRates", "Request_Id", "dbo.Request");
            DropForeignKey("dbo.AddonServicesLink", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.AddonServicesLink", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.AddonServicesLink", "AccountId", "dbo.Account");
            DropIndex("dbo.RequestRoomRates", new[] { "RoomRates_Id" });
            DropIndex("dbo.RequestRoomRates", new[] { "Request_Id" });
            DropIndex("dbo.AddonServicesLink", new[] { "StateId" });
            DropIndex("dbo.AddonServicesLink", new[] { "AccountId" });
            DropIndex("dbo.AddonServicesLink", new[] { "HistoryLinkId" });
            DropIndex("dbo.Child", new[] { "RoomRatesId" });
            DropIndex("dbo.Applicant", new[] { "RoomRatesId" });
            DropColumn("dbo.AddonServicesLink", "StateId");
            DropColumn("dbo.AddonServicesLink", "AccountId");
            DropColumn("dbo.AddonServicesLink", "HistoryLinkId");
            DropColumn("dbo.AddonServicesLink", "DateChange");
            DropColumn("dbo.AddonServicesLink", "DateCreate");
            DropColumn("dbo.Child", "RoomRatesId");
            DropColumn("dbo.Applicant", "RoomRatesId");
            DropTable("dbo.RequestRoomRates");
        }
    }
}
