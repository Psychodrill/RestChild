namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task115918_3Migration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MonitoringFinanceInformation", newName: "MonitoringFinancialInformation");
            RenameTable(name: "dbo.MonitoringFinantialData", newName: "MonitoringFinancialData");
            AddColumn("dbo.MonitoringFinancialInformation", "YearOfRestId", c => c.Long());
            AddColumn("dbo.MonitoringFinancialData", "Comment", c => c.String());
            AddColumn("dbo.MonitoringFinancialSource", "Code", c => c.String(maxLength: 1000));
            CreateIndex("dbo.MonitoringFinancialInformation", "YearOfRestId");
            AddForeignKey("dbo.MonitoringFinancialInformation", "YearOfRestId", "dbo.YearOfRest", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MonitoringFinancialInformation", "YearOfRestId", "dbo.YearOfRest");
            DropIndex("dbo.MonitoringFinancialInformation", new[] { "YearOfRestId" });
            DropColumn("dbo.MonitoringFinancialSource", "Code");
            DropColumn("dbo.MonitoringFinancialData", "Comment");
            DropColumn("dbo.MonitoringFinancialInformation", "YearOfRestId");
            RenameTable(name: "dbo.MonitoringFinancialData", newName: "MonitoringFinantialData");
            RenameTable(name: "dbo.MonitoringFinancialInformation", newName: "MonitoringFinanceInformation");
        }
    }
}
