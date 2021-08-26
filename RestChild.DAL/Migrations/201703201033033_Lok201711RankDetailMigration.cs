namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lok201711RankDetailMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ListTravelersRequestDetail",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Detail = c.String(),
                        ListTravelersRequestId = c.Long(),
                        ChildId = c.Long(),
                        ApplicantId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicant", t => t.ApplicantId)
                .ForeignKey("dbo.Child", t => t.ChildId)
                .ForeignKey("dbo.ListTravelersRequest", t => t.ListTravelersRequestId)
                .Index(t => t.ListTravelersRequestId)
                .Index(t => t.ChildId)
                .Index(t => t.ApplicantId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            AddColumn("dbo.YearOfRest", "ReceptionOfApplicationsCompleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.YearOfRest", "ListComplited", c => c.Boolean(nullable: false));
            AddColumn("dbo.YearOfRest", "TourOpened", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ListTravelersRequestDetail", "ListTravelersRequestId", "dbo.ListTravelersRequest");
            DropForeignKey("dbo.ListTravelersRequestDetail", "ChildId", "dbo.Child");
            DropForeignKey("dbo.ListTravelersRequestDetail", "ApplicantId", "dbo.Applicant");
            DropIndex("dbo.ListTravelersRequestDetail", new[] { "EidSendStatus" });
            DropIndex("dbo.ListTravelersRequestDetail", new[] { "Eid" });
            DropIndex("dbo.ListTravelersRequestDetail", new[] { "ApplicantId" });
            DropIndex("dbo.ListTravelersRequestDetail", new[] { "ChildId" });
            DropIndex("dbo.ListTravelersRequestDetail", new[] { "ListTravelersRequestId" });
            DropColumn("dbo.YearOfRest", "TourOpened");
            DropColumn("dbo.YearOfRest", "ListComplited");
            DropColumn("dbo.YearOfRest", "ReceptionOfApplicationsCompleted");
            DropTable("dbo.ListTravelersRequestDetail");
        }
    }
}
