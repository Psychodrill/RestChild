namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task96212Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pupil", "Filled", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pupil", "Filled");
        }
    }
}
