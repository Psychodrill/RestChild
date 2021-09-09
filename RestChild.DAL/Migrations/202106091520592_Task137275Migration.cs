namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task137275Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TradeUnionList", "IsCashbackUse", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TradeUnionList", "IsCashbackUse");
        }
    }
}
