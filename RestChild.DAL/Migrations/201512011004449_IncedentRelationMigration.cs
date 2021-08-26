namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncedentRelationMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contract", "OnService", c => c.Boolean(nullable: false));
            AddColumn("dbo.CounselorTest", "IsFinalTest", c => c.Boolean(nullable: false));
            AddColumn("dbo.CategoryIncident", "ParentId", c => c.Long());
            CreateIndex("dbo.CategoryIncident", "ParentId");
            AddForeignKey("dbo.CategoryIncident", "ParentId", "dbo.CategoryIncident", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoryIncident", "ParentId", "dbo.CategoryIncident");
            DropIndex("dbo.CategoryIncident", new[] { "ParentId" });
            DropColumn("dbo.CategoryIncident", "ParentId");
            DropColumn("dbo.CounselorTest", "IsFinalTest");
            DropColumn("dbo.Contract", "OnService");
        }
    }
}
