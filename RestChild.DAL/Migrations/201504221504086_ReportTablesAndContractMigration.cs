namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportTablesAndContractMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReportRowData",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        Style = c.String(maxLength: 1000),
                        CssClass = c.String(maxLength: 1000),
                        Value = c.String(maxLength: 1000),
                        Url = c.String(maxLength: 1000),
                        RowDataId = c.Long(),
                        ReportTableHeadId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ReportTableHead", t => t.ReportTableHeadId)
                .ForeignKey("dbo.ReportTableRow", t => t.RowDataId)
                .Index(t => t.RowDataId)
                .Index(t => t.ReportTableHeadId);
            
            CreateTable(
                "dbo.ReportTableHead",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        CssClass = c.String(maxLength: 1000),
                        SortOrder = c.Int(nullable: false),
                        Style = c.String(maxLength: 1000),
                        ReportTableId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ReportTable", t => t.ReportTableId)
                .Index(t => t.ReportTableId);
            
            CreateTable(
                "dbo.ReportTable",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 1000),
                        CssClass = c.String(maxLength: 1000),
                        SortOrder = c.Long(nullable: false),
                        ReportSheetId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ReportSheet", t => t.ReportSheetId)
                .Index(t => t.ReportSheetId);
            
            CreateTable(
                "dbo.ReportSheet",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        CodeAccess = c.Guid(nullable: false),
                        ReportName = c.String(maxLength: 1000),
                        SortOrder = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReportTableRow",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 1000),
                        CssClass = c.String(maxLength: 1000),
                        Style = c.String(maxLength: 1000),
                        SortOrder = c.Int(nullable: false),
                        TableId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ReportTable", t => t.TableId)
                .Index(t => t.TableId);
            
            AddColumn("dbo.Tour", "CorpusNumber", c => c.String(maxLength: 1000));
            AddColumn("dbo.Contract", "PlanCount", c => c.Int());
            AddColumn("dbo.Contract", "SupplierId", c => c.Long());
            CreateIndex("dbo.Contract", "SupplierId");
            AddForeignKey("dbo.Contract", "SupplierId", "dbo.Organization", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReportRowData", "RowDataId", "dbo.ReportTableRow");
            DropForeignKey("dbo.ReportRowData", "ReportTableHeadId", "dbo.ReportTableHead");
            DropForeignKey("dbo.ReportTableRow", "TableId", "dbo.ReportTable");
            DropForeignKey("dbo.ReportTableHead", "ReportTableId", "dbo.ReportTable");
            DropForeignKey("dbo.ReportTable", "ReportSheetId", "dbo.ReportSheet");
            DropForeignKey("dbo.Contract", "SupplierId", "dbo.Organization");
            DropIndex("dbo.ReportTableRow", new[] { "TableId" });
            DropIndex("dbo.ReportTable", new[] { "ReportSheetId" });
            DropIndex("dbo.ReportTableHead", new[] { "ReportTableId" });
            DropIndex("dbo.ReportRowData", new[] { "ReportTableHeadId" });
            DropIndex("dbo.ReportRowData", new[] { "RowDataId" });
            DropIndex("dbo.Contract", new[] { "SupplierId" });
            DropColumn("dbo.Contract", "SupplierId");
            DropColumn("dbo.Contract", "PlanCount");
            DropColumn("dbo.Tour", "CorpusNumber");
            DropTable("dbo.ReportTableRow");
            DropTable("dbo.ReportSheet");
            DropTable("dbo.ReportTable");
            DropTable("dbo.ReportTableHead");
            DropTable("dbo.ReportRowData");
        }
    }
}
