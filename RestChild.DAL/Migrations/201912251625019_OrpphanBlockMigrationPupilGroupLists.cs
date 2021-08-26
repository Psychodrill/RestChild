namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrpphanBlockMigrationPupilGroupLists : DbMigration
    {
        public override void Up()
        {
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
            
            CreateTable(
                "dbo.PupilGroupListCollaborator",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TicketTo = c.Boolean(nullable: false),
                        TicketFrom = c.Boolean(nullable: false),
                        OrganisatonCollaboratorId = c.Long(),
                        GroupRequestListId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrganisatorCollaborator", t => t.OrganisatonCollaboratorId)
                .ForeignKey("dbo.PupilGroupList", t => t.GroupRequestListId)
                .Index(t => t.OrganisatonCollaboratorId)
                .Index(t => t.GroupRequestListId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.PupilGroupListMember",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TicketTo = c.Boolean(nullable: false),
                        TicketFrom = c.Boolean(nullable: false),
                        QuantityOfDrug = c.String(maxLength: 1000),
                        GroupRequestListId = c.Long(),
                        PupilId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pupil", t => t.PupilId)
                .ForeignKey("dbo.PupilGroupList", t => t.GroupRequestListId)
                .Index(t => t.GroupRequestListId)
                .Index(t => t.PupilId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.PupilGroupListTransfer",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LargeParkingReAddress = c.String(maxLength: 1000),
                        Note = c.String(maxLength: 1000),
                        CountPeople = c.Int(),
                        BoardingHelp = c.Boolean(nullable: false),
                        GroupRequestListId = c.Long(),
                        AddressId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrphanageAddress", t => t.AddressId)
                .ForeignKey("dbo.PupilGroupList", t => t.GroupRequestListId)
                .Index(t => t.GroupRequestListId)
                .Index(t => t.AddressId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.TypeOfDrug",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            AddColumn("dbo.Pupil", "GlutenFreeFood", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pupil", "PureedFood", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pupil", "FoodAditionals", c => c.String(maxLength: 1000));
            AddColumn("dbo.Pupil", "NameOfDrug", c => c.String(maxLength: 1000));
            AddColumn("dbo.Pupil", "StorageOfDrug", c => c.String(maxLength: 1000));
            AddColumn("dbo.Pupil", "QuantityOfDrug", c => c.String(maxLength: 1000));
            AddColumn("dbo.Pupil", "TypeOfDrugId", c => c.Long());
            CreateIndex("dbo.Pupil", "TypeOfDrugId");
            AddForeignKey("dbo.Pupil", "TypeOfDrugId", "dbo.TypeOfDrug", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pupil", "TypeOfDrugId", "dbo.TypeOfDrug");
            DropForeignKey("dbo.PupilGroupList", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.PupilGroupList", "PupilGroupRequestId", "dbo.RequestForPeriodOfRest");
            DropForeignKey("dbo.PupilGroupList", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.PupilGroupListTransfer", "GroupRequestListId", "dbo.PupilGroupList");
            DropForeignKey("dbo.PupilGroupListTransfer", "AddressId", "dbo.OrphanageAddress");
            DropForeignKey("dbo.PupilGroupListMember", "GroupRequestListId", "dbo.PupilGroupList");
            DropForeignKey("dbo.PupilGroupListMember", "PupilId", "dbo.Pupil");
            DropForeignKey("dbo.PupilGroupListCollaborator", "GroupRequestListId", "dbo.PupilGroupList");
            DropForeignKey("dbo.PupilGroupListCollaborator", "OrganisatonCollaboratorId", "dbo.OrganisatorCollaborator");
            DropIndex("dbo.TypeOfDrug", new[] { "EidSendStatus" });
            DropIndex("dbo.TypeOfDrug", new[] { "Eid" });
            DropIndex("dbo.PupilGroupListTransfer", new[] { "EidSendStatus" });
            DropIndex("dbo.PupilGroupListTransfer", new[] { "Eid" });
            DropIndex("dbo.PupilGroupListTransfer", new[] { "AddressId" });
            DropIndex("dbo.PupilGroupListTransfer", new[] { "GroupRequestListId" });
            DropIndex("dbo.PupilGroupListMember", new[] { "EidSendStatus" });
            DropIndex("dbo.PupilGroupListMember", new[] { "Eid" });
            DropIndex("dbo.PupilGroupListMember", new[] { "PupilId" });
            DropIndex("dbo.PupilGroupListMember", new[] { "GroupRequestListId" });
            DropIndex("dbo.PupilGroupListCollaborator", new[] { "EidSendStatus" });
            DropIndex("dbo.PupilGroupListCollaborator", new[] { "Eid" });
            DropIndex("dbo.PupilGroupListCollaborator", new[] { "GroupRequestListId" });
            DropIndex("dbo.PupilGroupListCollaborator", new[] { "OrganisatonCollaboratorId" });
            DropIndex("dbo.PupilGroupList", new[] { "EidSendStatus" });
            DropIndex("dbo.PupilGroupList", new[] { "Eid" });
            DropIndex("dbo.PupilGroupList", new[] { "HistoryLinkId" });
            DropIndex("dbo.PupilGroupList", new[] { "StateId" });
            DropIndex("dbo.PupilGroupList", new[] { "PupilGroupRequestId" });
            DropIndex("dbo.Pupil", new[] { "TypeOfDrugId" });
            DropColumn("dbo.Pupil", "TypeOfDrugId");
            DropColumn("dbo.Pupil", "QuantityOfDrug");
            DropColumn("dbo.Pupil", "StorageOfDrug");
            DropColumn("dbo.Pupil", "NameOfDrug");
            DropColumn("dbo.Pupil", "FoodAditionals");
            DropColumn("dbo.Pupil", "PureedFood");
            DropColumn("dbo.Pupil", "GlutenFreeFood");
            DropTable("dbo.TypeOfDrug");
            DropTable("dbo.PupilGroupListTransfer");
            DropTable("dbo.PupilGroupListMember");
            DropTable("dbo.PupilGroupListCollaborator");
            DropTable("dbo.PupilGroupList");
        }
    }
}
