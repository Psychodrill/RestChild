namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhotosAndRatingMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CounselorFile",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 1000),
                        FileUrl = c.String(nullable: false, maxLength: 1000),
                        CounselorsId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Counselors", t => t.CounselorsId)
                .Index(t => t.CounselorsId);
            
            CreateTable(
                "dbo.SubjectOfRestFile",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 1000),
                        FileUrl = c.String(nullable: false, maxLength: 1000),
                        SubjectOfRestId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubjectOfRest", t => t.SubjectOfRestId)
                .Index(t => t.SubjectOfRestId);
            
            AddColumn("dbo.Counselors", "Rating", c => c.Single());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubjectOfRestFile", "SubjectOfRestId", "dbo.SubjectOfRest");
            DropForeignKey("dbo.CounselorFile", "CounselorsId", "dbo.Counselors");
            DropIndex("dbo.SubjectOfRestFile", new[] { "SubjectOfRestId" });
            DropIndex("dbo.CounselorFile", new[] { "CounselorsId" });
            DropColumn("dbo.Counselors", "Rating");
            DropTable("dbo.SubjectOfRestFile");
            DropTable("dbo.CounselorFile");
        }
    }
}
