namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportCascadeDeleteMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReportRowData", "RowDataId", "dbo.ReportTableRow");
            DropForeignKey("dbo.ReportTableHead", "ReportTableId", "dbo.ReportTable");
            DropForeignKey("dbo.ReportTable", "ReportSheetId", "dbo.ReportSheet");
            DropForeignKey("dbo.ReportTableRow", "TableId", "dbo.ReportTable");
            DropIndex("dbo.ReportRowData", new[] { "RowDataId" });
            DropIndex("dbo.ReportTableHead", new[] { "ReportTableId" });
            DropIndex("dbo.ReportTable", new[] { "ReportSheetId" });
            DropIndex("dbo.ReportTableRow", new[] { "TableId" });
            AlterColumn("dbo.ReportRowData", "RowDataId", c => c.Long(nullable: false));
            AlterColumn("dbo.ReportTableHead", "ReportTableId", c => c.Long(nullable: false));
            AlterColumn("dbo.ReportTable", "ReportSheetId", c => c.Long(nullable: false));
            AlterColumn("dbo.ReportTableRow", "TableId", c => c.Long(nullable: false));
            CreateIndex("dbo.ReportRowData", "RowDataId");
            CreateIndex("dbo.ReportTableHead", "ReportTableId");
            CreateIndex("dbo.ReportTable", "ReportSheetId");
            CreateIndex("dbo.ReportTableRow", "TableId");
            AddForeignKey("dbo.ReportRowData", "RowDataId", "dbo.ReportTableRow", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReportTableHead", "ReportTableId", "dbo.ReportTable", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReportTable", "ReportSheetId", "dbo.ReportSheet", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReportTableRow", "TableId", "dbo.ReportTable", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReportTableRow", "TableId", "dbo.ReportTable");
            DropForeignKey("dbo.ReportTable", "ReportSheetId", "dbo.ReportSheet");
            DropForeignKey("dbo.ReportTableHead", "ReportTableId", "dbo.ReportTable");
            DropForeignKey("dbo.ReportRowData", "RowDataId", "dbo.ReportTableRow");
            DropIndex("dbo.ReportTableRow", new[] { "TableId" });
            DropIndex("dbo.ReportTable", new[] { "ReportSheetId" });
            DropIndex("dbo.ReportTableHead", new[] { "ReportTableId" });
            DropIndex("dbo.ReportRowData", new[] { "RowDataId" });
            AlterColumn("dbo.ReportTableRow", "TableId", c => c.Long());
            AlterColumn("dbo.ReportTable", "ReportSheetId", c => c.Long());
            AlterColumn("dbo.ReportTableHead", "ReportTableId", c => c.Long());
            AlterColumn("dbo.ReportRowData", "RowDataId", c => c.Long());
            CreateIndex("dbo.ReportTableRow", "TableId");
            CreateIndex("dbo.ReportTable", "ReportSheetId");
            CreateIndex("dbo.ReportTableHead", "ReportTableId");
            CreateIndex("dbo.ReportRowData", "RowDataId");
            AddForeignKey("dbo.ReportTableRow", "TableId", "dbo.ReportTable", "Id");
            AddForeignKey("dbo.ReportTable", "ReportSheetId", "dbo.ReportSheet", "Id");
            AddForeignKey("dbo.ReportTableHead", "ReportTableId", "dbo.ReportTable", "Id");
            AddForeignKey("dbo.ReportRowData", "RowDataId", "dbo.ReportTableRow", "Id");
        }
    }
}
