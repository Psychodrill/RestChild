namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoutMemoMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bout", "Memo", c => c.String());
            AddColumn("dbo.Bout", "MemoFile", c => c.String(maxLength: 1000));
            AddColumn("dbo.Bout", "MemoLink", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bout", "MemoLink");
            DropColumn("dbo.Bout", "MemoFile");
            DropColumn("dbo.Bout", "Memo");
        }
    }
}
