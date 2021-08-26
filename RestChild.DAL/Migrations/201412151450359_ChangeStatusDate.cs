namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStatusDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnalyticsViewRow", "Day1", c => c.Int());
            AddColumn("dbo.AnalyticsViewRow", "Day2", c => c.Int());
            AddColumn("dbo.AnalyticsViewRow", "Day3", c => c.Int());
            AddColumn("dbo.AnalyticsViewRow", "Day4", c => c.Int());
            AddColumn("dbo.AnalyticsViewRow", "Day5", c => c.Int());
            AddColumn("dbo.AnalyticsViewRow", "Day6", c => c.Int());
            AddColumn("dbo.AnalyticsViewRow", "DataDay1", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AnalyticsViewRow", "DataDay2", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AnalyticsViewRow", "DataDay3", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AnalyticsViewRow", "DataDay4", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AnalyticsViewRow", "DataDay5", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AnalyticsViewRow", "DataDay6", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Request", "DateChangeStatus", c => c.DateTime(precision: 7, storeType: "datetime2"));
			Sql("update dbo.Request set DateChangeStatus = UpdateDate");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Request", "DateChangeStatus");
            DropColumn("dbo.AnalyticsViewRow", "DataDay6");
            DropColumn("dbo.AnalyticsViewRow", "DataDay5");
            DropColumn("dbo.AnalyticsViewRow", "DataDay4");
            DropColumn("dbo.AnalyticsViewRow", "DataDay3");
            DropColumn("dbo.AnalyticsViewRow", "DataDay2");
            DropColumn("dbo.AnalyticsViewRow", "DataDay1");
            DropColumn("dbo.AnalyticsViewRow", "Day6");
            DropColumn("dbo.AnalyticsViewRow", "Day5");
            DropColumn("dbo.AnalyticsViewRow", "Day4");
            DropColumn("dbo.AnalyticsViewRow", "Day3");
            DropColumn("dbo.AnalyticsViewRow", "Day2");
            DropColumn("dbo.AnalyticsViewRow", "Day1");
        }
    }
}
