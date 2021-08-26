namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContractAddonAgreementMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContractAddonAgreement",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SignNumber = c.String(maxLength: 1000),
                        SignDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateFrom = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateTo = c.DateTime(precision: 7, storeType: "datetime2"),
                        Description = c.String(),
                        ContractId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contract", t => t.ContractId)
                .Index(t => t.ContractId);
            
            AddColumn("dbo.BenefitType", "TypeOfGroupCheckId", c => c.Long());
            AddColumn("dbo.EventGeography", "DurationInDays", c => c.Int());
            CreateIndex("dbo.BenefitType", "TypeOfGroupCheckId");
            AddForeignKey("dbo.BenefitType", "TypeOfGroupCheckId", "dbo.TypeOfGroupCheck", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BenefitType", "TypeOfGroupCheckId", "dbo.TypeOfGroupCheck");
            DropForeignKey("dbo.ContractAddonAgreement", "ContractId", "dbo.Contract");
            DropIndex("dbo.BenefitType", new[] { "TypeOfGroupCheckId" });
            DropIndex("dbo.ContractAddonAgreement", new[] { "ContractId" });
            DropColumn("dbo.EventGeography", "DurationInDays");
            DropColumn("dbo.BenefitType", "TypeOfGroupCheckId");
            DropTable("dbo.ContractAddonAgreement");
        }
    }
}
