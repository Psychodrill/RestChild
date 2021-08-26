namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlaceOfBirthMigration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdministratorTour", "PlaceOfBirth", c => c.String(maxLength: 1000));
            AddColumn("dbo.AdministratorTour", "DocumentTypeId", c => c.Long());
            AddColumn("dbo.Counselors", "PlaceOfBirth", c => c.String(maxLength: 1000));
            CreateIndex("dbo.AdministratorTour", "DocumentTypeId");
            AddForeignKey("dbo.AdministratorTour", "DocumentTypeId", "dbo.DocumentType", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdministratorTour", "DocumentTypeId", "dbo.DocumentType");
            DropIndex("dbo.AdministratorTour", new[] { "DocumentTypeId" });
            DropColumn("dbo.Counselors", "PlaceOfBirth");
            DropColumn("dbo.AdministratorTour", "DocumentTypeId");
            DropColumn("dbo.AdministratorTour", "PlaceOfBirth");
        }
    }
}
