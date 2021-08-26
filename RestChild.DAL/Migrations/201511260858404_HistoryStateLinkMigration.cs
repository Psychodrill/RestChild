namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HistoryStateLinkMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.History", "ToStateId", c => c.Long());
            AddColumn("dbo.History", "FromStateId", c => c.Long());
            CreateIndex("dbo.History", "ToStateId");
            CreateIndex("dbo.History", "FromStateId");
            AddForeignKey("dbo.History", "FromStateId", "dbo.StateMachineState", "Id");
            AddForeignKey("dbo.History", "ToStateId", "dbo.StateMachineState", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.History", "ToStateId", "dbo.StateMachineState");
            DropForeignKey("dbo.History", "FromStateId", "dbo.StateMachineState");
            DropIndex("dbo.History", new[] { "FromStateId" });
            DropIndex("dbo.History", new[] { "ToStateId" });
            DropColumn("dbo.History", "FromStateId");
            DropColumn("dbo.History", "ToStateId");
        }
    }
}
