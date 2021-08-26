namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task105360Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pupil", "Foul", c => c.Boolean(defaultValue: false, nullable: false));
            AddColumn("dbo.Pupil", "FoulRegionRestriction", c => c.Boolean(defaultValue: false, nullable: false));
            AddColumn("dbo.Pupil", "FoulRegionRestrictionFrom", c => c.DateTime(precision: 7, storeType: "datetime2", nullable: true));
            AddColumn("dbo.Pupil", "FoulRegionRestrictionTo", c => c.DateTime(precision: 7, storeType: "datetime2", nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pupil", "FoulRegionRestrictionTo");
            DropColumn("dbo.Pupil", "FoulRegionRestrictionFrom");
            DropColumn("dbo.Pupil", "FoulRegionRestriction");
            DropColumn("dbo.Pupil", "Foul");
        }
    }
}
