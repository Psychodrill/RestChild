namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProcessErrorMesages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExchangeUTS", "ErrorText", c => c.String());
            AddColumn("dbo.ExchangeUTS", "ErrorDescription", c => c.String());

			RenameColumn("dbo.Child", "IsIncludeInInteragencySecondary", "IsIncludeInInteragencySecondary2");
			AddColumn("dbo.Child", "IsIncludeInInteragencySecondary", c => c.Boolean(nullable: false));
			Sql("Update dbo.Child set IsIncludeInInteragencySecondary = isnull(IsIncludeInInteragencySecondary2,0)");
			DropColumn("dbo.Child", "IsIncludeInInteragencySecondary2");			

			RenameColumn("dbo.Child", "IsInvalid", "IsInvalid2");
			AddColumn("dbo.Child", "IsInvalid", c => c.Boolean(nullable: false));
			Sql("Update dbo.Child set IsInvalid = isnull(IsInvalid2,0)");
			DropColumn("dbo.Child", "IsInvalid2");
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Child", "IsInvalid", c => c.Boolean());
            AlterColumn("dbo.Child", "IsIncludeInInteragencySecondary", c => c.Boolean());
            DropColumn("dbo.ExchangeUTS", "ErrorDescription");
            DropColumn("dbo.ExchangeUTS", "ErrorText");
        }
    }
}
