namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ERLDBChangesMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AverageRestPrice",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 32, scale: 4),
                        YearOfRestId = c.Long(),
                        TypeOfRestId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeOfRest", t => t.TypeOfRestId)
                .ForeignKey("dbo.YearOfRest", t => t.YearOfRestId)
                .Index(t => t.YearOfRestId)
                .Index(t => t.TypeOfRestId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.BenefitTypeERL",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                        LCCode = c.String(nullable: false, maxLength: 1000),
                        TypeOfRestId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeOfRest", t => t.TypeOfRestId)
                .Index(t => t.TypeOfRestId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.TypeOfRestERL",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                        UseApplicant = c.Boolean(nullable: false, defaultValue: false),
                        MSPCode = c.String(nullable: false, maxLength: 1000),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.BenefitTypeERLTypeOfRestERL",
                c => new
                    {
                        BenefitTypeERL_Id = c.Long(nullable: false),
                        TypeOfRestERL_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.BenefitTypeERL_Id, t.TypeOfRestERL_Id })
                .ForeignKey("dbo.BenefitTypeERL", t => t.BenefitTypeERL_Id, cascadeDelete: true)
                .ForeignKey("dbo.TypeOfRestERL", t => t.TypeOfRestERL_Id, cascadeDelete: true)
                .Index(t => t.BenefitTypeERL_Id)
                .Index(t => t.TypeOfRestERL_Id);
            
            AddColumn("dbo.BenefitType", "BenefitTypeERLId", c => c.Long());
            AddColumn("dbo.TypeOfRest", "TypeOfRestERLId", c => c.Long());
            AddColumn("dbo.ERLBenefitStatus", "Queue24Sended", c => c.Boolean(nullable: false, defaultValue: false));
            CreateIndex("dbo.TypeOfRest", "TypeOfRestERLId");
            CreateIndex("dbo.BenefitType", "BenefitTypeERLId");
            AddForeignKey("dbo.BenefitType", "BenefitTypeERLId", "dbo.BenefitTypeERL", "Id");
            AddForeignKey("dbo.TypeOfRest", "TypeOfRestERLId", "dbo.TypeOfRestERL", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AverageRestPrice", "YearOfRestId", "dbo.YearOfRest");
            DropForeignKey("dbo.AverageRestPrice", "TypeOfRestId", "dbo.TypeOfRest");
            DropForeignKey("dbo.TypeOfRest", "TypeOfRestERLId", "dbo.TypeOfRestERL");
            DropForeignKey("dbo.BenefitType", "BenefitTypeERLId", "dbo.BenefitTypeERL");
            DropForeignKey("dbo.BenefitTypeERLTypeOfRestERL", "TypeOfRestERL_Id", "dbo.TypeOfRestERL");
            DropForeignKey("dbo.BenefitTypeERLTypeOfRestERL", "BenefitTypeERL_Id", "dbo.BenefitTypeERL");
            DropForeignKey("dbo.BenefitTypeERL", "TypeOfRestId", "dbo.TypeOfRest");
            DropIndex("dbo.BenefitTypeERLTypeOfRestERL", new[] { "TypeOfRestERL_Id" });
            DropIndex("dbo.BenefitTypeERLTypeOfRestERL", new[] { "BenefitTypeERL_Id" });
            DropIndex("dbo.TypeOfRestERL", new[] { "EidSendStatus" });
            DropIndex("dbo.TypeOfRestERL", new[] { "Eid" });
            DropIndex("dbo.BenefitTypeERL", new[] { "EidSendStatus" });
            DropIndex("dbo.BenefitTypeERL", new[] { "Eid" });
            DropIndex("dbo.BenefitTypeERL", new[] { "TypeOfRestId" });
            DropIndex("dbo.BenefitType", new[] { "BenefitTypeERLId" });
            DropIndex("dbo.TypeOfRest", new[] { "TypeOfRestERLId" });
            DropIndex("dbo.AverageRestPrice", new[] { "EidSendStatus" });
            DropIndex("dbo.AverageRestPrice", new[] { "Eid" });
            DropIndex("dbo.AverageRestPrice", new[] { "TypeOfRestId" });
            DropIndex("dbo.AverageRestPrice", new[] { "YearOfRestId" });
            DropColumn("dbo.ERLBenefitStatus", "Queue24Sended");
            DropColumn("dbo.TypeOfRest", "TypeOfRestERLId");
            DropColumn("dbo.BenefitType", "BenefitTypeERLId");
            DropTable("dbo.BenefitTypeERLTypeOfRestERL");
            DropTable("dbo.TypeOfRestERL");
            DropTable("dbo.BenefitTypeERL");
            DropTable("dbo.AverageRestPrice");
        }
    }
}
