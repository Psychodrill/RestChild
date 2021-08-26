namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class TrainingCounselorsResultIsSuccessMirgation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TrainingCounselorsResult", "IsSuccess", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.TrainingCounselorsResult", "IsSuccess");
        }
    }
}
