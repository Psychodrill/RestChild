namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrpphanBlockPupilDrugDoseFixMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PupilDose", "Dose", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PupilDose", "Dose", c => c.String(nullable: false, maxLength: 1000));
        }
    }
}
