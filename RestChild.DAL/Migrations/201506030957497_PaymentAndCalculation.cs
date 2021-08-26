namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentAndCalculation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TourPrice",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Price = c.Decimal(precision: 32, scale: 4),
                        AgeFrom = c.Int(),
                        AgeTo = c.Int(),
                        TourId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tour", t => t.TourId)
                .Index(t => t.TourId);
            
            CreateTable(
                "dbo.Calculation",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Summa = c.Decimal(precision: 32, scale: 4),
                        DateCalculation = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastPaymentDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateCreate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TypeOfCalculationId = c.Long(),
                        RequestId = c.Long(),
                        HistoryId = c.Long(),
                        AccountId = c.Long(),
                        ChildId = c.Long(),
                        ApplicantId = c.Long(),
                        AddonServicesLinkId = c.Long(),
                        StateId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .ForeignKey("dbo.AddonServicesLink", t => t.AddonServicesLinkId)
                .ForeignKey("dbo.Applicant", t => t.ApplicantId)
                .ForeignKey("dbo.Child", t => t.ChildId)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryId)
                .ForeignKey("dbo.Request", t => t.RequestId)
                .ForeignKey("dbo.StateMachineState", t => t.StateId)
                .ForeignKey("dbo.TypeOfCalculation", t => t.TypeOfCalculationId)
                .Index(t => t.TypeOfCalculationId)
                .Index(t => t.RequestId)
                .Index(t => t.HistoryId)
                .Index(t => t.AccountId)
                .Index(t => t.ChildId)
                .Index(t => t.ApplicantId)
                .Index(t => t.AddonServicesLinkId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PaymentDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        PaymentNumber = c.String(maxLength: 1000),
                        PaymentSumm = c.Decimal(precision: 32, scale: 4),
                        Purpose = c.String(),
                        Description = c.String(),
                        DateCreate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        HistoryId = c.Long(),
                        AccountId = c.Long(),
                        StateId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryId)
                .ForeignKey("dbo.StateMachineState", t => t.StateId)
                .Index(t => t.HistoryId)
                .Index(t => t.AccountId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.TypeOfCalculation",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CalculationPayment",
                c => new
                    {
                        Calculation_Id = c.Long(nullable: false),
                        Payment_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Calculation_Id, t.Payment_Id })
                .ForeignKey("dbo.Calculation", t => t.Calculation_Id, cascadeDelete: true)
                .ForeignKey("dbo.Payment", t => t.Payment_Id, cascadeDelete: true)
                .Index(t => t.Calculation_Id)
                .Index(t => t.Payment_Id);
            
            AddColumn("dbo.Request", "DateIncome", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Request", "DateOutcome", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Calculation", "TypeOfCalculationId", "dbo.TypeOfCalculation");
            DropForeignKey("dbo.Calculation", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.Calculation", "RequestId", "dbo.Request");
            DropForeignKey("dbo.CalculationPayment", "Payment_Id", "dbo.Payment");
            DropForeignKey("dbo.CalculationPayment", "Calculation_Id", "dbo.Calculation");
            DropForeignKey("dbo.Payment", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.Payment", "HistoryId", "dbo.HistoryLink");
            DropForeignKey("dbo.Payment", "AccountId", "dbo.Account");
            DropForeignKey("dbo.Calculation", "HistoryId", "dbo.HistoryLink");
            DropForeignKey("dbo.Calculation", "ChildId", "dbo.Child");
            DropForeignKey("dbo.Calculation", "ApplicantId", "dbo.Applicant");
            DropForeignKey("dbo.Calculation", "AddonServicesLinkId", "dbo.AddonServicesLink");
            DropForeignKey("dbo.Calculation", "AccountId", "dbo.Account");
            DropForeignKey("dbo.TourPrice", "TourId", "dbo.Tour");
            DropIndex("dbo.CalculationPayment", new[] { "Payment_Id" });
            DropIndex("dbo.CalculationPayment", new[] { "Calculation_Id" });
            DropIndex("dbo.Payment", new[] { "StateId" });
            DropIndex("dbo.Payment", new[] { "AccountId" });
            DropIndex("dbo.Payment", new[] { "HistoryId" });
            DropIndex("dbo.Calculation", new[] { "StateId" });
            DropIndex("dbo.Calculation", new[] { "AddonServicesLinkId" });
            DropIndex("dbo.Calculation", new[] { "ApplicantId" });
            DropIndex("dbo.Calculation", new[] { "ChildId" });
            DropIndex("dbo.Calculation", new[] { "AccountId" });
            DropIndex("dbo.Calculation", new[] { "HistoryId" });
            DropIndex("dbo.Calculation", new[] { "RequestId" });
            DropIndex("dbo.Calculation", new[] { "TypeOfCalculationId" });
            DropIndex("dbo.TourPrice", new[] { "TourId" });
            DropColumn("dbo.Request", "DateOutcome");
            DropColumn("dbo.Request", "DateIncome");
            DropTable("dbo.CalculationPayment");
            DropTable("dbo.TypeOfCalculation");
            DropTable("dbo.Payment");
            DropTable("dbo.Calculation");
            DropTable("dbo.TourPrice");
        }
    }
}
