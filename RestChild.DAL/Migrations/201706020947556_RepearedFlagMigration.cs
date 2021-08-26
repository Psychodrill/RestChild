namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RepearedFlagMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Request", "Repared", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Request", "Repared");
        }
    }
}
