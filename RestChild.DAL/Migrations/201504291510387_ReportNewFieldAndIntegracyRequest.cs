namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportNewFieldAndIntegracyRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InteragencyRequest", "BtiDistrictId", c => c.Long());
            AddColumn("dbo.InteragencyRequest", "BtiRegionId", c => c.Long());
            AddColumn("dbo.ReportTableHead", "RowSpan", c => c.Int());
            AddColumn("dbo.ReportTableHead", "ColSpan", c => c.Int());
            AddColumn("dbo.ReportTableHead", "RowIndex", c => c.Int());
            CreateIndex("dbo.InteragencyRequest", "BtiDistrictId");
            CreateIndex("dbo.InteragencyRequest", "BtiRegionId");
            AddForeignKey("dbo.InteragencyRequest", "BtiDistrictId", "dbo.BtiDistrict", "Id");
            AddForeignKey("dbo.InteragencyRequest", "BtiRegionId", "dbo.BtiRegion", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InteragencyRequest", "BtiRegionId", "dbo.BtiRegion");
            DropForeignKey("dbo.InteragencyRequest", "BtiDistrictId", "dbo.BtiDistrict");
            DropIndex("dbo.InteragencyRequest", new[] { "BtiRegionId" });
            DropIndex("dbo.InteragencyRequest", new[] { "BtiDistrictId" });
            DropColumn("dbo.ReportTableHead", "RowIndex");
            DropColumn("dbo.ReportTableHead", "ColSpan");
            DropColumn("dbo.ReportTableHead", "RowSpan");
            DropColumn("dbo.InteragencyRequest", "BtiRegionId");
            DropColumn("dbo.InteragencyRequest", "BtiDistrictId");
        }
    }
}
