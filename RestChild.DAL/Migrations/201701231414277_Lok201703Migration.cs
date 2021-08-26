namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lok201703Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tour", "ForMultipleStageCompany", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tour", "ForMultipleStageCompany");
        }
    }
}
