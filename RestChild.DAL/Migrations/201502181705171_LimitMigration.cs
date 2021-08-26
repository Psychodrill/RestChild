namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LimitMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccountVedomstvo", "Account_Id", "dbo.Account");
            DropForeignKey("dbo.AccountVedomstvo", "Vedomstvo_Id", "dbo.Vedomstvo");
            DropForeignKey("dbo.Request", "VedomstvoId", "dbo.Vedomstvo");
			DropForeignKey("dbo.Organization", "VedomstvoId", "dbo.Vedomstvo");
			DropIndex("dbo.Organization", new[] { "VedomstvoId" });
            DropIndex("dbo.Request", new[] { "VedomstvoId" });
            DropIndex("dbo.AccountVedomstvo", new[] { "Account_Id" });
            DropIndex("dbo.AccountVedomstvo", new[] { "Vedomstvo_Id" });
			DropColumn("dbo.Organization", "VedomstvoId");
			DropColumn("dbo.Request", "VedomstvoId");
			CreateTable(
                "dbo.LimitOnOrganization",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Volume = c.Int(nullable: false),
                        Approved = c.Boolean(nullable: false),
                        LimitOnVedomstvoId = c.Long(),
                        OrganizationId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LimitOnVedomstvo", t => t.LimitOnVedomstvoId)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .Index(t => t.LimitOnVedomstvoId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.LimitOnVedomstvo",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LimitYear = c.Int(nullable: false),
                        Approved = c.Boolean(nullable: false),
                        Volume = c.Int(nullable: false),
                        OrganizationId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.OrganizationId);
            
            AddColumn("dbo.Organization", "IsVedomstvo", c => c.Boolean(nullable: false));
            AddColumn("dbo.Organization", "ParentId", c => c.Long());
            AddColumn("dbo.Child", "LimitOnOrganizationId", c => c.Long());
            CreateIndex("dbo.Organization", "ParentId");
            CreateIndex("dbo.Child", "LimitOnOrganizationId");
            AddForeignKey("dbo.Organization", "ParentId", "dbo.Organization", "Id");
            AddForeignKey("dbo.Child", "LimitOnOrganizationId", "dbo.LimitOnOrganization", "Id");
            DropTable("dbo.Vedomstvo");
            DropTable("dbo.AccountVedomstvo");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AccountVedomstvo",
                c => new
                    {
                        AccountId = c.Long(nullable: false),
                        VedomstvoId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.AccountId, t.VedomstvoId });
            
            CreateTable(
                "dbo.Vedomstvo",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        ShortName = c.String(maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Request", "VedomstvoId", c => c.Long());
            AddColumn("dbo.Organization", "VedomstvoId", c => c.Long());
            DropForeignKey("dbo.Child", "LimitOnOrganizationId", "dbo.LimitOnOrganization");
            DropForeignKey("dbo.Organization", "ParentId", "dbo.Organization");
            DropForeignKey("dbo.LimitOnOrganization", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.LimitOnOrganization", "LimitOnVedomstvoId", "dbo.LimitOnVedomstvo");
            DropIndex("dbo.Child", new[] { "LimitOnOrganizationId" });
            DropIndex("dbo.LimitOnVedomstvo", new[] { "OrganizationId" });
            DropIndex("dbo.LimitOnOrganization", new[] { "OrganizationId" });
            DropIndex("dbo.LimitOnOrganization", new[] { "LimitOnVedomstvoId" });
            DropIndex("dbo.Organization", new[] { "ParentId" });
            DropColumn("dbo.Child", "LimitOnOrganizationId");
            DropColumn("dbo.Organization", "ParentId");
            DropColumn("dbo.Organization", "IsVedomstvo");
            DropTable("dbo.LimitOnVedomstvo");
            DropTable("dbo.LimitOnOrganization");
            RenameColumn(table: "dbo.LimitOnVedomstvo", name: "OrganizationId", newName: "VedomstvoId");
            CreateIndex("dbo.AccountVedomstvo", "Vedomstvo_Id");
            CreateIndex("dbo.AccountVedomstvo", "Account_Id");
            CreateIndex("dbo.Request", "VedomstvoId");
            CreateIndex("dbo.Organization", "VedomstvoId");
            AddForeignKey("dbo.Request", "VedomstvoId", "dbo.Vedomstvo", "Id");
            AddForeignKey("dbo.AccountVedomstvo", "VedomstvoId", "dbo.Vedomstvo", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AccountVedomstvo", "AccountId", "dbo.Account", "Id", cascadeDelete: true);
        }
    }
}
