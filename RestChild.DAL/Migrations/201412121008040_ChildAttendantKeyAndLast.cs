namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChildAttendantKeyAndLast : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agent", "IsLast", c => c.Boolean(nullable: false));
            AddColumn("dbo.Applicant", "Key", c => c.String(maxLength: 400));
            AddColumn("dbo.Applicant", "IntervalStart", c => c.Long());
            AddColumn("dbo.Applicant", "IntervalEnd", c => c.Long());
            AddColumn("dbo.Applicant", "IsLast", c => c.Boolean(nullable: false));
            AddColumn("dbo.Child", "Key", c => c.String(maxLength: 400));
            AddColumn("dbo.Child", "IntervalStart", c => c.Long());
            AddColumn("dbo.Child", "IntervalEnd", c => c.Long());
            AddColumn("dbo.Child", "KeySame", c => c.String(maxLength: 400));
            AddColumn("dbo.Child", "IsLast", c => c.Boolean(nullable: false));
			Sql("Update dbo.Child set IsLast = (select IsLast from dbo.Request r where r.Id=dbo.Child.RequestId)");
			Sql("Update dbo.Applicant set IsLast = (select IsLast from dbo.Request r where r.Id=dbo.Applicant.RequestId), IsApplicant=0 where RequestId is not null");
			Sql("Update dbo.Applicant set IsLast = (select IsLast from dbo.Request r where r.ApplicantId=dbo.Applicant.Id), IsApplicant=1 where RequestId is null");
			Sql("Update dbo.Agent set IsLast = (select IsLast from dbo.Request r where r.AgentId=dbo.Agent.Id)");
			CreateIndex("dbo.Child", "Key");
			CreateIndex("dbo.Applicant", "Key");
			CreateIndex("dbo.Child", "KeySame");
		}
        
        public override void Down()
        {
			DropIndex("dbo.Child", "Key");
			DropIndex("dbo.Applicant", "Key");
			DropIndex("dbo.Child", "KeySame");
            DropColumn("dbo.Child", "IsLast");
            DropColumn("dbo.Child", "KeySame");
            DropColumn("dbo.Child", "IntervalEnd");
            DropColumn("dbo.Child", "IntervalStart");
            DropColumn("dbo.Child", "Key");
            DropColumn("dbo.Applicant", "IsLast");
            DropColumn("dbo.Applicant", "IntervalEnd");
            DropColumn("dbo.Applicant", "IntervalStart");
            DropColumn("dbo.Applicant", "Key");
            DropColumn("dbo.Agent", "IsLast");
        }
    }
}
