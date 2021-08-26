namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Lok20172Migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StatusStatusAction", "StatusAction_Id", "dbo.StatusAction");
            DropForeignKey("dbo.RequestStatusChainForMpgu", "StatusActionId", "dbo.StatusAction");
            DropPrimaryKey("dbo.StatusAction");
			Sql("truncate table [dbo].[StatusStatusAction]");
			Sql("truncate table dbo.StatusAction");
			DropColumn("dbo.StatusAction", "Id");
			AddColumn("dbo.StatusAction", "Id", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.StatusAction", "Id");
            AddForeignKey("dbo.StatusStatusAction", "StatusAction_Id", "dbo.StatusAction", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RequestStatusChainForMpgu", "StatusActionId", "dbo.StatusAction", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.RequestStatusChainForMpgu", "StatusActionId", "dbo.StatusAction");
            DropForeignKey("dbo.StatusStatusAction", "StatusAction_Id", "dbo.StatusAction");
            DropPrimaryKey("dbo.StatusAction");
			Sql("truncate table [dbo].[StatusStatusAction]");
			Sql("truncate table dbo.StatusAction");
			DropColumn("dbo.StatusAction", "Id");
			AddColumn("dbo.StatusAction", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.StatusAction", "Id");
            AddForeignKey("dbo.RequestStatusChainForMpgu", "StatusActionId", "dbo.StatusAction", "Id");
            AddForeignKey("dbo.StatusStatusAction", "StatusAction_Id", "dbo.StatusAction", "Id", cascadeDelete: true);
        }
    }
}
