namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ExchangeBaseRegistryManualRequestMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExchangeBaseRegistry", "SearchField", c => c.String(maxLength: 400));
            AddColumn("dbo.ExchangeBaseRegistry", "BirthDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ExchangeBaseRegistry", "IsAddonRequest", c => c.Boolean(nullable: false));
			CreateIndex("dbo.ExchangeBaseRegistry", new [] { "IsAddonRequest", "SearchField", "BirthDate" }, false, "IND_SEARCH_EBR1");
	        CreateIndex("dbo.ExchangeBaseRegistry", new[] {"IsAddonRequest", "BirthDate"}, false, "IND_SEARCH_EBR2");
        }

		public override void Down()
        {
			DropIndex("dbo.ExchangeBaseRegistry", "IND_SEARCH_EBR1");
			DropIndex("dbo.ExchangeBaseRegistry", "IND_SEARCH_EBR2");
			DropColumn("dbo.ExchangeBaseRegistry", "IsAddonRequest");
            DropColumn("dbo.ExchangeBaseRegistry", "BirthDate");
            DropColumn("dbo.ExchangeBaseRegistry", "SearchField");
		}
	}
}
