namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CounselorGroupTest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrainingCounselorsGroupTest",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateStart = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateEnd = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CounselorTestId = c.Long(),
                        TrainingCounselorsId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CounselorTest", t => t.CounselorTestId)
                .ForeignKey("dbo.TrainingCounselors", t => t.TrainingCounselorsId)
                .Index(t => t.CounselorTestId)
                .Index(t => t.TrainingCounselorsId);
            
            AddColumn("dbo.TrainingCounselorsTest", "TestGuid", c => c.Guid());
            AddColumn("dbo.TrainingCounselorsTest", "IsComplited", c => c.Boolean(nullable: false));
            AddColumn("dbo.TrainingCounselorsTest", "GroupTestId", c => c.Long());
            CreateIndex("dbo.TrainingCounselorsTest", "GroupTestId");
            AddForeignKey("dbo.TrainingCounselorsTest", "GroupTestId", "dbo.TrainingCounselorsGroupTest", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainingCounselorsTest", "GroupTestId", "dbo.TrainingCounselorsGroupTest");
            DropForeignKey("dbo.TrainingCounselorsGroupTest", "TrainingCounselorsId", "dbo.TrainingCounselors");
            DropForeignKey("dbo.TrainingCounselorsGroupTest", "CounselorTestId", "dbo.CounselorTest");
            DropIndex("dbo.TrainingCounselorsGroupTest", new[] { "TrainingCounselorsId" });
            DropIndex("dbo.TrainingCounselorsGroupTest", new[] { "CounselorTestId" });
            DropIndex("dbo.TrainingCounselorsTest", new[] { "GroupTestId" });
            DropColumn("dbo.TrainingCounselorsTest", "GroupTestId");
            DropColumn("dbo.TrainingCounselorsTest", "IsComplited");
            DropColumn("dbo.TrainingCounselorsTest", "TestGuid");
            DropTable("dbo.TrainingCounselorsGroupTest");
        }
    }
}
