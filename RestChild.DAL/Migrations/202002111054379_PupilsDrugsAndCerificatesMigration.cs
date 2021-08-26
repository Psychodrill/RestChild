namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PupilsDrugsAndCerificatesMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pupil", "TypeOfDrugId", "dbo.TypeOfDrug");
            DropIndex("dbo.Pupil", new[] { "TypeOfDrugId" });
            CreateTable(
                "dbo.Certificate",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ContractNumber = c.String(maxLength: 1000),
                        ContractDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        RestDateFrom = c.DateTime(precision: 7, storeType: "datetime2"),
                        RestDateTo = c.DateTime(precision: 7, storeType: "datetime2"),
                        Place = c.String(maxLength: 1000),
                        FullPrice = c.Decimal(precision: 32, scale: 4),
                        PriceForChild = c.Decimal(precision: 32, scale: 4),
                        DatePaidOff = c.DateTime(precision: 7, storeType: "datetime2"),
                        Region = c.String(maxLength: 1000),
                        RequestId = c.Long(),
                        StateMachineStateId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Request", t => t.RequestId)
                .ForeignKey("dbo.StateMachineState", t => t.StateMachineStateId)
                .Index(t => t.RequestId)
                .Index(t => t.StateMachineStateId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.PupilDose",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Dose = c.String(nullable: false, maxLength: 1000),
                        IsDeleted = c.Boolean(nullable: false),
                        PupilId = c.Long(),
                        DrugId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drug", t => t.DrugId)
                .ForeignKey("dbo.Pupil", t => t.PupilId)
                .Index(t => t.PupilId)
                .Index(t => t.DrugId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.Drug",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1000),
                        Storage = c.String(maxLength: 1000),
                        DrugTypeId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeOfDrug", t => t.DrugTypeId)
                .Index(t => t.DrugTypeId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.PupilGroupListMemberDrugDose",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DrugQuantity = c.String(maxLength: 1000),
                        DoseId = c.Long(),
                        GroupPupilId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PupilGroupListMember", t => t.GroupPupilId)
                .ForeignKey("dbo.PupilDose", t => t.DoseId)
                .Index(t => t.DoseId)
                .Index(t => t.GroupPupilId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            DropColumn("dbo.Pupil", "NameOfDrug");
            DropColumn("dbo.Pupil", "StorageOfDrug");
            DropColumn("dbo.Pupil", "QuantityOfDrug");
            DropColumn("dbo.Pupil", "TypeOfDrugId");
            DropColumn("dbo.PupilGroupListMember", "QuantityOfDrug");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PupilGroupListMember", "QuantityOfDrug", c => c.String(maxLength: 1000));
            AddColumn("dbo.Pupil", "TypeOfDrugId", c => c.Long());
            AddColumn("dbo.Pupil", "QuantityOfDrug", c => c.String(maxLength: 1000));
            AddColumn("dbo.Pupil", "StorageOfDrug", c => c.String(maxLength: 1000));
            AddColumn("dbo.Pupil", "NameOfDrug", c => c.String(maxLength: 1000));
            DropForeignKey("dbo.PupilDose", "PupilId", "dbo.Pupil");
            DropForeignKey("dbo.PupilGroupListMemberDrugDose", "DoseId", "dbo.PupilDose");
            DropForeignKey("dbo.PupilGroupListMemberDrugDose", "GroupPupilId", "dbo.PupilGroupListMember");
            DropForeignKey("dbo.Drug", "DrugTypeId", "dbo.TypeOfDrug");
            DropForeignKey("dbo.PupilDose", "DrugId", "dbo.Drug");
            DropForeignKey("dbo.Certificate", "StateMachineStateId", "dbo.StateMachineState");
            DropForeignKey("dbo.Certificate", "RequestId", "dbo.Request");
            DropIndex("dbo.PupilGroupListMemberDrugDose", new[] { "EidSendStatus" });
            DropIndex("dbo.PupilGroupListMemberDrugDose", new[] { "Eid" });
            DropIndex("dbo.PupilGroupListMemberDrugDose", new[] { "GroupPupilId" });
            DropIndex("dbo.PupilGroupListMemberDrugDose", new[] { "DoseId" });
            DropIndex("dbo.Drug", new[] { "EidSendStatus" });
            DropIndex("dbo.Drug", new[] { "Eid" });
            DropIndex("dbo.Drug", new[] { "DrugTypeId" });
            DropIndex("dbo.PupilDose", new[] { "EidSendStatus" });
            DropIndex("dbo.PupilDose", new[] { "Eid" });
            DropIndex("dbo.PupilDose", new[] { "DrugId" });
            DropIndex("dbo.PupilDose", new[] { "PupilId" });
            DropIndex("dbo.Certificate", new[] { "EidSendStatus" });
            DropIndex("dbo.Certificate", new[] { "Eid" });
            DropIndex("dbo.Certificate", new[] { "StateMachineStateId" });
            DropIndex("dbo.Certificate", new[] { "RequestId" });
            DropTable("dbo.PupilGroupListMemberDrugDose");
            DropTable("dbo.Drug");
            DropTable("dbo.PupilDose");
            DropTable("dbo.Certificate");
            CreateIndex("dbo.Pupil", "TypeOfDrugId");
            AddForeignKey("dbo.Pupil", "TypeOfDrugId", "dbo.TypeOfDrug", "Id");
        }
    }
}
