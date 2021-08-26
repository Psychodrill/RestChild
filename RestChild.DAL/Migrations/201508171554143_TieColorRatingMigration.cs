namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class TieColorRatingMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TieColor", "Raiting", c => c.Decimal(precision: 38, scale: 4));
            AlterColumn("dbo.TieColor", "Name", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.TieColor", "IsActive", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.TieColor", "IsActive", c => c.String(nullable: false));
            AlterColumn("dbo.TieColor", "Name", c => c.String(nullable: false));
            DropColumn("dbo.TieColor", "Raiting");
        }
    }
}
