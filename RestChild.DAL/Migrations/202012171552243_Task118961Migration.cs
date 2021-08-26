namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task118961Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MonitoringSmallLeisureInfo", "LinkToFilesId", c => c.Long());
            AddColumn("dbo.MonitoringChildrenNumberInformation", "LinkToFilesId", c => c.Long());
            AddColumn("dbo.MonitoringFinancialInformation", "LinkToFilesId", c => c.Long());
            CreateIndex("dbo.MonitoringSmallLeisureInfo", "LinkToFilesId");
            CreateIndex("dbo.MonitoringChildrenNumberInformation", "LinkToFilesId");
            CreateIndex("dbo.MonitoringFinancialInformation", "LinkToFilesId");
            AddForeignKey("dbo.MonitoringSmallLeisureInfo", "LinkToFilesId", "dbo.LinkToFile", "Id");
            AddForeignKey("dbo.MonitoringChildrenNumberInformation", "LinkToFilesId", "dbo.LinkToFile", "Id");
            AddForeignKey("dbo.MonitoringFinancialInformation", "LinkToFilesId", "dbo.LinkToFile", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MonitoringFinancialInformation", "LinkToFilesId", "dbo.LinkToFile");
            DropForeignKey("dbo.MonitoringChildrenNumberInformation", "LinkToFilesId", "dbo.LinkToFile");
            DropForeignKey("dbo.MonitoringSmallLeisureInfo", "LinkToFilesId", "dbo.LinkToFile");
            DropIndex("dbo.MonitoringFinancialInformation", new[] { "LinkToFilesId" });
            DropIndex("dbo.MonitoringChildrenNumberInformation", new[] { "LinkToFilesId" });
            DropIndex("dbo.MonitoringSmallLeisureInfo", new[] { "LinkToFilesId" });
            DropColumn("dbo.MonitoringFinancialInformation", "LinkToFilesId");
            DropColumn("dbo.MonitoringChildrenNumberInformation", "LinkToFilesId");
            DropColumn("dbo.MonitoringSmallLeisureInfo", "LinkToFilesId");
        }
    }
}
