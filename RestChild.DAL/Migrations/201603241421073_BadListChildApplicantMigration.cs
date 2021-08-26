namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BadListChildApplicantMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TypeViolation",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        YearOfRestId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.YearOfRest", t => t.YearOfRestId)
                .Index(t => t.YearOfRestId);
            
            AddColumn("dbo.AddonServices", "ProcentOver", c => c.Decimal(precision: 32, scale: 4));
            AddColumn("dbo.Tour", "ProcentOver", c => c.Decimal(precision: 32, scale: 4));
            AddColumn("dbo.Applicant", "TypeViolationId", c => c.Long());
            AddColumn("dbo.Child", "TypeViolationId", c => c.Long());
            AddColumn("dbo.DeclineReason", "StatusId", c => c.Long());
            AddColumn("dbo.Request", "ProcentOver", c => c.Decimal(precision: 32, scale: 4));
            CreateIndex("dbo.Applicant", "TypeViolationId");
            CreateIndex("dbo.Child", "TypeViolationId");
            CreateIndex("dbo.DeclineReason", "StatusId");
            AddForeignKey("dbo.DeclineReason", "StatusId", "dbo.Status", "Id");
            AddForeignKey("dbo.Child", "TypeViolationId", "dbo.TypeViolation", "Id");
            AddForeignKey("dbo.Applicant", "TypeViolationId", "dbo.TypeViolation", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applicant", "TypeViolationId", "dbo.TypeViolation");
            DropForeignKey("dbo.Child", "TypeViolationId", "dbo.TypeViolation");
            DropForeignKey("dbo.TypeViolation", "YearOfRestId", "dbo.YearOfRest");
            DropForeignKey("dbo.DeclineReason", "StatusId", "dbo.Status");
            DropIndex("dbo.TypeViolation", new[] { "YearOfRestId" });
            DropIndex("dbo.DeclineReason", new[] { "StatusId" });
            DropIndex("dbo.Child", new[] { "TypeViolationId" });
            DropIndex("dbo.Applicant", new[] { "TypeViolationId" });
            DropColumn("dbo.Request", "ProcentOver");
            DropColumn("dbo.DeclineReason", "StatusId");
            DropColumn("dbo.Child", "TypeViolationId");
            DropColumn("dbo.Applicant", "TypeViolationId");
            DropColumn("dbo.Tour", "ProcentOver");
            DropColumn("dbo.AddonServices", "ProcentOver");
            DropTable("dbo.TypeViolation");
        }
    }
}
