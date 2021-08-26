namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class TradeUnionAddonFieldsMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "ContingentUid", c => c.String(maxLength: 200));
            AddColumn("dbo.Person", "IsChild", c => c.Boolean(nullable: false));
            AddColumn("dbo.Person", "KeyField", c => c.String(maxLength: 300));
            AddColumn("dbo.Person", "AddressId", c => c.Long());
            AddColumn("dbo.TradeUnionCamper", "TradeUnionOrganizationOther", c => c.String(maxLength: 1000));
            CreateIndex("dbo.Person", "AddressId");
			CreateIndex("dbo.Person", "ContingentUid");
	        CreateIndex("dbo.Person", new[] { "IsChild", "KeyField" });
			AddForeignKey("dbo.Person", "AddressId", "dbo.Address", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Person", "AddressId", "dbo.Address");
            DropIndex("dbo.Person", new[] { "AddressId" });
	        DropIndex("dbo.Person", new[] {"ContingentUid"});
			DropIndex("dbo.Person", new[] { "IsChild", "KeyField" });

			DropColumn("dbo.TradeUnionCamper", "TradeUnionOrganizationOther");
            DropColumn("dbo.Person", "AddressId");
            DropColumn("dbo.Person", "KeyField");
            DropColumn("dbo.Person", "IsChild");
            DropColumn("dbo.Person", "ContingentUid");
        }
    }
}
