namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeriaNumberCertOfBirth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicant", "DocumentSeriaCertOfBirth", c => c.String(maxLength: 1000));
            AddColumn("dbo.Applicant", "DocumentNumberCertOfBirth", c => c.String(maxLength: 1000));
            AddColumn("dbo.Applicant", "KeyOther", c => c.String(maxLength: 400));
            AddColumn("dbo.Child", "DocumentSeriaCertOfBirth", c => c.String(maxLength: 1000));
            AddColumn("dbo.Child", "DocumentNumberCertOfBirth", c => c.String(maxLength: 1000));
            AddColumn("dbo.Child", "KeyOther", c => c.String(maxLength: 400));
			CreateIndex("dbo.Child", "KeyOther", false, "IX_KeyOther");
			CreateIndex("dbo.Applicant", "KeyOther", false, "IX_KeyOther");
		}
        
        public override void Down()
        {
			DropIndex("dbo.Child", "IX_KeyOther");
			DropIndex("dbo.Applicant", "IX_KeyOther");
			DropColumn("dbo.Child", "KeyOther");
            DropColumn("dbo.Child", "DocumentNumberCertOfBirth");
            DropColumn("dbo.Child", "DocumentSeriaCertOfBirth");
            DropColumn("dbo.Applicant", "KeyOther");
            DropColumn("dbo.Applicant", "DocumentNumberCertOfBirth");
            DropColumn("dbo.Applicant", "DocumentSeriaCertOfBirth");
        }
    }
}
