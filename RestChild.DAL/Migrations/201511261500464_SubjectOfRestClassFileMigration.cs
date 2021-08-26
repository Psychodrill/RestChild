namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubjectOfRestClassFileMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubjectOfRestClassification", "FileOrLinkId", c => c.Long());
            CreateIndex("dbo.SubjectOfRestClassification", "FileOrLinkId");
            AddForeignKey("dbo.SubjectOfRestClassification", "FileOrLinkId", "dbo.FileOrLink", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubjectOfRestClassification", "FileOrLinkId", "dbo.FileOrLink");
            DropIndex("dbo.SubjectOfRestClassification", new[] { "FileOrLinkId" });
            DropColumn("dbo.SubjectOfRestClassification", "FileOrLinkId");
        }
    }
}
