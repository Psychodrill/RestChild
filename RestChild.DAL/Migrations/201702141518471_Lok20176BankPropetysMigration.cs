namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lok20176BankPropetysMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Address", "Vladenie", c => c.String(maxLength: 1000));
            AddColumn("dbo.Request", "BankKpp", c => c.String(maxLength: 1000));
            AddColumn("dbo.Request", "BankCardNumber", c => c.String(maxLength: 1000));
            AddColumn("dbo.Request", "StatusApplicant", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Request", "StatusApplicant");
            DropColumn("dbo.Request", "BankCardNumber");
            DropColumn("dbo.Request", "BankKpp");
            DropColumn("dbo.Address", "Vladenie");
        }
    }
}
