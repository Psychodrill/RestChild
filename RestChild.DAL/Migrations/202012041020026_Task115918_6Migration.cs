namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task115918_6Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MonitoringSmallLeisureInfo", "OrganisationId", c => c.Long());
            AddColumn("dbo.SmallLeisureType", "IsTextData", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.SmallLeisureType", "Formula", c => c.String(maxLength: 1000));
            AddColumn("dbo.MonitoringFinancialData", "Formula", c => c.String(maxLength: 1000));
            AddColumn("dbo.MonitoringFinancialSource", "ShowInForm", c => c.Boolean(nullable: false, defaultValue: true));
            AddColumn("dbo.MonitoringFinancialSource", "ParrentId", c => c.Long());
            CreateIndex("dbo.MonitoringSmallLeisureInfo", "OrganisationId");
            CreateIndex("dbo.MonitoringFinancialSource", "ParrentId");
            AddForeignKey("dbo.MonitoringSmallLeisureInfo", "OrganisationId", "dbo.Organization", "Id");
            AddForeignKey("dbo.MonitoringFinancialSource", "ParrentId", "dbo.MonitoringFinancialSource", "Id");
            DropColumn("dbo.MonitoringFinancialData", "FinanceType");
            DropColumn("dbo.MonitoringFinancialSource", "ForDevelopment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MonitoringFinancialSource", "ForDevelopment", c => c.Boolean(nullable: false));
            AddColumn("dbo.MonitoringFinancialData", "FinanceType", c => c.Int(nullable: false));
            DropForeignKey("dbo.MonitoringFinancialSource", "ParrentId", "dbo.MonitoringFinancialSource");
            DropForeignKey("dbo.MonitoringSmallLeisureInfo", "OrganisationId", "dbo.Organization");
            DropIndex("dbo.MonitoringFinancialSource", new[] { "ParrentId" });
            DropIndex("dbo.MonitoringSmallLeisureInfo", new[] { "OrganisationId" });
            DropColumn("dbo.MonitoringFinancialSource", "ParrentId");
            DropColumn("dbo.MonitoringFinancialSource", "ShowInForm");
            DropColumn("dbo.MonitoringFinancialData", "Formula");
            DropColumn("dbo.SmallLeisureType", "Formula");
            DropColumn("dbo.SmallLeisureType", "IsTextData");
            DropColumn("dbo.MonitoringSmallLeisureInfo", "OrganisationId");
        }
    }
}
