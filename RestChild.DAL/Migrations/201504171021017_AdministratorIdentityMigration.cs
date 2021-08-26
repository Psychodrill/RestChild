namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdministratorIdentityMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AdministratorTourBout", "AdministratorTour_Id", "dbo.AdministratorTour");
            DropPrimaryKey("dbo.AdministratorTour");
			DropColumn("dbo.AdministratorTour", "Id");
			AddColumn("dbo.AdministratorTour", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.AdministratorTour", "Id");
            AddForeignKey("dbo.AdministratorTourBout", "AdministratorTour_Id", "dbo.AdministratorTour", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdministratorTourBout", "AdministratorTour_Id", "dbo.AdministratorTour");
            DropPrimaryKey("dbo.AdministratorTour");
            AlterColumn("dbo.AdministratorTour", "Id", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.AdministratorTour", "Id");
            AddForeignKey("dbo.AdministratorTourBout", "AdministratorTour_Id", "dbo.AdministratorTour", "Id", cascadeDelete: true);
        }
    }
}
