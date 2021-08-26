namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountErrorInTestMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CounselorTest", "CountErrorInTest", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CounselorTest", "CountErrorInTest");
        }
    }
}
