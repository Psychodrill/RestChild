namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HistoryLinkHotels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hotels", "HistoryLinkId", c => c.Long());
            CreateIndex("dbo.Hotels", "HistoryLinkId");
            AddForeignKey("dbo.Hotels", "HistoryLinkId", "dbo.HistoryLink", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Hotels", "HistoryLinkId", "dbo.HistoryLink");
            DropIndex("dbo.Hotels", new[] { "HistoryLinkId" });
            DropColumn("dbo.Hotels", "HistoryLinkId");
        }
    }
}
