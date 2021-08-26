namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewPropertysMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicant", "Male", c => c.Boolean());
            AddColumn("dbo.Applicant", "DateOfBirth", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Applicant", "Position", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applicant", "Position");
            DropColumn("dbo.Applicant", "DateOfBirth");
            DropColumn("dbo.Applicant", "Male");
        }
    }
}
