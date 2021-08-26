namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlaceOfBirthMigration2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicant", "PlaceOfBirth", c => c.String(maxLength: 1000));
            AddColumn("dbo.Child", "PlaceOfBirth", c => c.String(maxLength: 1000));
            DropColumn("dbo.Applicant", "PlaceOfRest");
            DropColumn("dbo.Child", "PlaceOfRest");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Child", "PlaceOfRest", c => c.String(maxLength: 1000));
            AddColumn("dbo.Applicant", "PlaceOfRest", c => c.String(maxLength: 1000));
            DropColumn("dbo.Child", "PlaceOfBirth");
            DropColumn("dbo.Applicant", "PlaceOfBirth");
        }
    }
}
