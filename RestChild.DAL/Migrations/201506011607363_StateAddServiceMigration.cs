namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StateAddServiceMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddonServices", "Description", c => c.String());
            AddColumn("dbo.AddonServices", "ParentId", c => c.Long());
            AddColumn("dbo.AddonServices", "StateId", c => c.Long());
            CreateIndex("dbo.AddonServices", "ParentId");
            CreateIndex("dbo.AddonServices", "StateId");
            AddForeignKey("dbo.AddonServices", "ParentId", "dbo.AddonServices", "Id");
            AddForeignKey("dbo.AddonServices", "StateId", "dbo.StateMachineState", "Id");			
        }
        
		
        public override void Down()
        {
            DropForeignKey("dbo.AddonServices", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.AddonServices", "ParentId", "dbo.AddonServices");
            DropIndex("dbo.AddonServices", new[] { "StateId" });
            DropIndex("dbo.AddonServices", new[] { "ParentId" });
            DropColumn("dbo.AddonServices", "StateId");
            DropColumn("dbo.AddonServices", "ParentId");
            DropColumn("dbo.AddonServices", "Description");
        }
    }
}
