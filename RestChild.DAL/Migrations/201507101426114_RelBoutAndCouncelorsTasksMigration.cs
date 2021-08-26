namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelBoutAndCouncelorsTasksMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CounselorTask", "BoutId", c => c.Long());
            CreateIndex("dbo.CounselorTask", "BoutId");
            AddForeignKey("dbo.CounselorTask", "BoutId", "dbo.Bout", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CounselorTask", "BoutId", "dbo.Bout");
            DropIndex("dbo.CounselorTask", new[] { "BoutId" });
            DropColumn("dbo.CounselorTask", "BoutId");
        }
    }
}
