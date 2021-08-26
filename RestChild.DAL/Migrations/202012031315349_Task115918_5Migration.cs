namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task115918_5Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SendEmailAndSms", "DateToSend", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SendEmailAndSms", "DateToSend");
        }
    }
}
