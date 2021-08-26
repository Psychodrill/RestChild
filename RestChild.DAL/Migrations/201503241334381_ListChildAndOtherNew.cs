namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ListChildAndOtherNew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChildIncludeExcludeReason", "AccountId", c => c.Long());
            CreateIndex("dbo.ChildIncludeExcludeReason", "AccountId");
            AddForeignKey("dbo.ChildIncludeExcludeReason", "AccountId", "dbo.Account", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChildIncludeExcludeReason", "AccountId", "dbo.Account");
            DropIndex("dbo.ChildIncludeExcludeReason", new[] { "AccountId" });
            DropColumn("dbo.ChildIncludeExcludeReason", "AccountId");
        }
    }
}
