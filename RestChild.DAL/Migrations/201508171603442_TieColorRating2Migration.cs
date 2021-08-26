namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class TieColorRating2Migration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Counselors", "Rating", c => c.Decimal(precision: 38, scale: 4));
        }

        public override void Down()
        {
            AlterColumn("dbo.Counselors", "Rating", c => c.Single());
        }
    }
}
