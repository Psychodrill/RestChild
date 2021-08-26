namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotComeInPlaceOfRestMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdministratorTour", "LinkedAccountId", c => c.Long());
            AddColumn("dbo.Applicant", "NotComeInPlaceOfRest", c => c.Boolean(nullable: false));
            AddColumn("dbo.Child", "NotComeInPlaceOfRest", c => c.Boolean(nullable: false));
            AddColumn("dbo.LinkToPeople", "NotComeInPlaceOfRest", c => c.Boolean(nullable: false));
            CreateIndex("dbo.AdministratorTour", "LinkedAccountId");
            AddForeignKey("dbo.AdministratorTour", "LinkedAccountId", "dbo.Account", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdministratorTour", "LinkedAccountId", "dbo.Account");
            DropIndex("dbo.AdministratorTour", new[] { "LinkedAccountId" });
            DropColumn("dbo.LinkToPeople", "NotComeInPlaceOfRest");
            DropColumn("dbo.Child", "NotComeInPlaceOfRest");
            DropColumn("dbo.Applicant", "NotComeInPlaceOfRest");
            DropColumn("dbo.AdministratorTour", "LinkedAccountId");
        }
    }
}
