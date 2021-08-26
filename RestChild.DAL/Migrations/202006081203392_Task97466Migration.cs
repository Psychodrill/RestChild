namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task97466Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestForPeriodOfRest", "PupilsCount", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.RequestForPeriodOfRest", "CollaboratorsCount", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.RequestForPeriodOfRest", "MGTCollaboratorsCount", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequestForPeriodOfRest", "MGTCollaboratorsCount");
            DropColumn("dbo.RequestForPeriodOfRest", "CollaboratorsCount");
            DropColumn("dbo.RequestForPeriodOfRest", "PupilsCount");
        }
    }
}
