namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task95757Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FormOfRest", "IsDeleted", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FormOfRest", "IsDeleted");
        }
    }
}
