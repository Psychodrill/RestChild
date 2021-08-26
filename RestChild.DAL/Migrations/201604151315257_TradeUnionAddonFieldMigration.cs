namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class TradeUnionAddonFieldMigration : DbMigration
    {
        public override void Up()
        {
			RenameColumn("dbo.Organization", "IsСontractor", "IsContractor");
            AddColumn("dbo.RequestAccommodationLink", "Price", c => c.Decimal(precision: 32, scale: 4));
            AddColumn("dbo.RequestAccommodationLink", "PriceInternal", c => c.Decimal(precision: 32, scale: 4));
            AddColumn("dbo.Person", "DocumentTypeId", c => c.Long());
            AddColumn("dbo.TradeUnionCamper", "IsChecked", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Person", "DocumentTypeId");
            AddForeignKey("dbo.Person", "DocumentTypeId", "dbo.DocumentType", "Id");
        }

        public override void Down()
        {
			RenameColumn("dbo.Organization", "IsContractor", "IsСontractor");
			DropForeignKey("dbo.Person", "DocumentTypeId", "dbo.DocumentType");
            DropIndex("dbo.Person", new[] { "DocumentTypeId" });
            DropColumn("dbo.TradeUnionCamper", "IsChecked");
            DropColumn("dbo.Person", "DocumentTypeId");
            DropColumn("dbo.RequestAccommodationLink", "PriceInternal");
            DropColumn("dbo.RequestAccommodationLink", "Price");
        }
    }
}
