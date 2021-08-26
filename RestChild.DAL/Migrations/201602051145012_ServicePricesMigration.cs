namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServicePricesMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddonServicesPrice",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AgeFrom = c.Int(),
                        AgeTo = c.Int(),
                        Price = c.Decimal(nullable: false, precision: 32, scale: 4),
                        PriceInternal = c.Decimal(nullable: false, precision: 32, scale: 4),
                        AddonServicesId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AddonServices", t => t.AddonServicesId)
                .Index(t => t.AddonServicesId);
            
            AddColumn("dbo.RequestFileType", "CodeAsGuf", c => c.String());
            AddColumn("dbo.Applicant", "Inn", c => c.String(maxLength: 1000));
            AddColumn("dbo.Child", "Inn", c => c.String(maxLength: 1000));
            AddColumn("dbo.Request", "ChangeByScan", c => c.Boolean(nullable: false));
            DropColumn("dbo.AddonServices", "Price");
            DropColumn("dbo.AddonServices", "AgeFrom");
            DropColumn("dbo.AddonServices", "AgeTo");
            DropColumn("dbo.AddonServices", "PriceInternal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AddonServices", "PriceInternal", c => c.Decimal(nullable: false, precision: 32, scale: 4));
            AddColumn("dbo.AddonServices", "AgeTo", c => c.Int());
            AddColumn("dbo.AddonServices", "AgeFrom", c => c.Int());
            AddColumn("dbo.AddonServices", "Price", c => c.Decimal(nullable: false, precision: 32, scale: 4));
            DropForeignKey("dbo.AddonServicesPrice", "AddonServicesId", "dbo.AddonServices");
            DropIndex("dbo.AddonServicesPrice", new[] { "AddonServicesId" });
            DropColumn("dbo.Request", "ChangeByScan");
            DropColumn("dbo.Child", "Inn");
            DropColumn("dbo.Applicant", "Inn");
            DropColumn("dbo.RequestFileType", "CodeAsGuf");
            DropTable("dbo.AddonServicesPrice");
        }
    }
}
