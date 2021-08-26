namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ListChildAndOther : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ListOfChildsCategory",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                        OrganizationId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.ChildIncludeExcludeReason",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Reason = c.String(),
                        OperartionDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        SignInfoId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SignInfo", t => t.SignInfoId)
                .Index(t => t.SignInfoId);
            
            AddColumn("dbo.SignInfo", "Information", c => c.String(maxLength: 1000));
            AddColumn("dbo.Applicant", "HaveMiddleName", c => c.Boolean(nullable: false));
            AddColumn("dbo.Applicant", "ExcludeReasonId", c => c.Long());
            AddColumn("dbo.Applicant", "IncludeReasonId", c => c.Long());
            AddColumn("dbo.Child", "HaveMiddleName", c => c.Boolean(nullable: false));
            AddColumn("dbo.Child", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Child", "ContactPhone", c => c.String(maxLength: 1000));
            AddColumn("dbo.Child", "ContactPerson", c => c.String(maxLength: 1000));
            AddColumn("dbo.Child", "IncludeReasonId", c => c.Long());
            AddColumn("dbo.Child", "ExcludeReasonId", c => c.Long());
            AddColumn("dbo.ListOfChilds", "ListOfChildsCategoryId", c => c.Long());
            CreateIndex("dbo.Applicant", "ExcludeReasonId");
            CreateIndex("dbo.Applicant", "IncludeReasonId");
            CreateIndex("dbo.Child", "IncludeReasonId");
            CreateIndex("dbo.Child", "ExcludeReasonId");
            CreateIndex("dbo.ListOfChilds", "ListOfChildsCategoryId");
            AddForeignKey("dbo.ListOfChilds", "ListOfChildsCategoryId", "dbo.ListOfChildsCategory", "Id");
            AddForeignKey("dbo.Child", "ExcludeReasonId", "dbo.ChildIncludeExcludeReason", "Id");
            AddForeignKey("dbo.Child", "IncludeReasonId", "dbo.ChildIncludeExcludeReason", "Id");
            AddForeignKey("dbo.Applicant", "ExcludeReasonId", "dbo.ChildIncludeExcludeReason", "Id");
            AddForeignKey("dbo.Applicant", "IncludeReasonId", "dbo.ChildIncludeExcludeReason", "Id");
            DropColumn("dbo.StateMachineAction", "Attribute_420");
            DropColumn("dbo.Child", "IsExcluded");
            DropColumn("dbo.Child", "IsIncluded");
            DropColumn("dbo.Child", "ReasonInclude");
            DropColumn("dbo.Child", "ReasonExclude");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Child", "ReasonExclude", c => c.String(maxLength: 1000));
            AddColumn("dbo.Child", "ReasonInclude", c => c.String(maxLength: 1000));
            AddColumn("dbo.Child", "IsIncluded", c => c.Boolean(nullable: false));
            AddColumn("dbo.Child", "IsExcluded", c => c.Boolean(nullable: false));
            AddColumn("dbo.StateMachineAction", "Attribute_420", c => c.Int());
            DropForeignKey("dbo.Applicant", "IncludeReasonId", "dbo.ChildIncludeExcludeReason");
            DropForeignKey("dbo.Applicant", "ExcludeReasonId", "dbo.ChildIncludeExcludeReason");
            DropForeignKey("dbo.Child", "IncludeReasonId", "dbo.ChildIncludeExcludeReason");
            DropForeignKey("dbo.Child", "ExcludeReasonId", "dbo.ChildIncludeExcludeReason");
            DropForeignKey("dbo.ChildIncludeExcludeReason", "SignInfoId", "dbo.SignInfo");
            DropForeignKey("dbo.ListOfChilds", "ListOfChildsCategoryId", "dbo.ListOfChildsCategory");
            DropForeignKey("dbo.ListOfChildsCategory", "OrganizationId", "dbo.Organization");
            DropIndex("dbo.ChildIncludeExcludeReason", new[] { "SignInfoId" });
            DropIndex("dbo.ListOfChildsCategory", new[] { "OrganizationId" });
            DropIndex("dbo.ListOfChilds", new[] { "ListOfChildsCategoryId" });
            DropIndex("dbo.Child", new[] { "ExcludeReasonId" });
            DropIndex("dbo.Child", new[] { "IncludeReasonId" });
            DropIndex("dbo.Applicant", new[] { "IncludeReasonId" });
            DropIndex("dbo.Applicant", new[] { "ExcludeReasonId" });
            DropColumn("dbo.ListOfChilds", "ListOfChildsCategoryId");
            DropColumn("dbo.Child", "ExcludeReasonId");
            DropColumn("dbo.Child", "IncludeReasonId");
            DropColumn("dbo.Child", "ContactPerson");
            DropColumn("dbo.Child", "ContactPhone");
            DropColumn("dbo.Child", "IsDeleted");
            DropColumn("dbo.Child", "HaveMiddleName");
            DropColumn("dbo.Applicant", "IncludeReasonId");
            DropColumn("dbo.Applicant", "ExcludeReasonId");
            DropColumn("dbo.Applicant", "HaveMiddleName");
            DropColumn("dbo.SignInfo", "Information");
            DropTable("dbo.ChildIncludeExcludeReason");
            DropTable("dbo.ListOfChildsCategory");
        }
    }
}
