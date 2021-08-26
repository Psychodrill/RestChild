namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JournalBoutActiveMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.History", "AuthorString", c => c.String(maxLength: 1000));
            AddColumn("dbo.BoutJournal", "IsArchive", c => c.Boolean(nullable: false));
            AddColumn("dbo.BoutJournal", "HistoryLinkId", c => c.Long());
            CreateIndex("dbo.BoutJournal", "HistoryLinkId");
            AddForeignKey("dbo.BoutJournal", "HistoryLinkId", "dbo.HistoryLink", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BoutJournal", "HistoryLinkId", "dbo.HistoryLink");
            DropIndex("dbo.BoutJournal", new[] { "HistoryLinkId" });
            DropColumn("dbo.BoutJournal", "HistoryLinkId");
            DropColumn("dbo.BoutJournal", "IsArchive");
            DropColumn("dbo.History", "AuthorString");
        }
    }
}
