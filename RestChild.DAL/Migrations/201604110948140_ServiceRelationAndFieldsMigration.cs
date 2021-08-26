namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServiceRelationAndFieldsMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tour", "PrepaymentMayBe", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tour", "PrepaymentProcent", c => c.Decimal(precision: 32, scale: 4));
            AddColumn("dbo.Request", "CalculationOnPerson", c => c.Boolean(nullable: false));
            AddColumn("dbo.TourAccommodation", "TypePriceCalculationId", c => c.Long());
            AddColumn("dbo.RequestService", "ParentId", c => c.Long());
            CreateIndex("dbo.TourAccommodation", "TypePriceCalculationId");
            CreateIndex("dbo.RequestService", "ParentId");
            AddForeignKey("dbo.TourAccommodation", "TypePriceCalculationId", "dbo.TypePriceCalculation", "Id");
            AddForeignKey("dbo.RequestService", "ParentId", "dbo.RequestService", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequestService", "ParentId", "dbo.RequestService");
            DropForeignKey("dbo.TourAccommodation", "TypePriceCalculationId", "dbo.TypePriceCalculation");
            DropIndex("dbo.RequestService", new[] { "ParentId" });
            DropIndex("dbo.TourAccommodation", new[] { "TypePriceCalculationId" });
            DropColumn("dbo.RequestService", "ParentId");
            DropColumn("dbo.TourAccommodation", "TypePriceCalculationId");
            DropColumn("dbo.Request", "CalculationOnPerson");
            DropColumn("dbo.Tour", "PrepaymentProcent");
            DropColumn("dbo.Tour", "PrepaymentMayBe");
        }
    }
}
