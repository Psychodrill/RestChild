namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CounselorTestAddonFieldsMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CounselorTestQuestion", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CounselorTestQuestion", "SortOrder", c => c.Int(nullable: false));
            AddColumn("dbo.CounselorTestAnswerVariant", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CounselorTestAnswerVariant", "SortOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CounselorTestAnswerVariant", "SortOrder");
            DropColumn("dbo.CounselorTestAnswerVariant", "IsDeleted");
            DropColumn("dbo.CounselorTestQuestion", "SortOrder");
            DropColumn("dbo.CounselorTestQuestion", "IsDeleted");
        }
    }
}
