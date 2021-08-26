namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CounselorsAddMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Counselors",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LastName = c.String(maxLength: 1000),
                        FirstName = c.String(maxLength: 1000),
                        MiddleName = c.String(maxLength: 1000),
                        HaveMiddleName = c.Boolean(nullable: false),
                        DocumentSeria = c.String(maxLength: 1000),
                        DocumentNumber = c.String(maxLength: 1000),
                        DocumentDateOfIssue = c.DateTime(precision: 7, storeType: "datetime2"),
                        DocumentSubjectIssue = c.String(maxLength: 1000),
                        DateOfBirth = c.DateTime(precision: 7, storeType: "datetime2"),
                        Male = c.Boolean(nullable: false),
                        DateCreate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateUpdate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DocumentTypeId = c.Long(),
                        HistoryLinkId = c.Long(),
                        AccountId = c.Long(),
                        StateId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .ForeignKey("dbo.DocumentType", t => t.DocumentTypeId)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .ForeignKey("dbo.StateMachineState", t => t.StateId)
                .Index(t => t.DocumentTypeId)
                .Index(t => t.HistoryLinkId)
                .Index(t => t.AccountId)
                .Index(t => t.StateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Counselors", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.Counselors", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.Counselors", "DocumentTypeId", "dbo.DocumentType");
            DropForeignKey("dbo.Counselors", "AccountId", "dbo.Account");
            DropIndex("dbo.Counselors", new[] { "StateId" });
            DropIndex("dbo.Counselors", new[] { "AccountId" });
            DropIndex("dbo.Counselors", new[] { "HistoryLinkId" });
            DropIndex("dbo.Counselors", new[] { "DocumentTypeId" });
            DropTable("dbo.Counselors");
        }
    }
}
