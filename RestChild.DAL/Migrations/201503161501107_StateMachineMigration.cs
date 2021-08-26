namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StateMachineMigration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.LimitStatus", newName: "StateMachine");
            DropForeignKey("dbo.LimitOnVedomstvo", "LimitStatusId", "dbo.LimitStatus");
            DropForeignKey("dbo.YearOfRest", "LimitStatusYearOfRestId", "dbo.LimitStatusYearOfRest");
            DropForeignKey("dbo.LimitOnOrganization", "LimitStatusOrganizationId", "dbo.LimitStatusOrganization");
            DropForeignKey("dbo.ListOfChilds", "StatusListChildsId", "dbo.StatusListChilds");
            DropIndex("dbo.LimitOnOrganization", new[] { "LimitStatusOrganizationId" });
            DropIndex("dbo.LimitOnVedomstvo", new[] { "LimitStatusId" });
            DropIndex("dbo.YearOfRest", new[] { "LimitStatusYearOfRestId" });
            DropIndex("dbo.ListOfChilds", new[] { "StatusListChildsId" });
            CreateTable(
                "dbo.StateMachineState",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 1000),
                        StateMachineId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StateMachine", t => t.StateMachineId)
                .Index(t => t.StateMachineId);
            
            CreateTable(
                "dbo.StateMachineAction",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        ActionName = c.String(nullable: false, maxLength: 1000),
                        Description = c.String(maxLength: 1000),
                        ActionCode = c.String(nullable: false, maxLength: 1000),
                        IsSystemAction = c.Boolean(nullable: false),
                        StateMachineId = c.Long(),
                        ToStateId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StateMachine", t => t.StateMachineId)
                .ForeignKey("dbo.StateMachineState", t => t.ToStateId)
                .Index(t => t.StateMachineId)
                .Index(t => t.ToStateId);
            
            CreateTable(
                "dbo.StateMachineFromStatus",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        ServiceCode = c.String(maxLength: 1000),
                        RightCode = c.String(maxLength: 1000),
                        StateMachineActionId = c.Long(),
                        StateMachineId = c.Long(),
                        FromStateId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StateMachineState", t => t.FromStateId)
                .ForeignKey("dbo.StateMachine", t => t.StateMachineId)
                .ForeignKey("dbo.StateMachineAction", t => t.StateMachineActionId)
                .Index(t => t.StateMachineActionId)
                .Index(t => t.StateMachineId)
                .Index(t => t.FromStateId);
            
            AddColumn("dbo.LimitOnOrganization", "StateMachineStateId", c => c.Long());
            AddColumn("dbo.LimitOnVedomstvo", "StateId", c => c.Long());
            AddColumn("dbo.YearOfRest", "StateId", c => c.Long());
            AddColumn("dbo.ListOfChilds", "StateId", c => c.Long());
            CreateIndex("dbo.LimitOnOrganization", "StateMachineStateId");
            CreateIndex("dbo.LimitOnVedomstvo", "StateId");
            CreateIndex("dbo.YearOfRest", "StateId");
            CreateIndex("dbo.ListOfChilds", "StateId");
            AddForeignKey("dbo.LimitOnVedomstvo", "StateId", "dbo.StateMachineState", "Id");
            AddForeignKey("dbo.YearOfRest", "StateId", "dbo.StateMachineState", "Id");
            AddForeignKey("dbo.LimitOnOrganization", "StateMachineStateId", "dbo.StateMachineState", "Id");
            AddForeignKey("dbo.ListOfChilds", "StateId", "dbo.StateMachineState", "Id");
            DropColumn("dbo.LimitOnOrganization", "LimitStatusOrganizationId");
            DropColumn("dbo.LimitOnVedomstvo", "LimitStatusId");
            DropColumn("dbo.YearOfRest", "LimitStatusYearOfRestId");
            DropColumn("dbo.ListOfChilds", "StatusListChildsId");
            DropTable("dbo.LimitStatusYearOfRest");
            DropTable("dbo.LimitStatusOrganization");
            DropTable("dbo.StatusListChilds");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StatusListChilds",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LimitStatusOrganization",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LimitStatusYearOfRest",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ListOfChilds", "StatusListChildsId", c => c.Long());
            AddColumn("dbo.YearOfRest", "LimitStatusYearOfRestId", c => c.Long());
            AddColumn("dbo.LimitOnVedomstvo", "LimitStatusId", c => c.Long());
            AddColumn("dbo.LimitOnOrganization", "LimitStatusOrganizationId", c => c.Long());
            DropForeignKey("dbo.ListOfChilds", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.LimitOnOrganization", "StateMachineStateId", "dbo.StateMachineState");
            DropForeignKey("dbo.YearOfRest", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.LimitOnVedomstvo", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.StateMachineState", "StateMachineId", "dbo.StateMachine");
            DropForeignKey("dbo.StateMachineAction", "ToStateId", "dbo.StateMachineState");
            DropForeignKey("dbo.StateMachineAction", "StateMachineId", "dbo.StateMachine");
            DropForeignKey("dbo.StateMachineFromStatus", "StateMachineActionId", "dbo.StateMachineAction");
            DropForeignKey("dbo.StateMachineFromStatus", "StateMachineId", "dbo.StateMachine");
            DropForeignKey("dbo.StateMachineFromStatus", "FromStateId", "dbo.StateMachineState");
            DropIndex("dbo.ListOfChilds", new[] { "StateId" });
            DropIndex("dbo.YearOfRest", new[] { "StateId" });
            DropIndex("dbo.StateMachineFromStatus", new[] { "FromStateId" });
            DropIndex("dbo.StateMachineFromStatus", new[] { "StateMachineId" });
            DropIndex("dbo.StateMachineFromStatus", new[] { "StateMachineActionId" });
            DropIndex("dbo.StateMachineAction", new[] { "ToStateId" });
            DropIndex("dbo.StateMachineAction", new[] { "StateMachineId" });
            DropIndex("dbo.StateMachineState", new[] { "StateMachineId" });
            DropIndex("dbo.LimitOnVedomstvo", new[] { "StateId" });
            DropIndex("dbo.LimitOnOrganization", new[] { "StateMachineStateId" });
            DropColumn("dbo.ListOfChilds", "StateId");
            DropColumn("dbo.YearOfRest", "StateId");
            DropColumn("dbo.LimitOnVedomstvo", "StateId");
            DropColumn("dbo.LimitOnOrganization", "StateMachineStateId");
            DropTable("dbo.StateMachineFromStatus");
            DropTable("dbo.StateMachineAction");
            DropTable("dbo.StateMachineState");
            CreateIndex("dbo.ListOfChilds", "StatusListChildsId");
            CreateIndex("dbo.YearOfRest", "LimitStatusYearOfRestId");
            CreateIndex("dbo.LimitOnVedomstvo", "LimitStatusId");
            CreateIndex("dbo.LimitOnOrganization", "LimitStatusOrganizationId");
            AddForeignKey("dbo.ListOfChilds", "StatusListChildsId", "dbo.StatusListChilds", "Id");
            AddForeignKey("dbo.LimitOnOrganization", "LimitStatusOrganizationId", "dbo.LimitStatusOrganization", "Id");
            AddForeignKey("dbo.YearOfRest", "LimitStatusYearOfRestId", "dbo.LimitStatusYearOfRest", "Id");
            AddForeignKey("dbo.LimitOnVedomstvo", "LimitStatusId", "dbo.LimitStatus", "Id");
            RenameTable(name: "dbo.StateMachine", newName: "LimitStatus");
        }
    }
}
