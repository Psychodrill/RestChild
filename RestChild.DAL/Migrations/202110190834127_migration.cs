namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LeisureFacilities",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Abbreviated = c.String(),
                        Fullname = c.String(),
                        ActualAdress = c.String(),
                        Inn = c.String(maxLength: 1000),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.LeisureFacilities", new[] { "EidSendStatus" });
            DropIndex("dbo.LeisureFacilities", new[] { "Eid" });
            DropTable("dbo.LeisureFacilities");
        }
    }
}
