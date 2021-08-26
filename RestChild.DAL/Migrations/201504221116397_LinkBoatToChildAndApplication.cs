namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkBoatToChildAndApplication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bout", "DateIncome", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Bout", "DateOutcome", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Applicant", "BoutId", c => c.Long());
            AddColumn("dbo.Child", "BoutId", c => c.Long());
            CreateIndex("dbo.Applicant", "BoutId");
            CreateIndex("dbo.Child", "BoutId");
            AddForeignKey("dbo.Applicant", "BoutId", "dbo.Bout", "Id");
            AddForeignKey("dbo.Child", "BoutId", "dbo.Bout", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Child", "BoutId", "dbo.Bout");
            DropForeignKey("dbo.Applicant", "BoutId", "dbo.Bout");
            DropIndex("dbo.Child", new[] { "BoutId" });
            DropIndex("dbo.Applicant", new[] { "BoutId" });
            DropColumn("dbo.Child", "BoutId");
            DropColumn("dbo.Applicant", "BoutId");
            DropColumn("dbo.Bout", "DateOutcome");
            DropColumn("dbo.Bout", "DateIncome");
        }
    }
}
