namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TypeOfRestNeedPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeOfRest", "NeedPrice", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TypeOfRest", "NeedPrice");
        }
    }
}
