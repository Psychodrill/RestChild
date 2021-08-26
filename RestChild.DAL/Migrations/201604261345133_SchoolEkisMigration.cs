namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SchoolEkisMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.School", "SourcePk", c => c.Long());
            AddColumn("dbo.School", "Status", c => c.Long());
            AddColumn("dbo.School", "CloseDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.School", "ToOrganizationId", c => c.Long());
            AddColumn("dbo.School", "OrganizationGuid", c => c.Guid());
            AddColumn("dbo.School", "DateChange", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.School", "ExternalId", c => c.Long());
            AddColumn("dbo.TradeUnionCamper", "IsScoolNotPresent", c => c.Boolean(nullable: false));
            AddColumn("dbo.TradeUnionCamper", "LinkToFileId", c => c.Long());
            AddColumn("dbo.TradeUnionCamper", "SelectedSchoolId", c => c.Long());
            AddColumn("dbo.TradeUnionList", "LinkToFileId", c => c.Long());
			CreateIndex("dbo.School", "Status");
			CreateIndex("dbo.TradeUnionCamper", "LinkToFileId");
            CreateIndex("dbo.TradeUnionCamper", "SelectedSchoolId");
            CreateIndex("dbo.TradeUnionList", "LinkToFileId");
            AddForeignKey("dbo.TradeUnionCamper", "LinkToFileId", "dbo.LinkToFile", "Id");
            AddForeignKey("dbo.TradeUnionCamper", "SelectedSchoolId", "dbo.School", "Id");
            AddForeignKey("dbo.TradeUnionList", "LinkToFileId", "dbo.LinkToFile", "Id");
        }

        public override void Down()
        {
	        DropIndex("dbo.School", new[] {"Status"});
			DropForeignKey("dbo.TradeUnionList", "LinkToFileId", "dbo.LinkToFile");
            DropForeignKey("dbo.TradeUnionCamper", "SelectedSchoolId", "dbo.School");
            DropForeignKey("dbo.TradeUnionCamper", "LinkToFileId", "dbo.LinkToFile");
            DropIndex("dbo.TradeUnionList", new[] { "LinkToFileId" });
            DropIndex("dbo.TradeUnionCamper", new[] { "SelectedSchoolId" });
            DropIndex("dbo.TradeUnionCamper", new[] { "LinkToFileId" });
            DropColumn("dbo.TradeUnionList", "LinkToFileId");
            DropColumn("dbo.TradeUnionCamper", "SelectedSchoolId");
            DropColumn("dbo.TradeUnionCamper", "LinkToFileId");
            DropColumn("dbo.TradeUnionCamper", "IsScoolNotPresent");
            DropColumn("dbo.School", "ExternalId");
            DropColumn("dbo.School", "DateChange");
            DropColumn("dbo.School", "OrganizationGuid");
            DropColumn("dbo.School", "ToOrganizationId");
            DropColumn("dbo.School", "CloseDate");
            DropColumn("dbo.School", "Status");
            DropColumn("dbo.School", "SourcePk");
        }
    }
}
