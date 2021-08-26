namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CertificateNumberMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Request", "CertificateNumber", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Request", "CertificateNumber");
        }
    }
}
