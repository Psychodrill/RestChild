namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlaceOfBirthMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicant", "PlaceOfRest", c => c.String(maxLength: 1000));
            AddColumn("dbo.Child", "PlaceOfRest", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Child", "PlaceOfRest");
            DropColumn("dbo.Applicant", "PlaceOfRest");
        }
    }
}
