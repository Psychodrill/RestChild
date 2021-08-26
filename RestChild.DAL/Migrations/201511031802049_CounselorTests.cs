namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CounselorTests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CounselorTestAnswer",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Answer = c.String(),
                        Raiting = c.Decimal(precision: 32, scale: 4),
                        VariantId = c.Long(),
                        CounselorTestId = c.Long(),
                        TrainingCounselorsTestId = c.Long(),
                        TrainingCounselorId = c.Long(),
                        QuestionId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CounselorTest", t => t.CounselorTestId)
                .ForeignKey("dbo.CounselorTestQuestion", t => t.QuestionId)
                .ForeignKey("dbo.TrainingCounselorsTest", t => t.TrainingCounselorsTestId)
                .ForeignKey("dbo.CounselorTestAnswerVariant", t => t.VariantId)
                .ForeignKey("dbo.TrainingCounselorsResult", t => t.TrainingCounselorId)
                .Index(t => t.VariantId)
                .Index(t => t.CounselorTestId)
                .Index(t => t.TrainingCounselorsTestId)
                .Index(t => t.TrainingCounselorId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.CounselorTest",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        Description = c.String(),
                        DateCreate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        StateId = c.Long(),
                        HistoryLinkId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .ForeignKey("dbo.StateMachineState", t => t.StateId)
                .Index(t => t.StateId)
                .Index(t => t.HistoryLinkId);
            
            CreateTable(
                "dbo.CounselorTestQuestion",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Question = c.String(),
                        CounselorTestId = c.Long(),
                        TypeId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CounselorTest", t => t.CounselorTestId)
                .ForeignKey("dbo.CounselorTestQuestionType", t => t.TypeId)
                .Index(t => t.CounselorTestId)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.CounselorTestQuestionType",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CounselorTestAnswerVariant",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Text = c.String(),
                        IsTrue = c.Boolean(nullable: false),
                        QuestionId = c.Long(),
                        FileOrLinkId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileOrLink", t => t.FileOrLinkId)
                .ForeignKey("dbo.CounselorTestQuestion", t => t.QuestionId)
                .Index(t => t.QuestionId)
                .Index(t => t.FileOrLinkId);
            
            CreateTable(
                "dbo.TrainingCounselorsTest",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Rating = c.Decimal(precision: 32, scale: 4),
                        DateTesting = c.DateTime(precision: 7, storeType: "datetime2"),
                        TrainingCounselorsResultId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrainingCounselorsResult", t => t.TrainingCounselorsResultId)
                .Index(t => t.TrainingCounselorsResultId);
            
            CreateTable(
                "dbo.RequestFileTypeTypeOfRest",
                c => new
                    {
                        RequestFileType_Id = c.Long(nullable: false),
                        TypeOfRest_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.RequestFileType_Id, t.TypeOfRest_Id })
                .ForeignKey("dbo.RequestFileType", t => t.RequestFileType_Id, cascadeDelete: true)
                .ForeignKey("dbo.TypeOfRest", t => t.TypeOfRest_Id, cascadeDelete: true)
                .Index(t => t.RequestFileType_Id)
                .Index(t => t.TypeOfRest_Id);
            
            AddColumn("dbo.FileHotel", "IsMainPhoto", c => c.Boolean(nullable: false));
            AddColumn("dbo.FileHotel", "IsArchive", c => c.Boolean(nullable: false));
            AddColumn("dbo.TrainingCounselorsResult", "DateInclude", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.TrainingCounselorsResult", "DateExclude", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.TrainingCounselors", "StartTraining", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.TrainingCounselors", "EndTraining", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.TrainingCounselors", "Duration", c => c.Int(nullable: false));
            AddColumn("dbo.TrainingCounselors", "StateId", c => c.Long());
            CreateIndex("dbo.TrainingCounselors", "StateId");
            AddForeignKey("dbo.TrainingCounselors", "StateId", "dbo.StateMachineState", "Id");
            DropColumn("dbo.TrainingCounselors", "StartLeanring");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TrainingCounselors", "StartLeanring", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropForeignKey("dbo.TrainingCounselors", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.TrainingCounselorsTest", "TrainingCounselorsResultId", "dbo.TrainingCounselorsResult");
            DropForeignKey("dbo.CounselorTestAnswer", "TrainingCounselorId", "dbo.TrainingCounselorsResult");
            DropForeignKey("dbo.CounselorTestAnswer", "VariantId", "dbo.CounselorTestAnswerVariant");
            DropForeignKey("dbo.CounselorTestAnswer", "TrainingCounselorsTestId", "dbo.TrainingCounselorsTest");
            DropForeignKey("dbo.CounselorTestAnswer", "QuestionId", "dbo.CounselorTestQuestion");
            DropForeignKey("dbo.CounselorTestAnswer", "CounselorTestId", "dbo.CounselorTest");
            DropForeignKey("dbo.CounselorTest", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.CounselorTestAnswerVariant", "QuestionId", "dbo.CounselorTestQuestion");
            DropForeignKey("dbo.CounselorTestAnswerVariant", "FileOrLinkId", "dbo.FileOrLink");
            DropForeignKey("dbo.CounselorTestQuestion", "TypeId", "dbo.CounselorTestQuestionType");
            DropForeignKey("dbo.CounselorTestQuestion", "CounselorTestId", "dbo.CounselorTest");
            DropForeignKey("dbo.CounselorTest", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.RequestFileTypeTypeOfRest", "TypeOfRest_Id", "dbo.TypeOfRest");
            DropForeignKey("dbo.RequestFileTypeTypeOfRest", "RequestFileType_Id", "dbo.RequestFileType");
            DropIndex("dbo.RequestFileTypeTypeOfRest", new[] { "TypeOfRest_Id" });
            DropIndex("dbo.RequestFileTypeTypeOfRest", new[] { "RequestFileType_Id" });
            DropIndex("dbo.TrainingCounselors", new[] { "StateId" });
            DropIndex("dbo.TrainingCounselorsTest", new[] { "TrainingCounselorsResultId" });
            DropIndex("dbo.CounselorTestAnswerVariant", new[] { "FileOrLinkId" });
            DropIndex("dbo.CounselorTestAnswerVariant", new[] { "QuestionId" });
            DropIndex("dbo.CounselorTestQuestion", new[] { "TypeId" });
            DropIndex("dbo.CounselorTestQuestion", new[] { "CounselorTestId" });
            DropIndex("dbo.CounselorTest", new[] { "HistoryLinkId" });
            DropIndex("dbo.CounselorTest", new[] { "StateId" });
            DropIndex("dbo.CounselorTestAnswer", new[] { "QuestionId" });
            DropIndex("dbo.CounselorTestAnswer", new[] { "TrainingCounselorId" });
            DropIndex("dbo.CounselorTestAnswer", new[] { "TrainingCounselorsTestId" });
            DropIndex("dbo.CounselorTestAnswer", new[] { "CounselorTestId" });
            DropIndex("dbo.CounselorTestAnswer", new[] { "VariantId" });
            DropColumn("dbo.TrainingCounselors", "StateId");
            DropColumn("dbo.TrainingCounselors", "Duration");
            DropColumn("dbo.TrainingCounselors", "EndTraining");
            DropColumn("dbo.TrainingCounselors", "StartTraining");
            DropColumn("dbo.TrainingCounselorsResult", "DateExclude");
            DropColumn("dbo.TrainingCounselorsResult", "DateInclude");
            DropColumn("dbo.FileHotel", "IsArchive");
            DropColumn("dbo.FileHotel", "IsMainPhoto");
            DropTable("dbo.RequestFileTypeTypeOfRest");
            DropTable("dbo.TrainingCounselorsTest");
            DropTable("dbo.CounselorTestAnswerVariant");
            DropTable("dbo.CounselorTestQuestionType");
            DropTable("dbo.CounselorTestQuestion");
            DropTable("dbo.CounselorTest");
            DropTable("dbo.CounselorTestAnswer");
        }
    }
}
