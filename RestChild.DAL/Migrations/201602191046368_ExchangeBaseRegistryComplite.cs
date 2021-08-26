namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ExchangeBaseRegistryComplite : DbMigration
    {
        public override void Up()
        {
			DropColumn("dbo.HotelBlock", "RowVersion");
			DropColumn("dbo.HotelBlockDate", "RowVersion");
			DropColumn("dbo.ServiceBlock", "RowVersion");
			DropColumn("dbo.ServiceBlockDate", "RowVersion");
			AddColumn("dbo.ExchangeBaseRegistry", "NotActual", c => c.Boolean(nullable: false));
            AddColumn("dbo.ExchangeBaseRegistry", "Success", c => c.Boolean(nullable: false));
            AddColumn("dbo.ExchangeBaseRegistry", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
			AddColumn("dbo.HotelBlock", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
			AddColumn("dbo.HotelBlockDate", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
			AddColumn("dbo.ServiceBlock", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.ServiceBlockDate", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }

        public override void Down()
        {
            AlterColumn("dbo.ServiceBlockDate", "RowVersion", c => c.Binary());
            AlterColumn("dbo.ServiceBlock", "RowVersion", c => c.Binary());
            AlterColumn("dbo.HotelBlockDate", "RowVersion", c => c.Binary());
            AlterColumn("dbo.HotelBlock", "RowVersion", c => c.Binary());
            DropColumn("dbo.ExchangeBaseRegistry", "RowVersion");
            DropColumn("dbo.ExchangeBaseRegistry", "Success");
            DropColumn("dbo.ExchangeBaseRegistry", "NotActual");
        }
    }
}
