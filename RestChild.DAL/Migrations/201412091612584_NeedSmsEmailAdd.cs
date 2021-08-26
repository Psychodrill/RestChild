namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NeedSmsEmailAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Request", "NeedEmail", c => c.Boolean(nullable: false));
            AddColumn("dbo.Request", "NeedSms", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Request", "NeedSms");
            DropColumn("dbo.Request", "NeedEmail");
        }
    }
}
