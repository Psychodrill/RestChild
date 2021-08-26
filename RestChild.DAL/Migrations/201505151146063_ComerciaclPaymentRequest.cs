namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComerciaclPaymentRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tour", "PaymentForAdult", c => c.Decimal(precision: 34, scale: 4));
            AddColumn("dbo.Tour", "PaymentForChild", c => c.Decimal(precision: 34, scale: 4));
            AddColumn("dbo.Tour", "ChildAgeFrom", c => c.Int());
            AddColumn("dbo.Tour", "ChildAgeTo", c => c.Int());
            AddColumn("dbo.Request", "Price", c => c.Decimal(precision: 34, scale: 4));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Request", "Price");
            DropColumn("dbo.Tour", "ChildAgeTo");
            DropColumn("dbo.Tour", "ChildAgeFrom");
            DropColumn("dbo.Tour", "PaymentForChild");
            DropColumn("dbo.Tour", "PaymentForAdult");
        }
    }
}
