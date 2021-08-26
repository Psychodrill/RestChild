namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YearsAndRequestToPlace : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RequestCurrentPeriod", "CreateUserId", "dbo.Account");
            DropForeignKey("dbo.Request", "RequestCurrentPeriodId", "dbo.RequestCurrentPeriod");
            DropIndex("dbo.Request", new[] { "RequestCurrentPeriodId" });
            DropIndex("dbo.RequestCurrentPeriod", new[] { "CreateUserId" });
            AddColumn("dbo.YearOfRest", "IsClosed", c => c.Boolean(nullable: false));
            AddColumn("dbo.YearOfRest", "DateFirstStage", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.YearOfRest", "DateSecondStage", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Request", "YearOfRestId", c => c.Long());
            CreateIndex("dbo.Request", "YearOfRestId");
            AddForeignKey("dbo.Request", "YearOfRestId", "dbo.YearOfRest", "Id");
            DropColumn("dbo.Request", "RequestCurrentPeriodId");
            DropTable("dbo.RequestCurrentPeriod");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RequestCurrentPeriod",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1000),
                        IsClosed = c.Boolean(nullable: false),
                        DateFirstStage = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateSecondStage = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreateUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Request", "RequestCurrentPeriodId", c => c.Long());
            DropForeignKey("dbo.Request", "YearOfRestId", "dbo.YearOfRest");
            DropIndex("dbo.Request", new[] { "YearOfRestId" });
            DropColumn("dbo.Request", "YearOfRestId");
            DropColumn("dbo.YearOfRest", "DateSecondStage");
            DropColumn("dbo.YearOfRest", "DateFirstStage");
            DropColumn("dbo.YearOfRest", "IsClosed");
            CreateIndex("dbo.RequestCurrentPeriod", "CreateUserId");
            CreateIndex("dbo.Request", "RequestCurrentPeriodId");
            AddForeignKey("dbo.Request", "RequestCurrentPeriodId", "dbo.RequestCurrentPeriod", "Id");
            AddForeignKey("dbo.RequestCurrentPeriod", "CreateUserId", "dbo.Account", "Id");
        }
    }
}
