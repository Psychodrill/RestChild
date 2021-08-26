namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lok2017ListOfChildFamilyMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicant", "CountryId", c => c.Long());
            AddColumn("dbo.Child", "CountryId", c => c.Long());
            AddColumn("dbo.TypeOfRest", "HotelTypeId", c => c.Long());
            CreateIndex("dbo.Applicant", "CountryId");
            CreateIndex("dbo.Child", "CountryId");
            CreateIndex("dbo.TypeOfRest", "HotelTypeId");
            AddForeignKey("dbo.TypeOfRest", "HotelTypeId", "dbo.HotelType", "Id");
            AddForeignKey("dbo.Child", "CountryId", "dbo.Country", "Id");
            AddForeignKey("dbo.Applicant", "CountryId", "dbo.Country", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applicant", "CountryId", "dbo.Country");
            DropForeignKey("dbo.Child", "CountryId", "dbo.Country");
            DropForeignKey("dbo.TypeOfRest", "HotelTypeId", "dbo.HotelType");
            DropIndex("dbo.TypeOfRest", new[] { "HotelTypeId" });
            DropIndex("dbo.Child", new[] { "CountryId" });
            DropIndex("dbo.Applicant", new[] { "CountryId" });
            DropColumn("dbo.TypeOfRest", "HotelTypeId");
            DropColumn("dbo.Child", "CountryId");
            DropColumn("dbo.Applicant", "CountryId");
        }
    }
}
