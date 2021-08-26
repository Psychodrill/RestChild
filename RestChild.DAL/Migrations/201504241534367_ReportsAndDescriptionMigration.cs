namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportsAndDescriptionMigration : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ReportRowData", name: "RowDataId", newName: "RowId");
            RenameIndex(table: "dbo.ReportRowData", name: "IX_RowDataId", newName: "IX_RowId");
            AddColumn("dbo.Hotels", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hotels", "Description");
            RenameIndex(table: "dbo.ReportRowData", name: "IX_RowId", newName: "IX_RowDataId");
            RenameColumn(table: "dbo.ReportRowData", name: "RowId", newName: "RowDataId");
        }
    }
}
