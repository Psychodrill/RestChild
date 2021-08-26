namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountRightAndOrganization : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccountOrganization", "Account_Id", "dbo.Account");
            DropForeignKey("dbo.AccountOrganization", "Organization_Id", "dbo.Organization");
            DropForeignKey("dbo.AccountRole", "Account_Id", "dbo.Account");
            DropForeignKey("dbo.AccountRole", "Role_Id", "dbo.Role");
            DropForeignKey("dbo.AccessRightAccount", "AccessRight_Id", "dbo.AccessRight");
            DropForeignKey("dbo.AccessRightAccount", "Account_Id", "dbo.Account");
            DropIndex("dbo.AccountOrganization", new[] { "Account_Id" });
            DropIndex("dbo.AccountOrganization", new[] { "Organization_Id" });
            DropIndex("dbo.AccountRole", new[] { "Account_Id" });
            DropIndex("dbo.AccountRole", new[] { "Role_Id" });
            DropIndex("dbo.AccessRightAccount", new[] { "AccessRight_Id" });
            DropIndex("dbo.AccessRightAccount", new[] { "Account_Id" });
            CreateTable(
                "dbo.AccountRights",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AccountId = c.Long(),
                        AccessRightId = c.Long(),
                        OrganizationId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .ForeignKey("dbo.AccessRight", t => t.AccessRightId)
                .Index(t => t.AccountId)
                .Index(t => t.AccessRightId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.AccountRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OrganizationId = c.Long(),
                        AccountId = c.Long(),
                        RoleId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .ForeignKey("dbo.Role", t => t.RoleId)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .Index(t => t.OrganizationId)
                .Index(t => t.AccountId)
                .Index(t => t.RoleId);
            
            DropTable("dbo.AccountOrganization");
            DropTable("dbo.AccountRole");
            DropTable("dbo.AccessRightAccount");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AccessRightAccount",
                c => new
                    {
                        AccessRight_Id = c.Long(nullable: false),
                        Account_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.AccessRight_Id, t.Account_Id });
            
            CreateTable(
                "dbo.AccountRole",
                c => new
                    {
                        Account_Id = c.Long(nullable: false),
                        Role_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Account_Id, t.Role_Id });
            
            CreateTable(
                "dbo.AccountOrganization",
                c => new
                    {
                        Account_Id = c.Long(nullable: false),
                        Organization_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Account_Id, t.Organization_Id });
            
            DropForeignKey("dbo.AccountRights", "AccessRightId", "dbo.AccessRight");
            DropForeignKey("dbo.AccountRights", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.AccountRoles", "AccountId", "dbo.Account");
            DropForeignKey("dbo.AccountRoles", "RoleId", "dbo.Role");
            DropForeignKey("dbo.AccountRoles", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.AccountRights", "AccountId", "dbo.Account");
            DropIndex("dbo.AccountRoles", new[] { "RoleId" });
            DropIndex("dbo.AccountRoles", new[] { "AccountId" });
            DropIndex("dbo.AccountRoles", new[] { "OrganizationId" });
            DropIndex("dbo.AccountRights", new[] { "OrganizationId" });
            DropIndex("dbo.AccountRights", new[] { "AccessRightId" });
            DropIndex("dbo.AccountRights", new[] { "AccountId" });
            DropTable("dbo.AccountRoles");
            DropTable("dbo.AccountRights");
            CreateIndex("dbo.AccessRightAccount", "Account_Id");
            CreateIndex("dbo.AccessRightAccount", "AccessRight_Id");
            CreateIndex("dbo.AccountRole", "Role_Id");
            CreateIndex("dbo.AccountRole", "Account_Id");
            CreateIndex("dbo.AccountOrganization", "Organization_Id");
            CreateIndex("dbo.AccountOrganization", "Account_Id");
            AddForeignKey("dbo.AccessRightAccount", "Account_Id", "dbo.Account", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AccessRightAccount", "AccessRight_Id", "dbo.AccessRight", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AccountRole", "Role_Id", "dbo.Role", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AccountRole", "Account_Id", "dbo.Account", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AccountOrganization", "Organization_Id", "dbo.Organization", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AccountOrganization", "Account_Id", "dbo.Account", "Id", cascadeDelete: true);
        }
    }
}
