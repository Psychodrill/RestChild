using System.Data.Entity.Migrations;

namespace RestChild.DAL.Migrations
{
	public partial class ExchangeBaseRegsitryControlFlagMigration : DbMigration
	{
		public override void Up()
		{
			AddColumn("dbo.ExchangeBaseRegistryType", "IsDeleted", c => c.Boolean(false));
			AddColumn("dbo.ExchangeBaseRegistryType", "SendMessage", c => c.Boolean(false));
			Sql("update dbo.ExchangeBaseRegistryType set SendMessage = 1 where Id <> 10062");
			Sql("update dbo.ExchangeBaseRegistryType set IsDeleted = 1 where Id = 10062");
		}

		public override void Down()
		{
			DropColumn("dbo.ExchangeBaseRegistryType", "SendMessage");
			DropColumn("dbo.ExchangeBaseRegistryType", "IsDeleted");
		}
	}
}
