namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InteragencyRequestModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HistoryInteragencyRequest",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Operation = c.String(),
                        OperationDate = c.DateTime(nullable: false),
                        Code = c.String(maxLength: 1000),
                        InteragencyRequestId = c.Long(),
                        AccountId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .ForeignKey("dbo.InteragencyRequest", t => t.InteragencyRequestId)
                .Index(t => t.InteragencyRequestId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.InteragencyRequest",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RequestNumber = c.String(maxLength: 1000),
                        RequsetDate = c.DateTime(),
                        AnswerNumber = c.String(maxLength: 1000),
                        AnswerDate = c.DateTime(),
                        RequestComment = c.String(),
                        AnswerComment = c.String(),
                        RequestFileUrl = c.String(),
                        AnswerFileUrl = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        AccountId = c.Long(),
                        StatusInteragencyRequestId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .ForeignKey("dbo.StatusInteragencyRequest", t => t.StatusInteragencyRequestId)
                .Index(t => t.AccountId)
                .Index(t => t.StatusInteragencyRequestId);
            
            CreateTable(
                "dbo.StatusInteragencyRequest",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HistoryRequest",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Operation = c.String(),
                        OperationDate = c.DateTime(nullable: false),
                        Code = c.String(maxLength: 1000),
                        RequestId = c.Long(),
                        AccountId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .ForeignKey("dbo.Request", t => t.RequestId)
                .Index(t => t.RequestId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.InteragencyRequestResult",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Comment = c.String(),
                        InteragencyRequestId = c.Long(),
                        ChildId = c.Long(),
                        StatusResultId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Child", t => t.ChildId)
                .ForeignKey("dbo.InteragencyRequest", t => t.InteragencyRequestId)
                .ForeignKey("dbo.StatusResult", t => t.StatusResultId)
                .Index(t => t.InteragencyRequestId)
                .Index(t => t.ChildId)
                .Index(t => t.StatusResultId);
            
            CreateTable(
                "dbo.StatusResult",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InteragencyRequestResult", "StatusResultId", "dbo.StatusResult");
            DropForeignKey("dbo.InteragencyRequestResult", "InteragencyRequestId", "dbo.InteragencyRequest");
            DropForeignKey("dbo.InteragencyRequestResult", "ChildId", "dbo.Child");
            DropForeignKey("dbo.HistoryRequest", "RequestId", "dbo.Request");
            DropForeignKey("dbo.HistoryRequest", "AccountId", "dbo.Account");
            DropForeignKey("dbo.HistoryInteragencyRequest", "InteragencyRequestId", "dbo.InteragencyRequest");
            DropForeignKey("dbo.InteragencyRequest", "StatusInteragencyRequestId", "dbo.StatusInteragencyRequest");
            DropForeignKey("dbo.InteragencyRequest", "AccountId", "dbo.Account");
            DropForeignKey("dbo.HistoryInteragencyRequest", "AccountId", "dbo.Account");
            DropIndex("dbo.InteragencyRequestResult", new[] { "StatusResultId" });
            DropIndex("dbo.InteragencyRequestResult", new[] { "ChildId" });
            DropIndex("dbo.InteragencyRequestResult", new[] { "InteragencyRequestId" });
            DropIndex("dbo.HistoryRequest", new[] { "AccountId" });
            DropIndex("dbo.HistoryRequest", new[] { "RequestId" });
            DropIndex("dbo.InteragencyRequest", new[] { "StatusInteragencyRequestId" });
            DropIndex("dbo.InteragencyRequest", new[] { "AccountId" });
            DropIndex("dbo.HistoryInteragencyRequest", new[] { "AccountId" });
            DropIndex("dbo.HistoryInteragencyRequest", new[] { "InteragencyRequestId" });
            DropTable("dbo.StatusResult");
            DropTable("dbo.InteragencyRequestResult");
            DropTable("dbo.HistoryRequest");
            DropTable("dbo.StatusInteragencyRequest");
            DropTable("dbo.InteragencyRequest");
            DropTable("dbo.HistoryInteragencyRequest");
        }
    }
}
