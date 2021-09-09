namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task126938_Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MGTWorkingDay", "SuoVisitTooEarly", c => c.Long(nullable: true));
            AddColumn("dbo.MGTWorkingDay", "SuoVisitTooLate", c => c.Long(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MGTWorkingDay", "SuoVisitTooLate");
            DropColumn("dbo.MGTWorkingDay", "SuoVisitTooEarly");
        }
    }
}
