namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgeCalc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeOfRest", "DayOfMonth", c => c.Int(nullable: false));
            AddColumn("dbo.TimeOfRest", "Month", c => c.Int(nullable: false));
            AddColumn("dbo.TimeOfRest", "PeriodLength", c => c.Int(nullable: false));
            AddColumn("dbo.TypeOfRest", "MinAge", c => c.Int(nullable: false));
            AddColumn("dbo.TypeOfRest", "MaxAge", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TypeOfRest", "MaxAge");
            DropColumn("dbo.TypeOfRest", "MinAge");
            DropColumn("dbo.TimeOfRest", "PeriodLength");
            DropColumn("dbo.TimeOfRest", "Month");
            DropColumn("dbo.TimeOfRest", "DayOfMonth");
        }
    }
}
