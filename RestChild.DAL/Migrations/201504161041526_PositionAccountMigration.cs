namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PositionAccountMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Account", "Position", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Account", "Position");
        }
    }
}
