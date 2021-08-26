namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LonLanAndOtherMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdministratorTour", "StateId", c => c.Long());
            AddColumn("dbo.Hotels", "Latitude", c => c.Decimal(precision: 32, scale: 10));
            AddColumn("dbo.Hotels", "Longitude", c => c.Decimal(precision: 32, scale: 10));
            AddColumn("dbo.Address", "Latitude", c => c.Decimal(precision: 32, scale: 10));
            AddColumn("dbo.Address", "Longitude", c => c.Decimal(precision: 32, scale: 10));
            AddColumn("dbo.ListOfChilds", "CertificateNumber", c => c.String(maxLength: 1000));
            AddColumn("dbo.Payment", "PaymentFileUrl", c => c.String(maxLength: 1000));
            AddColumn("dbo.Payment", "PaymentFileTitle", c => c.String(maxLength: 1000));
            CreateIndex("dbo.AdministratorTour", "StateId");
            AddForeignKey("dbo.AdministratorTour", "StateId", "dbo.StateMachineState", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdministratorTour", "StateId", "dbo.StateMachineState");
            DropIndex("dbo.AdministratorTour", new[] { "StateId" });
            DropColumn("dbo.Payment", "PaymentFileTitle");
            DropColumn("dbo.Payment", "PaymentFileUrl");
            DropColumn("dbo.ListOfChilds", "CertificateNumber");
            DropColumn("dbo.Address", "Longitude");
            DropColumn("dbo.Address", "Latitude");
            DropColumn("dbo.Hotels", "Longitude");
            DropColumn("dbo.Hotels", "Latitude");
            DropColumn("dbo.AdministratorTour", "StateId");
        }
    }
}
