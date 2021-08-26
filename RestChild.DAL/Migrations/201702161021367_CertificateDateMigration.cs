namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CertificateDateMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Request", "CertificateDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Request", "CertificateDate");
        }
    }
}
