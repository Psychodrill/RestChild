namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BenefitRequestDateAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Child", "BenefitApproveRequestDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
			Sql("update dbo.Child set BenefitApproveRequestDate = getdate()");
		}
        
        public override void Down()
        {
            DropColumn("dbo.Child", "BenefitApproveRequestDate");
        }
    }
}
