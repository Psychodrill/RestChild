namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StateMachineMigrationChange : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.LimitOnOrganization", name: "StateMachineStateId", newName: "StateId");
            RenameIndex(table: "dbo.LimitOnOrganization", name: "IX_StateMachineStateId", newName: "IX_StateId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.LimitOnOrganization", name: "IX_StateId", newName: "IX_StateMachineStateId");
            RenameColumn(table: "dbo.LimitOnOrganization", name: "StateId", newName: "StateMachineStateId");
        }
    }
}
