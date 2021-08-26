namespace RestChild.DAL.Migrations
{
	using System;
	using System.Data.Entity.Migrations;

	public partial class Lok2019TourMigration : DbMigration
	{
		public override void Up()
		{
			AddColumn("dbo.Tour", "ForInvalid", c => c.Boolean(nullable: false));
			Sql("delete from RequestStatusForMpgu");
		}

		public override void Down()
		{
			DropColumn("dbo.Tour", "ForInvalid");
		}
	}
}
