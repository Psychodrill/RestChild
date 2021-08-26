namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AccessRightGroupMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccessRight", "GroupCode", c => c.String(maxLength: 400));
        }

        public override void Down()
        {
            DropColumn("dbo.AccessRight", "GroupCode");
        }
    }
}
