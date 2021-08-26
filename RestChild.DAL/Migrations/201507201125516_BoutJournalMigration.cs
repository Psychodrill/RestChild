namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoutJournalMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoutJournal",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(maxLength: 1000),
                        Description = c.String(),
                        EventDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateCreate = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateChange = c.DateTime(precision: 7, storeType: "datetime2"),
                        CounselorsId = c.Long(),
                        BoutId = c.Long(),
                        CounselorTaskExecutorTypeId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Counselors", t => t.CounselorsId)
                .ForeignKey("dbo.CounselorTaskExecutorType", t => t.CounselorTaskExecutorTypeId)
                .ForeignKey("dbo.Bout", t => t.BoutId)
                .Index(t => t.CounselorsId)
                .Index(t => t.BoutId)
                .Index(t => t.CounselorTaskExecutorTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BoutJournal", "BoutId", "dbo.Bout");
            DropForeignKey("dbo.BoutJournal", "CounselorTaskExecutorTypeId", "dbo.CounselorTaskExecutorType");
            DropForeignKey("dbo.BoutJournal", "CounselorsId", "dbo.Counselors");
            DropIndex("dbo.BoutJournal", new[] { "CounselorTaskExecutorTypeId" });
            DropIndex("dbo.BoutJournal", new[] { "BoutId" });
            DropIndex("dbo.BoutJournal", new[] { "CounselorsId" });
            DropTable("dbo.BoutJournal");
        }
    }
}
