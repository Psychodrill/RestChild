namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StateHotelTourAndOther : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Hotels", name: "StateMachineStateId", newName: "StateId");
            RenameIndex(table: "dbo.Hotels", name: "IX_StateMachineStateId", newName: "IX_StateId");
            AddColumn("dbo.TypeOfRest", "ForTour", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tour", "ForList", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tour", "HotelsId", c => c.Long());
            AddColumn("dbo.Tour", "StateId", c => c.Long());
            CreateIndex("dbo.Tour", "HotelsId");
            CreateIndex("dbo.Tour", "StateId");
            AddForeignKey("dbo.Tour", "HotelsId", "dbo.Hotels", "Id");
            AddForeignKey("dbo.Tour", "StateId", "dbo.StateMachineState", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tour", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.Tour", "HotelsId", "dbo.Hotels");
            DropIndex("dbo.Tour", new[] { "StateId" });
            DropIndex("dbo.Tour", new[] { "HotelsId" });
            DropColumn("dbo.Tour", "StateId");
            DropColumn("dbo.Tour", "HotelsId");
            DropColumn("dbo.Tour", "ForList");
            DropColumn("dbo.TypeOfRest", "ForTour");
            RenameIndex(table: "dbo.Hotels", name: "IX_StateId", newName: "IX_StateMachineStateId");
            RenameColumn(table: "dbo.Hotels", name: "StateId", newName: "StateMachineStateId");
        }
    }
}
