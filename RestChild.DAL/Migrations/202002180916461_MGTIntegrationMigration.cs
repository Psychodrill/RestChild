namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MGTIntegrationMigration : DbMigration
    {
        public override void Up()
        {
            Sql(@"Delete From dbo.PupilGroupListCollaborator");
            Sql(@"Delete From dbo.PupilGroupListMemberDrugDose");
            Sql(@"Delete From dbo.PupilGroupListMember");
            Sql(@"Delete From dbo.PupilGroupListTransfer");
            Sql(@"Delete From dbo.PupilGroupList");
            Sql(@"Delete From dbo.RequestForPeriodOfRest");

            AddColumn("dbo.OrganisatorCollaborator", "EntityId", c => c.Long());
            CreateIndex("dbo.OrganisatorCollaborator", "EntityId");
            AddForeignKey("dbo.OrganisatorCollaborator", "EntityId", "dbo.OrganisatorCollaborator", "Id");

            AddColumn("dbo.Pupil", "EntityId", c => c.Long());
            CreateIndex("dbo.Pupil", "EntityId");
            AddForeignKey("dbo.Pupil", "EntityId", "dbo.Pupil", "Id");

            DropForeignKey("dbo.PupilGroupList", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.PupilGroupList", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.PupilGroupList", "PupilGroupRequestId", "dbo.RequestForPeriodOfRest");

            DropForeignKey("dbo.PupilGroupListCollaborator", "GroupRequestListId", "dbo.PupilGroupList");
            DropForeignKey("dbo.PupilGroupListMember", "GroupRequestListId", "dbo.PupilGroupList");
            DropForeignKey("dbo.PupilGroupListTransfer", "GroupRequestListId", "dbo.PupilGroupList");

            DropIndex("dbo.PupilGroupList", new[] { "PupilGroupRequestId" });
            DropIndex("dbo.PupilGroupList", new[] { "StateId" });
            DropIndex("dbo.PupilGroupList", new[] { "HistoryLinkId" });
            DropIndex("dbo.PupilGroupList", new[] { "Eid" });
            DropIndex("dbo.PupilGroupList", new[] { "EidSendStatus" });

            DropTable("dbo.PupilGroupList");

            AddForeignKey("dbo.PupilGroupListCollaborator", "GroupRequestListId", "dbo.ListOfChilds", "Id");
            AddForeignKey("dbo.PupilGroupListMember", "GroupRequestListId", "dbo.ListOfChilds", "Id");
            AddForeignKey("dbo.PupilGroupListTransfer", "GroupRequestListId", "dbo.ListOfChilds", "Id");

            AddColumn("dbo.RequestForPeriodOfRest", "ListsId", c => c.Long());
            CreateIndex("dbo.RequestForPeriodOfRest", "ListsId");
            AddForeignKey("dbo.RequestForPeriodOfRest", "ListsId", "dbo.ListOfChilds", "Id");
        }

        public override void Down()
        {
            Sql(@"Delete From dbo.PupilGroupListCollaborator");
            Sql(@"Delete From dbo.PupilGroupListMemberDrugDose");
            Sql(@"Delete From dbo.PupilGroupListMember");
            Sql(@"Delete From dbo.PupilGroupListTransfer");
            Sql(@"Delete From dbo.RequestForPeriodOfRest");

            DropForeignKey("dbo.OrganisatorCollaborator", "EntityId", "dbo.OrganisatorCollaborator");
            DropIndex("dbo.OrganisatorCollaborator", new[] { "EntityId" });
            DropColumn("dbo.OrganisatorCollaborator", "EntityId");

            DropForeignKey("dbo.Pupil", "EntityId", "dbo.Pupil");
            DropIndex("dbo.Pupil", new[] { "EntityId" });
            DropColumn("dbo.Pupil", "EntityId");

            DropForeignKey("dbo.RequestForPeriodOfRest", "ListsId", "dbo.ListOfChilds");
            DropIndex("dbo.RequestForPeriodOfRest", new[] { "ListsId" });
            DropColumn("dbo.RequestForPeriodOfRest", "ListsId");

            DropForeignKey("dbo.PupilGroupListCollaborator", "GroupRequestListId", "dbo.ListOfChilds");
            DropForeignKey("dbo.PupilGroupListMember", "GroupRequestListId", "dbo.ListOfChilds");
            DropForeignKey("dbo.PupilGroupListTransfer", "GroupRequestListId", "dbo.ListOfChilds");

            CreateTable(
                    "dbo.PupilGroupList",
                    c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PupilGroupRequestId = c.Long(),
                        StateId = c.Long(),
                        HistoryLinkId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .ForeignKey("dbo.RequestForPeriodOfRest", t => t.PupilGroupRequestId)
                .ForeignKey("dbo.StateMachineState", t => t.StateId)
                .Index(t => t.PupilGroupRequestId)
                .Index(t => t.StateId)
                .Index(t => t.HistoryLinkId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);

            AddForeignKey("dbo.PupilGroupListCollaborator", "GroupRequestListId", "dbo.PupilGroupList", "Id");
            AddForeignKey("dbo.PupilGroupListMember", "GroupRequestListId", "dbo.PupilGroupList", "Id");
            AddForeignKey("dbo.PupilGroupListTransfer", "GroupRequestListId", "dbo.PupilGroupList", "Id");
        }
    }
}
