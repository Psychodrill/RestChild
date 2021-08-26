using System.Data.Entity.Migrations.Model;

namespace RestChild.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class VocSinglePk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccessRightAccount", "AccessRight_Id", "dbo.AccessRight");
            DropForeignKey("dbo.AccessRightRole", "AccessRight_Id", "dbo.AccessRight");
            DropForeignKey("dbo.AnalyticsViewRow", "AnalyticsViewRowTypeId", "dbo.AnalyticsViewRowType");
            DropForeignKey("dbo.ExchangeBaseRegistry", "ExchangeBaseRegistryTypeId", "dbo.ExchangeBaseRegistryType");
            DropForeignKey("dbo.Child", "BenefitApproveTypeId", "dbo.BenefitApproveType");
            DropForeignKey("dbo.Request", "AttendantTypeId", "dbo.AttendantType");
            DropForeignKey("dbo.Request", "DeclineReasonId", "dbo.DeclineReason");
            DropForeignKey("dbo.TypeOfRest", "ParentId", "dbo.TypeOfRest");
            DropForeignKey("dbo.TimeOfRest", "TypeOfRestId", "dbo.TypeOfRest");
            DropForeignKey("dbo.Request", "TypeOfRestId", "dbo.TypeOfRest");
            DropForeignKey("dbo.TypeOfRestBenefitRestriction", "TypeOfRestId", "dbo.TypeOfRest");
            DropForeignKey("dbo.Child", "StatusByChildId", "dbo.StatusByChild");
            DropPrimaryKey("dbo.AccessRight");
            DropPrimaryKey("dbo.AnalyticsViewRowType");
            DropPrimaryKey("dbo.ExchangeBaseRegistryType");
            DropPrimaryKey("dbo.BenefitApproveType");
            DropPrimaryKey("dbo.AttendantType");
            DropPrimaryKey("dbo.DeclineReason");
            DropPrimaryKey("dbo.TypeOfRest");
            DropPrimaryKey("dbo.StatusByChild");


			RenameColumn("dbo.AccessRight", "Id", "Id2");
			RenameColumn("dbo.AnalyticsViewRowType", "Id", "Id2");
			RenameColumn("dbo.ExchangeBaseRegistryType", "Id", "Id2");
			RenameColumn("dbo.BenefitApproveType", "Id", "Id2");
			RenameColumn("dbo.AttendantType", "Id", "Id2");
			RenameColumn("dbo.DeclineReason", "Id", "Id2");
			RenameColumn("dbo.TypeOfRest", "Id", "Id2");
			RenameColumn("dbo.StatusByChild", "Id", "Id2");

			AddColumn("dbo.StatusByChild", "Id", c => c.Long(nullable: false));
			AddColumn("dbo.TypeOfRest", "Id", c => c.Long(nullable: false));
			AddColumn("dbo.DeclineReason", "Id", c => c.Long(nullable: false));
			AddColumn("dbo.AttendantType", "Id", c => c.Long(nullable: false));
			AddColumn("dbo.BenefitApproveType", "Id", c => c.Long(nullable: false));
			AddColumn("dbo.ExchangeBaseRegistryType", "Id", c => c.Long(nullable: false));
			AddColumn("dbo.AnalyticsViewRowType", "Id", c => c.Long(nullable: false));
			AddColumn("dbo.AccessRight", "Id", c => c.Long(nullable: false));

			Sql("update dbo.StatusByChild set Id=Id2");
			Sql("update dbo.TypeOfRest set Id=Id2");
			Sql("update dbo.DeclineReason set Id=Id2");
			Sql("update dbo.AttendantType set Id=Id2");
			Sql("update dbo.BenefitApproveType set Id=Id2");
			Sql("update dbo.ExchangeBaseRegistryType set Id=Id2");
			Sql("update dbo.AnalyticsViewRowType set Id=Id2");
			Sql("update dbo.AccessRight set Id=Id2");

			DropColumn("dbo.AccessRight", "Id2");
			DropColumn("dbo.AnalyticsViewRowType", "Id2");
			DropColumn("dbo.ExchangeBaseRegistryType", "Id2");
			DropColumn("dbo.BenefitApproveType", "Id2");
			DropColumn("dbo.AttendantType", "Id2");
			DropColumn("dbo.DeclineReason", "Id2");
			DropColumn("dbo.TypeOfRest", "Id2");
			DropColumn("dbo.StatusByChild", "Id2");

			AddPrimaryKey("dbo.AccessRight", "Id");
            AddPrimaryKey("dbo.AnalyticsViewRowType", "Id");
            AddPrimaryKey("dbo.ExchangeBaseRegistryType", "Id");
            AddPrimaryKey("dbo.BenefitApproveType", "Id");
            AddPrimaryKey("dbo.AttendantType", "Id");
            AddPrimaryKey("dbo.DeclineReason", "Id");
            AddPrimaryKey("dbo.TypeOfRest", "Id");
            AddPrimaryKey("dbo.StatusByChild", "Id");
            AddForeignKey("dbo.AccessRightAccount", "AccessRight_Id", "dbo.AccessRight", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AccessRightRole", "AccessRight_Id", "dbo.AccessRight", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AnalyticsViewRow", "AnalyticsViewRowTypeId", "dbo.AnalyticsViewRowType", "Id");
            AddForeignKey("dbo.ExchangeBaseRegistry", "ExchangeBaseRegistryTypeId", "dbo.ExchangeBaseRegistryType", "Id");
            AddForeignKey("dbo.Child", "BenefitApproveTypeId", "dbo.BenefitApproveType", "Id");
            AddForeignKey("dbo.Request", "AttendantTypeId", "dbo.AttendantType", "Id");
            AddForeignKey("dbo.Request", "DeclineReasonId", "dbo.DeclineReason", "Id");
            AddForeignKey("dbo.TypeOfRest", "ParentId", "dbo.TypeOfRest", "Id");
            AddForeignKey("dbo.TimeOfRest", "TypeOfRestId", "dbo.TypeOfRest", "Id");
            AddForeignKey("dbo.Request", "TypeOfRestId", "dbo.TypeOfRest", "Id");
            AddForeignKey("dbo.TypeOfRestBenefitRestriction", "TypeOfRestId", "dbo.TypeOfRest", "Id");
            AddForeignKey("dbo.Child", "StatusByChildId", "dbo.StatusByChild", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Child", "StatusByChildId", "dbo.StatusByChild");
            DropForeignKey("dbo.TypeOfRestBenefitRestriction", "TypeOfRestId", "dbo.TypeOfRest");
            DropForeignKey("dbo.Request", "TypeOfRestId", "dbo.TypeOfRest");
            DropForeignKey("dbo.TimeOfRest", "TypeOfRestId", "dbo.TypeOfRest");
            DropForeignKey("dbo.TypeOfRest", "ParentId", "dbo.TypeOfRest");
            DropForeignKey("dbo.Request", "DeclineReasonId", "dbo.DeclineReason");
            DropForeignKey("dbo.Request", "AttendantTypeId", "dbo.AttendantType");
            DropForeignKey("dbo.Child", "BenefitApproveTypeId", "dbo.BenefitApproveType");
            DropForeignKey("dbo.ExchangeBaseRegistry", "ExchangeBaseRegistryTypeId", "dbo.ExchangeBaseRegistryType");
            DropForeignKey("dbo.AnalyticsViewRow", "AnalyticsViewRowTypeId", "dbo.AnalyticsViewRowType");
            DropForeignKey("dbo.AccessRightRole", "AccessRight_Id", "dbo.AccessRight");
            DropForeignKey("dbo.AccessRightAccount", "AccessRight_Id", "dbo.AccessRight");
            DropPrimaryKey("dbo.StatusByChild");
            DropPrimaryKey("dbo.TypeOfRest");
            DropPrimaryKey("dbo.DeclineReason");
            DropPrimaryKey("dbo.AttendantType");
            DropPrimaryKey("dbo.BenefitApproveType");
            DropPrimaryKey("dbo.ExchangeBaseRegistryType");
            DropPrimaryKey("dbo.AnalyticsViewRowType");
            DropPrimaryKey("dbo.AccessRight");
            AlterColumn("dbo.StatusByChild", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.TypeOfRest", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.DeclineReason", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.AttendantType", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.BenefitApproveType", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.ExchangeBaseRegistryType", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.AnalyticsViewRowType", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.AccessRight", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.StatusByChild", "Id");
            AddPrimaryKey("dbo.TypeOfRest", "Id");
            AddPrimaryKey("dbo.DeclineReason", "Id");
            AddPrimaryKey("dbo.AttendantType", "Id");
            AddPrimaryKey("dbo.BenefitApproveType", "Id");
            AddPrimaryKey("dbo.ExchangeBaseRegistryType", "Id");
            AddPrimaryKey("dbo.AnalyticsViewRowType", "Id");
            AddPrimaryKey("dbo.AccessRight", "Id");
            AddForeignKey("dbo.Child", "StatusByChildId", "dbo.StatusByChild", "Id");
            AddForeignKey("dbo.TypeOfRestBenefitRestriction", "TypeOfRestId", "dbo.TypeOfRest", "Id");
            AddForeignKey("dbo.Request", "TypeOfRestId", "dbo.TypeOfRest", "Id");
            AddForeignKey("dbo.TimeOfRest", "TypeOfRestId", "dbo.TypeOfRest", "Id");
            AddForeignKey("dbo.TypeOfRest", "ParentId", "dbo.TypeOfRest", "Id");
            AddForeignKey("dbo.Request", "DeclineReasonId", "dbo.DeclineReason", "Id");
            AddForeignKey("dbo.Request", "AttendantTypeId", "dbo.AttendantType", "Id");
            AddForeignKey("dbo.Child", "BenefitApproveTypeId", "dbo.BenefitApproveType", "Id");
            AddForeignKey("dbo.ExchangeBaseRegistry", "ExchangeBaseRegistryTypeId", "dbo.ExchangeBaseRegistryType", "Id");
            AddForeignKey("dbo.AnalyticsViewRow", "AnalyticsViewRowTypeId", "dbo.AnalyticsViewRowType", "Id");
            AddForeignKey("dbo.AccessRightRole", "AccessRight_Id", "dbo.AccessRight", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AccessRightAccount", "AccessRight_Id", "dbo.AccessRight", "Id", cascadeDelete: true);
        }
    }
}
