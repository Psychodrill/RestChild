namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountAttendantAndCountApplicantInRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Request", "CountPlace", c => c.Int(nullable: false));
            AddColumn("dbo.Request", "CountAttendants", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Request", "CountAttendants");
            DropColumn("dbo.Request", "CountPlace");
        }
    }
}
