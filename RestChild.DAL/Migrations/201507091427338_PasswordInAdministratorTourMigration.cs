namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PasswordInAdministratorTourMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdministratorTour", "Password", c => c.String(maxLength: 1000));
            AddColumn("dbo.AdministratorTour", "Salt", c => c.String(maxLength: 1000));
            AddColumn("dbo.Counselors", "Password", c => c.String(maxLength: 1000));
            AddColumn("dbo.Counselors", "Salt", c => c.String(maxLength: 1000));
            AddColumn("dbo.Counselors", "LinkedAccountId", c => c.Long());
            CreateIndex("dbo.Counselors", "LinkedAccountId");
            AddForeignKey("dbo.Counselors", "LinkedAccountId", "dbo.Account", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Counselors", "LinkedAccountId", "dbo.Account");
            DropIndex("dbo.Counselors", new[] { "LinkedAccountId" });
            DropColumn("dbo.Counselors", "LinkedAccountId");
            DropColumn("dbo.Counselors", "Salt");
            DropColumn("dbo.Counselors", "Password");
            DropColumn("dbo.AdministratorTour", "Salt");
            DropColumn("dbo.AdministratorTour", "Password");
        }
    }
}
