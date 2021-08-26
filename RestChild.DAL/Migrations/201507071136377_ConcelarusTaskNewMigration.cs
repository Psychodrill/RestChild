namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConcelarusTaskNewMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Counselors", "CounselorPracticeId", "dbo.CounselorPractice");
            DropIndex("dbo.Counselors", new[] { "CounselorPracticeId" });
            AddColumn("dbo.Counselors", "LinkOk", c => c.String(maxLength: 1000));
            AddColumn("dbo.Counselors", "LinkLinkedIn", c => c.String(maxLength: 1000));
            AddColumn("dbo.CounselorPractice", "CounselorsId", c => c.Long());
            CreateIndex("dbo.CounselorPractice", "CounselorsId");
            AddForeignKey("dbo.CounselorPractice", "CounselorsId", "dbo.Counselors", "Id");
            DropColumn("dbo.Counselors", "LinkОк");
            DropColumn("dbo.Counselors", "LinkInstagramm");
            DropColumn("dbo.Counselors", "CounselorPracticeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Counselors", "CounselorPracticeId", c => c.Long());
            AddColumn("dbo.Counselors", "LinkInstagramm", c => c.String(maxLength: 1000));
            AddColumn("dbo.Counselors", "LinkОк", c => c.String(maxLength: 1000));
            DropForeignKey("dbo.CounselorPractice", "CounselorsId", "dbo.Counselors");
            DropIndex("dbo.CounselorPractice", new[] { "CounselorsId" });
            DropColumn("dbo.CounselorPractice", "CounselorsId");
            DropColumn("dbo.Counselors", "LinkLinkedIn");
            DropColumn("dbo.Counselors", "LinkOk");
            CreateIndex("dbo.Counselors", "CounselorPracticeId");
            AddForeignKey("dbo.Counselors", "CounselorPracticeId", "dbo.CounselorPractice", "Id");
        }
    }
}
