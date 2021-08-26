namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task95753Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PupilGroupVacationPeriod",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            AddColumn("dbo.PupilGroup", "VacationPeriodId", c => c.Long());
            CreateIndex("dbo.PupilGroup", "VacationPeriodId");
            AddForeignKey("dbo.PupilGroup", "VacationPeriodId", "dbo.PupilGroupVacationPeriod", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PupilGroup", "VacationPeriodId", "dbo.PupilGroupVacationPeriod");
            DropIndex("dbo.PupilGroupVacationPeriod", new[] { "EidSendStatus" });
            DropIndex("dbo.PupilGroupVacationPeriod", new[] { "Eid" });
            DropIndex("dbo.PupilGroup", new[] { "VacationPeriodId" });
            DropColumn("dbo.PupilGroup", "VacationPeriodId");
            DropTable("dbo.PupilGroupVacationPeriod");
        }
    }
}
