namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CounselorTestDeletedMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TrainingCounselorsGroupTest", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TrainingCounselorsGroupTest", "IsDeleted");
        }
    }
}
