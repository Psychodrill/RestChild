namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class DeletedRecordListMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeletedRecord",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ClassName = c.String(nullable: false, maxLength: 400),
                        Uid = c.Long(nullable: false),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);

	        CreateIndex("dbo.DeletedRecord", new[] {"ClassName", "EidSendStatus"});
        }

        public override void Down()
        {
	        DropIndex("dbo.DeletedRecord", new[] {"ClassName", "EidSendStatus"});
			DropIndex("dbo.DeletedRecord", new[] { "EidSendStatus" });
            DropIndex("dbo.DeletedRecord", new[] { "Eid" });
            DropTable("dbo.DeletedRecord");
        }
    }
}
