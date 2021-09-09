namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task139443Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Request", "NeedSendForFRI", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Request", "NeedSendForFRI");
        }
    }
}
