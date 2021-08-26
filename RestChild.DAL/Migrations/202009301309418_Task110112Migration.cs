namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task110112Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestForPeriodOfRest", "VacationFrom", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.RequestForPeriodOfRest", "VacationTo", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequestForPeriodOfRest", "VacationTo");
            DropColumn("dbo.RequestForPeriodOfRest", "VacationFrom");
        }
    }
}
