namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganizationBankPropertyMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrganizationBank",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Bank = c.String(maxLength: 1000),
                        Bik = c.String(maxLength: 1000),
                        Inn = c.String(maxLength: 1000),
                        Correspondent = c.String(maxLength: 1000),
                        Account = c.String(maxLength: 1000),
                        Comment = c.String(),
                        OrganizationId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.Okved",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Code = c.String(maxLength: 1000),
                        Name = c.String(maxLength: 1000),
                        ParentId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Okved", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.OkvedOrganization",
                c => new
                    {
                        Okved_Id = c.Long(nullable: false),
                        Organization_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Okved_Id, t.Organization_Id })
                .ForeignKey("dbo.Okved", t => t.Okved_Id, cascadeDelete: true)
                .ForeignKey("dbo.Organization", t => t.Organization_Id, cascadeDelete: true)
                .Index(t => t.Okved_Id)
                .Index(t => t.Organization_Id);
            
            AddColumn("dbo.Organization", "Comment", c => c.String());
            AddColumn("dbo.Organization", "Commission", c => c.Decimal(precision: 32, scale: 4));
            AddColumn("dbo.Organization", "CuratorId", c => c.Long());
            AddColumn("dbo.Contract", "Name", c => c.String(maxLength: 1000));
            AddColumn("dbo.Contract", "Commission", c => c.Decimal(precision: 32, scale: 4));
            AddColumn("dbo.Contract", "Comment", c => c.String());
            AddColumn("dbo.Contract", "OrganizationBankId", c => c.Long());
            AddColumn("dbo.Contract", "CuratorId", c => c.Long());
            AddColumn("dbo.TypeOfRest", "ResponsibleText", c => c.String(maxLength: 1000));
            AddColumn("dbo.TypeOfRest", "NeedGeneratePermit", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfRest", "ResponsibleId", c => c.Long());
            CreateIndex("dbo.Organization", "CuratorId");
            CreateIndex("dbo.Contract", "OrganizationBankId");
            CreateIndex("dbo.Contract", "CuratorId");
            CreateIndex("dbo.TypeOfRest", "ResponsibleId");
            AddForeignKey("dbo.Organization", "CuratorId", "dbo.Account", "Id");
            AddForeignKey("dbo.Contract", "CuratorId", "dbo.Account", "Id");
            AddForeignKey("dbo.TypeOfRest", "ResponsibleId", "dbo.Account", "Id");
            AddForeignKey("dbo.Contract", "OrganizationBankId", "dbo.OrganizationBank", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Okved", "ParentId", "dbo.Okved");
            DropForeignKey("dbo.OkvedOrganization", "Organization_Id", "dbo.Organization");
            DropForeignKey("dbo.OkvedOrganization", "Okved_Id", "dbo.Okved");
            DropForeignKey("dbo.Contract", "OrganizationBankId", "dbo.OrganizationBank");
            DropForeignKey("dbo.TypeOfRest", "ResponsibleId", "dbo.Account");
            DropForeignKey("dbo.Contract", "CuratorId", "dbo.Account");
            DropForeignKey("dbo.Organization", "CuratorId", "dbo.Account");
            DropForeignKey("dbo.OrganizationBank", "OrganizationId", "dbo.Organization");
            DropIndex("dbo.OkvedOrganization", new[] { "Organization_Id" });
            DropIndex("dbo.OkvedOrganization", new[] { "Okved_Id" });
            DropIndex("dbo.Okved", new[] { "ParentId" });
            DropIndex("dbo.TypeOfRest", new[] { "ResponsibleId" });
            DropIndex("dbo.Contract", new[] { "CuratorId" });
            DropIndex("dbo.Contract", new[] { "OrganizationBankId" });
            DropIndex("dbo.OrganizationBank", new[] { "OrganizationId" });
            DropIndex("dbo.Organization", new[] { "CuratorId" });
            DropColumn("dbo.TypeOfRest", "ResponsibleId");
            DropColumn("dbo.TypeOfRest", "NeedGeneratePermit");
            DropColumn("dbo.TypeOfRest", "ResponsibleText");
            DropColumn("dbo.Contract", "CuratorId");
            DropColumn("dbo.Contract", "OrganizationBankId");
            DropColumn("dbo.Contract", "Comment");
            DropColumn("dbo.Contract", "Commission");
            DropColumn("dbo.Contract", "Name");
            DropColumn("dbo.Organization", "CuratorId");
            DropColumn("dbo.Organization", "Commission");
            DropColumn("dbo.Organization", "Comment");
            DropTable("dbo.OkvedOrganization");
            DropTable("dbo.Okved");
            DropTable("dbo.OrganizationBank");
        }
    }
}
