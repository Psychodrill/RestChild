namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lok2019Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RepresentInterest",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 1000),
                        IsDeleted = c.Boolean(nullable: false),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.TypeOfTransfer",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 1000),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.TypeOfSubRestriction",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 1000),
                        IsDeleted = c.Boolean(nullable: false),
                        TypeOfRestrictionId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeOfRestriction", t => t.TypeOfRestrictionId)
                .Index(t => t.TypeOfRestrictionId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            AddColumn("dbo.Child", "TypeOfSubRestrictionId", c => c.Long());
            AddColumn("dbo.Request", "RepresentInterestId", c => c.Long());
            AddColumn("dbo.Request", "TransferToId", c => c.Long());
            AddColumn("dbo.Request", "TransferFromId", c => c.Long());
            AddColumn("dbo.ApplicantType", "IsDeleted", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Child", "TypeOfSubRestrictionId");
            CreateIndex("dbo.Request", "RepresentInterestId");
            CreateIndex("dbo.Request", "TransferToId");
            CreateIndex("dbo.Request", "TransferFromId");
            AddForeignKey("dbo.Request", "RepresentInterestId", "dbo.RepresentInterest", "Id");
            AddForeignKey("dbo.Request", "TransferFromId", "dbo.TypeOfTransfer", "Id");
            AddForeignKey("dbo.Request", "TransferToId", "dbo.TypeOfTransfer", "Id");
            AddForeignKey("dbo.Child", "TypeOfSubRestrictionId", "dbo.TypeOfSubRestriction", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Child", "TypeOfSubRestrictionId", "dbo.TypeOfSubRestriction");
            DropForeignKey("dbo.TypeOfSubRestriction", "TypeOfRestrictionId", "dbo.TypeOfRestriction");
            DropForeignKey("dbo.Request", "TransferToId", "dbo.TypeOfTransfer");
            DropForeignKey("dbo.Request", "TransferFromId", "dbo.TypeOfTransfer");
            DropForeignKey("dbo.Request", "RepresentInterestId", "dbo.RepresentInterest");
            DropIndex("dbo.TypeOfSubRestriction", new[] { "EidSendStatus" });
            DropIndex("dbo.TypeOfSubRestriction", new[] { "Eid" });
            DropIndex("dbo.TypeOfSubRestriction", new[] { "TypeOfRestrictionId" });
            DropIndex("dbo.TypeOfTransfer", new[] { "EidSendStatus" });
            DropIndex("dbo.TypeOfTransfer", new[] { "Eid" });
            DropIndex("dbo.RepresentInterest", new[] { "EidSendStatus" });
            DropIndex("dbo.RepresentInterest", new[] { "Eid" });
            DropIndex("dbo.Request", new[] { "TransferFromId" });
            DropIndex("dbo.Request", new[] { "TransferToId" });
            DropIndex("dbo.Request", new[] { "RepresentInterestId" });
            DropIndex("dbo.Child", new[] { "TypeOfSubRestrictionId" });
            DropColumn("dbo.ApplicantType", "IsDeleted");
            DropColumn("dbo.Request", "TransferFromId");
            DropColumn("dbo.Request", "TransferToId");
            DropColumn("dbo.Request", "RepresentInterestId");
            DropColumn("dbo.Child", "TypeOfSubRestrictionId");
            DropTable("dbo.TypeOfSubRestriction");
            DropTable("dbo.TypeOfTransfer");
            DropTable("dbo.RepresentInterest");
        }
    }
}
