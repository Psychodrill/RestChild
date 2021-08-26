namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentsFileds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicant", "PaymentFileUrl", c => c.String(maxLength: 1000));
            AddColumn("dbo.Applicant", "PaymentFileTitle", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applicant", "PaymentFileTitle");
            DropColumn("dbo.Applicant", "PaymentFileUrl");
        }
    }
}
