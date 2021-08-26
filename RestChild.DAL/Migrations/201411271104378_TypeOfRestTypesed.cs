namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TypeOfRestTypesed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeOfRest", "NeedPlace", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfRest", "NeedPlacment", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfRest", "NeedSubject", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfRest", "NeedApplicant", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfRest", "NeedAttendant", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TypeOfRest", "NeedAttendant");
            DropColumn("dbo.TypeOfRest", "NeedApplicant");
            DropColumn("dbo.TypeOfRest", "NeedSubject");
            DropColumn("dbo.TypeOfRest", "NeedPlacment");
            DropColumn("dbo.TypeOfRest", "NeedPlace");
        }
    }
}
