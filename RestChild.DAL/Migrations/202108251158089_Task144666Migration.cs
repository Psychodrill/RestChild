namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task144666Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeOfRest", "UrlToStationaryTypeOfCampPhoto", c => c.String(maxLength: 1000));
            AddColumn("dbo.TypeOfRest", "UrlToCampTypeOfCampPhoto", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TypeOfRest", "UrlToCampTypeOfCampPhoto");
            DropColumn("dbo.TypeOfRest", "UrlToStationaryTypeOfCampPhoto");
        }
    }
}
