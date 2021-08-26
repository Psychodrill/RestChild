namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConcelarusTaskCommentaryAddonMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CounselorTaskCommentary", "AccountId", "dbo.Account");
            DropForeignKey("dbo.CounselorTaskCommentary", "AdministratorTourId", "dbo.AdministratorTour");
            DropForeignKey("dbo.CounselorTaskCommentary", "CounselorsId", "dbo.Counselors");
            DropIndex("dbo.CounselorTaskCommentary", new[] { "AdministratorTourId" });
            DropIndex("dbo.CounselorTaskCommentary", new[] { "CounselorsId" });
            DropIndex("dbo.CounselorTaskCommentary", new[] { "AccountId" });
            AddColumn("dbo.ResponsibilityForTask", "Name", c => c.String(maxLength: 1000));
            AddColumn("dbo.CounselorTaskCommentary", "ResponsibilityForTaskId", c => c.Long());
            CreateIndex("dbo.CounselorTaskCommentary", "ResponsibilityForTaskId");
            AddForeignKey("dbo.CounselorTaskCommentary", "ResponsibilityForTaskId", "dbo.ResponsibilityForTask", "Id");
            DropColumn("dbo.CounselorTaskCommentary", "AdministratorTourId");
            DropColumn("dbo.CounselorTaskCommentary", "CounselorsId");
            DropColumn("dbo.CounselorTaskCommentary", "AccountId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CounselorTaskCommentary", "AccountId", c => c.Long());
            AddColumn("dbo.CounselorTaskCommentary", "CounselorsId", c => c.Long());
            AddColumn("dbo.CounselorTaskCommentary", "AdministratorTourId", c => c.Long());
            DropForeignKey("dbo.CounselorTaskCommentary", "ResponsibilityForTaskId", "dbo.ResponsibilityForTask");
            DropIndex("dbo.CounselorTaskCommentary", new[] { "ResponsibilityForTaskId" });
            DropColumn("dbo.CounselorTaskCommentary", "ResponsibilityForTaskId");
            DropColumn("dbo.ResponsibilityForTask", "Name");
            CreateIndex("dbo.CounselorTaskCommentary", "AccountId");
            CreateIndex("dbo.CounselorTaskCommentary", "CounselorsId");
            CreateIndex("dbo.CounselorTaskCommentary", "AdministratorTourId");
            AddForeignKey("dbo.CounselorTaskCommentary", "CounselorsId", "dbo.Counselors", "Id");
            AddForeignKey("dbo.CounselorTaskCommentary", "AdministratorTourId", "dbo.AdministratorTour", "Id");
            AddForeignKey("dbo.CounselorTaskCommentary", "AccountId", "dbo.Account", "Id");
        }
    }
}
