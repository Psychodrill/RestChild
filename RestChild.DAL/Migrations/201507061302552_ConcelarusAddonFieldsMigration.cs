namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConcelarusAddonFieldsMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CounselorCources",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CounselorPractice",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Camp = c.String(nullable: false),
                        Year = c.String(nullable: false),
                        Tour = c.String(),
                        Party = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ForeginPassport",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PassportNumber = c.String(),
                        PassportIssueDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        PassportValidityEndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        PassportIssue = c.String(),
                        CounselorsId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Counselors", t => t.CounselorsId)
                .Index(t => t.CounselorsId);
            
            CreateTable(
                "dbo.MatrialStatus",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MilitaryDuty",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CounselorSkill",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OtherText = c.String(),
                        CounselorsId = c.Long(),
                        SkillId = c.Long(),
                        SkillVocabularyId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Skill", t => t.SkillId)
                .ForeignKey("dbo.SkillVocabulary", t => t.SkillVocabularyId)
                .ForeignKey("dbo.Counselors", t => t.CounselorsId)
                .Index(t => t.CounselorsId)
                .Index(t => t.SkillId)
                .Index(t => t.SkillVocabularyId);
            
            CreateTable(
                "dbo.Skill",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        SortOrder = c.Long(),
                        NeedText = c.Boolean(nullable: false),
                        NeedVocabulary = c.Boolean(),
                        SkillsGroupId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SkillsGroup", t => t.SkillsGroupId)
                .Index(t => t.SkillsGroupId);
            
            CreateTable(
                "dbo.SkillsGroup",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        SortOrder = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SkillVocabulary",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        SortOrder = c.Long(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        SkillId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Skill", t => t.SkillId)
                .Index(t => t.SkillId);
            
            CreateTable(
                "dbo.StateDistrict",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TieColor",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsActive = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypeOfEducation",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CouncelorComment",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Author = c.String(nullable: false),
                        Comment = c.String(),
                        DateCreate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        SystemInfo = c.String(),
                        VisibleOnSite = c.Boolean(nullable: false),
                        SiteUserUid = c.String(maxLength: 300),
                        CommentsId = c.Long(),
                        ApplicantId = c.Long(),
                        AccountId = c.Long(),
                        ChildId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .ForeignKey("dbo.Applicant", t => t.ApplicantId)
                .ForeignKey("dbo.Child", t => t.ChildId)
                .ForeignKey("dbo.Counselors", t => t.CommentsId)
                .Index(t => t.CommentsId)
                .Index(t => t.ApplicantId)
                .Index(t => t.AccountId)
                .Index(t => t.ChildId);
            
            CreateTable(
                "dbo.CounselorHighSchool",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EducationInstitutionName = c.String(),
                        Department = c.String(),
                        Speciality = c.String(),
                        Course = c.String(),
                        GraduationYear = c.String(),
                        HighSchoolGraduationsId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Counselors", t => t.HighSchoolGraduationsId)
                .Index(t => t.HighSchoolGraduationsId);
            
            AddColumn("dbo.Counselors", "RegistrationAddtress", c => c.String());
            AddColumn("dbo.Counselors", "Snils", c => c.String());
            AddColumn("dbo.Counselors", "Inn", c => c.String());
            AddColumn("dbo.Counselors", "ForeignPassport", c => c.Boolean());
            AddColumn("dbo.Counselors", "MilitaryReserveCategory", c => c.String());
            AddColumn("dbo.Counselors", "MilitaryRank", c => c.String());
            AddColumn("dbo.Counselors", "MilitartStaff", c => c.String());
            AddColumn("dbo.Counselors", "VusCodeName", c => c.String());
            AddColumn("dbo.Counselors", "MIlitaryCategory", c => c.String());
            AddColumn("dbo.Counselors", "GoldenSail", c => c.Boolean(nullable: false));
            AddColumn("dbo.Counselors", "UnaccountedForWaste", c => c.Int(nullable: false));
            AddColumn("dbo.Counselors", "LinkFacebook", c => c.String(maxLength: 1000));
            AddColumn("dbo.Counselors", "LinkVk", c => c.String(maxLength: 1000));
            AddColumn("dbo.Counselors", "LinkОк", c => c.String(maxLength: 1000));
            AddColumn("dbo.Counselors", "LinkInstagramm", c => c.String(maxLength: 1000));
            AddColumn("dbo.Counselors", "CounselorPracticeId", c => c.Long());
            AddColumn("dbo.Counselors", "CounselorCourcesId", c => c.Long());
            AddColumn("dbo.Counselors", "TieColorId", c => c.Long());
            AddColumn("dbo.Counselors", "StateDistrictId", c => c.Long());
            AddColumn("dbo.Counselors", "MatrialStatusId", c => c.Long());
            AddColumn("dbo.Counselors", "MilitaryDutyId", c => c.Long());
            AddColumn("dbo.Counselors", "TypeOfEducationId", c => c.Long());
            CreateIndex("dbo.Counselors", "CounselorPracticeId");
            CreateIndex("dbo.Counselors", "CounselorCourcesId");
            CreateIndex("dbo.Counselors", "TieColorId");
            CreateIndex("dbo.Counselors", "StateDistrictId");
            CreateIndex("dbo.Counselors", "MatrialStatusId");
            CreateIndex("dbo.Counselors", "MilitaryDutyId");
            CreateIndex("dbo.Counselors", "TypeOfEducationId");
            AddForeignKey("dbo.Counselors", "CounselorCourcesId", "dbo.CounselorCources", "Id");
            AddForeignKey("dbo.Counselors", "CounselorPracticeId", "dbo.CounselorPractice", "Id");
            AddForeignKey("dbo.Counselors", "MatrialStatusId", "dbo.MatrialStatus", "Id");
            AddForeignKey("dbo.Counselors", "MilitaryDutyId", "dbo.MilitaryDuty", "Id");
            AddForeignKey("dbo.Counselors", "StateDistrictId", "dbo.StateDistrict", "Id");
            AddForeignKey("dbo.Counselors", "TieColorId", "dbo.TieColor", "Id");
            AddForeignKey("dbo.Counselors", "TypeOfEducationId", "dbo.TypeOfEducation", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CounselorHighSchool", "HighSchoolGraduationsId", "dbo.Counselors");
            DropForeignKey("dbo.CouncelorComment", "CommentsId", "dbo.Counselors");
            DropForeignKey("dbo.CouncelorComment", "ChildId", "dbo.Child");
            DropForeignKey("dbo.CouncelorComment", "ApplicantId", "dbo.Applicant");
            DropForeignKey("dbo.CouncelorComment", "AccountId", "dbo.Account");
            DropForeignKey("dbo.Counselors", "TypeOfEducationId", "dbo.TypeOfEducation");
            DropForeignKey("dbo.Counselors", "TieColorId", "dbo.TieColor");
            DropForeignKey("dbo.Counselors", "StateDistrictId", "dbo.StateDistrict");
            DropForeignKey("dbo.CounselorSkill", "CounselorsId", "dbo.Counselors");
            DropForeignKey("dbo.CounselorSkill", "SkillVocabularyId", "dbo.SkillVocabulary");
            DropForeignKey("dbo.SkillVocabulary", "SkillId", "dbo.Skill");
            DropForeignKey("dbo.CounselorSkill", "SkillId", "dbo.Skill");
            DropForeignKey("dbo.Skill", "SkillsGroupId", "dbo.SkillsGroup");
            DropForeignKey("dbo.Counselors", "MilitaryDutyId", "dbo.MilitaryDuty");
            DropForeignKey("dbo.Counselors", "MatrialStatusId", "dbo.MatrialStatus");
            DropForeignKey("dbo.ForeginPassport", "CounselorsId", "dbo.Counselors");
            DropForeignKey("dbo.Counselors", "CounselorPracticeId", "dbo.CounselorPractice");
            DropForeignKey("dbo.Counselors", "CounselorCourcesId", "dbo.CounselorCources");
            DropIndex("dbo.CounselorHighSchool", new[] { "HighSchoolGraduationsId" });
            DropIndex("dbo.CouncelorComment", new[] { "ChildId" });
            DropIndex("dbo.CouncelorComment", new[] { "AccountId" });
            DropIndex("dbo.CouncelorComment", new[] { "ApplicantId" });
            DropIndex("dbo.CouncelorComment", new[] { "CommentsId" });
            DropIndex("dbo.SkillVocabulary", new[] { "SkillId" });
            DropIndex("dbo.Skill", new[] { "SkillsGroupId" });
            DropIndex("dbo.CounselorSkill", new[] { "SkillVocabularyId" });
            DropIndex("dbo.CounselorSkill", new[] { "SkillId" });
            DropIndex("dbo.CounselorSkill", new[] { "CounselorsId" });
            DropIndex("dbo.ForeginPassport", new[] { "CounselorsId" });
            DropIndex("dbo.Counselors", new[] { "TypeOfEducationId" });
            DropIndex("dbo.Counselors", new[] { "MilitaryDutyId" });
            DropIndex("dbo.Counselors", new[] { "MatrialStatusId" });
            DropIndex("dbo.Counselors", new[] { "StateDistrictId" });
            DropIndex("dbo.Counselors", new[] { "TieColorId" });
            DropIndex("dbo.Counselors", new[] { "CounselorCourcesId" });
            DropIndex("dbo.Counselors", new[] { "CounselorPracticeId" });
            DropColumn("dbo.Counselors", "TypeOfEducationId");
            DropColumn("dbo.Counselors", "MilitaryDutyId");
            DropColumn("dbo.Counselors", "MatrialStatusId");
            DropColumn("dbo.Counselors", "StateDistrictId");
            DropColumn("dbo.Counselors", "TieColorId");
            DropColumn("dbo.Counselors", "CounselorCourcesId");
            DropColumn("dbo.Counselors", "CounselorPracticeId");
            DropColumn("dbo.Counselors", "LinkInstagramm");
            DropColumn("dbo.Counselors", "LinkОк");
            DropColumn("dbo.Counselors", "LinkVk");
            DropColumn("dbo.Counselors", "LinkFacebook");
            DropColumn("dbo.Counselors", "UnaccountedForWaste");
            DropColumn("dbo.Counselors", "GoldenSail");
            DropColumn("dbo.Counselors", "MIlitaryCategory");
            DropColumn("dbo.Counselors", "VusCodeName");
            DropColumn("dbo.Counselors", "MilitartStaff");
            DropColumn("dbo.Counselors", "MilitaryRank");
            DropColumn("dbo.Counselors", "MilitaryReserveCategory");
            DropColumn("dbo.Counselors", "ForeignPassport");
            DropColumn("dbo.Counselors", "Inn");
            DropColumn("dbo.Counselors", "Snils");
            DropColumn("dbo.Counselors", "RegistrationAddtress");
            DropTable("dbo.CounselorHighSchool");
            DropTable("dbo.CouncelorComment");
            DropTable("dbo.TypeOfEducation");
            DropTable("dbo.TieColor");
            DropTable("dbo.StateDistrict");
            DropTable("dbo.SkillVocabulary");
            DropTable("dbo.SkillsGroup");
            DropTable("dbo.Skill");
            DropTable("dbo.CounselorSkill");
            DropTable("dbo.MilitaryDuty");
            DropTable("dbo.MatrialStatus");
            DropTable("dbo.ForeginPassport");
            DropTable("dbo.CounselorPractice");
            DropTable("dbo.CounselorCources");
        }
    }
}
