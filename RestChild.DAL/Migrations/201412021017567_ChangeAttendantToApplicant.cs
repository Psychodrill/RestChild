namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAttendantToApplicant : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Child", "AttendantId", "dbo.Attendant");
            DropForeignKey("dbo.Attendant", "DocumentTypeId", "dbo.DocumentType");
            DropForeignKey("dbo.Attendant", "EntityId", "dbo.Attendant");
            DropIndex("dbo.Attendant", new[] { "RequestId" });
            DropIndex("dbo.Attendant", new[] { "DocumentTypeId" });
            DropIndex("dbo.Attendant", new[] { "EntityId" });
            DropIndex("dbo.Child", new[] { "AttendantId" });
            AddColumn("dbo.Applicant", "IsApplicant", c => c.Boolean(nullable: false));
            AddColumn("dbo.Applicant", "EntityId", c => c.Long());
            AddColumn("dbo.Applicant", "RequestId", c => c.Long());
            AddColumn("dbo.Child", "ApplicantId", c => c.Long());
            CreateIndex("dbo.Applicant", "EntityId");
            CreateIndex("dbo.Applicant", "RequestId");
            CreateIndex("dbo.Child", "ApplicantId");
            AddForeignKey("dbo.Child", "ApplicantId", "dbo.Applicant", "Id");
            AddForeignKey("dbo.Applicant", "EntityId", "dbo.Applicant", "Id");
            DropColumn("dbo.Child", "AttendantId");
            DropTable("dbo.Attendant");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Attendant",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LastName = c.String(maxLength: 1000),
                        FirstName = c.String(maxLength: 1000),
                        MiddleName = c.String(maxLength: 1000),
                        DocumentSeria = c.String(maxLength: 1000),
                        DocumentNumber = c.String(maxLength: 1000),
                        DocumentDateOfIssue = c.DateTime(precision: 7, storeType: "datetime2"),
                        DocumentSubjectIssue = c.String(maxLength: 1000),
                        Phone = c.String(maxLength: 1000),
                        Email = c.String(maxLength: 1000),
                        RequestId = c.Long(),
                        DocumentTypeId = c.Long(),
                        EntityId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Child", "AttendantId", c => c.Long());
            DropForeignKey("dbo.Applicant", "EntityId", "dbo.Applicant");
            DropForeignKey("dbo.Child", "ApplicantId", "dbo.Applicant");
            DropIndex("dbo.Child", new[] { "ApplicantId" });
            DropIndex("dbo.Applicant", new[] { "RequestId" });
            DropIndex("dbo.Applicant", new[] { "EntityId" });
            DropColumn("dbo.Child", "ApplicantId");
            DropColumn("dbo.Applicant", "RequestId");
            DropColumn("dbo.Applicant", "EntityId");
            DropColumn("dbo.Applicant", "IsApplicant");
            CreateIndex("dbo.Child", "AttendantId");
            CreateIndex("dbo.Attendant", "EntityId");
            CreateIndex("dbo.Attendant", "DocumentTypeId");
            CreateIndex("dbo.Attendant", "RequestId");
            AddForeignKey("dbo.Attendant", "EntityId", "dbo.Attendant", "Id");
            AddForeignKey("dbo.Attendant", "DocumentTypeId", "dbo.DocumentType", "Id");
            AddForeignKey("dbo.Child", "AttendantId", "dbo.Attendant", "Id");
        }
    }
}
