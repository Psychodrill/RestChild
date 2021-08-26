namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChildAttendantDates : DbMigration
    {
        public override void Up()
        {
	        var ticksPerDay = TimeSpan.TicksPerDay;
			Sql(String.Format("UPDATE Child SET IntervalStart=DATEDIFF(DAY, DATEFROMPARTS(1, 1, 1), DATEFROMPARTS(TimeOfRest.Year, TimeOfRest.Month, TimeOfRest.DayOfMonth))*{0}, IntervalEnd = DATEDIFF(DAY, DATEFROMPARTS(1, 1, 1), DATEADD(day, TimeOfRest.PeriodLength+1, DATEFROMPARTS(TimeOfRest.Year, TimeOfRest.Month, TimeOfRest.DayOfMonth)))*{0}" +
								"FROM Child " + "LEFT JOIN Request ON Request.Id = Child.RequestId "+
								"LEFT JOIN TimeOfRest ON TimeOfRest.Id = Request.TimeOfRestId",
				ticksPerDay));

			Sql(String.Format("UPDATE Applicant SET IntervalStart=DATEDIFF(DAY, DATEFROMPARTS(1, 1, 1), DATEFROMPARTS(TimeOfRest.Year, TimeOfRest.Month, TimeOfRest.DayOfMonth))*{0}, IntervalEnd = DATEDIFF(DAY, DATEFROMPARTS(1, 1, 1), DATEADD(day, TimeOfRest.PeriodLength+1, DATEFROMPARTS(TimeOfRest.Year, TimeOfRest.Month, TimeOfRest.DayOfMonth)))*{0}" +
				  "FROM Applicant " +
				  "LEFT JOIN Request ON Request.Id = Applicant.RequestId " +
				  "LEFT JOIN TimeOfRest ON TimeOfRest.Id = Request.TimeOfRestId " +
				  "WHERE Applicant.RequestId is not null", 
				  ticksPerDay));

			Sql(String.Format("UPDATE Applicant SET IntervalStart=DATEDIFF(DAY, DATEFROMPARTS(1, 1, 1), DATEFROMPARTS(TimeOfRest.Year, TimeOfRest.Month, TimeOfRest.DayOfMonth))*{0}, IntervalEnd = DATEDIFF(DAY, DATEFROMPARTS(1, 1, 1), DATEADD(day, TimeOfRest.PeriodLength+1, DATEFROMPARTS(TimeOfRest.Year, TimeOfRest.Month, TimeOfRest.DayOfMonth)))*{0}" +
				  "FROM Applicant " +
				  "LEFT JOIN Request ON Request.ApplicantId = Applicant.Id " +
				  "LEFT JOIN TimeOfRest ON TimeOfRest.Id = Request.TimeOfRestId " +
				  "WHERE Applicant.RequestId is null", 
				  ticksPerDay));
        }
        
        public override void Down()
        {
        }
    }
}
