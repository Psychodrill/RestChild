namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task134589Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FormOfRest", "AgeFrom", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.FormOfRest", "AgeTo", c => c.Int(nullable: false, defaultValue: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FormOfRest", "AgeTo");
            DropColumn("dbo.FormOfRest", "AgeFrom");
        }
    }
}
