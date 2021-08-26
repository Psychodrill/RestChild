namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDbMigration : DbMigration
    {
        public override void Up()
        {
          AddColumn("dbo.MGTWorkingDayWindow", "WindowNumber", c => c.Int());
        }

      public override void Down()
      { 
         DropColumn("dbo.MGTWorkingDayWindow", "WindowNumber");
      }
   }
}
