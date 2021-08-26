namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TypeOfServiceMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TypeOfService",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeclineReasonTypeOfRest",
                c => new
                    {
                        DeclineReason_Id = c.Long(nullable: false),
                        TypeOfRest_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.DeclineReason_Id, t.TypeOfRest_Id })
                .ForeignKey("dbo.DeclineReason", t => t.DeclineReason_Id, cascadeDelete: true)
                .ForeignKey("dbo.TypeOfRest", t => t.TypeOfRest_Id, cascadeDelete: true)
                .Index(t => t.DeclineReason_Id)
                .Index(t => t.TypeOfRest_Id);
            
            AddColumn("dbo.AddonServices", "TypeOfServiceId", c => c.Long());
            CreateIndex("dbo.AddonServices", "TypeOfServiceId");
            AddForeignKey("dbo.AddonServices", "TypeOfServiceId", "dbo.TypeOfService", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AddonServices", "TypeOfServiceId", "dbo.TypeOfService");
            DropForeignKey("dbo.DeclineReasonTypeOfRest", "TypeOfRest_Id", "dbo.TypeOfRest");
            DropForeignKey("dbo.DeclineReasonTypeOfRest", "DeclineReason_Id", "dbo.DeclineReason");
            DropIndex("dbo.DeclineReasonTypeOfRest", new[] { "TypeOfRest_Id" });
            DropIndex("dbo.DeclineReasonTypeOfRest", new[] { "DeclineReason_Id" });
            DropIndex("dbo.AddonServices", new[] { "TypeOfServiceId" });
            DropColumn("dbo.AddonServices", "TypeOfServiceId");
            DropTable("dbo.DeclineReasonTypeOfRest");
            DropTable("dbo.TypeOfService");
        }
    }
}
