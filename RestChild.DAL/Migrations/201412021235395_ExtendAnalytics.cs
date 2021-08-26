namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendAnalytics : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnalyticsViewRow", "ByDayColor", c => c.String(maxLength: 1000));
            AddColumn("dbo.AnalyticsViewRow", "ByHourColor", c => c.String(maxLength: 1000));
            AddColumn("dbo.AnalyticsViewRow", "ByWeekColor", c => c.String(maxLength: 1000));
            AddColumn("dbo.AnalyticsViewRow", "TotalColor", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnalyticsViewRow", "TotalColor");
            DropColumn("dbo.AnalyticsViewRow", "ByWeekColor");
            DropColumn("dbo.AnalyticsViewRow", "ByHourColor");
            DropColumn("dbo.AnalyticsViewRow", "ByDayColor");
        }
    }
}
