namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnalyticsRowTypeMigr : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnalyticsViewRowType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AnalyticsViewRow", "ByDay2", c => c.Int());
            AddColumn("dbo.AnalyticsViewRow", "ByHour2", c => c.Int());
            AddColumn("dbo.AnalyticsViewRow", "ByWeek2", c => c.Int());
            AddColumn("dbo.AnalyticsViewRow", "Total2", c => c.Int());
            AddColumn("dbo.AnalyticsViewRow", "AnalyticsViewRowTypeId", c => c.Long());
            CreateIndex("dbo.AnalyticsViewRow", "AnalyticsViewRowTypeId");
            AddForeignKey("dbo.AnalyticsViewRow", "AnalyticsViewRowTypeId", "dbo.AnalyticsViewRowType", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnalyticsViewRow", "AnalyticsViewRowTypeId", "dbo.AnalyticsViewRowType");
            DropIndex("dbo.AnalyticsViewRow", new[] { "AnalyticsViewRowTypeId" });
            DropColumn("dbo.AnalyticsViewRow", "AnalyticsViewRowTypeId");
            DropColumn("dbo.AnalyticsViewRow", "Total2");
            DropColumn("dbo.AnalyticsViewRow", "ByWeek2");
            DropColumn("dbo.AnalyticsViewRow", "ByHour2");
            DropColumn("dbo.AnalyticsViewRow", "ByDay2");
            DropTable("dbo.AnalyticsViewRowType");
        }
    }
}
