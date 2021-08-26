namespace RestChild.Mobile.DAL
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.ModelConfiguration.Conventions;
	using System.Data.Entity.ModelConfiguration.Configuration;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;
	using RestChild.Mobile.Domain;

	public partial class Context : DbContext
	{
		static Context()
		{
			//Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Configuration>());
		}

		public Context()
			: base("RestChild.Mobile")
		{
		}

		public Context(string connString)
			: base(connString)
		{
		}

		//RestChild.Mobile.Domain.Account
		public DbSet<Account> Account { get; set; }
		//RestChild.Mobile.Domain.AccountExternal
		public DbSet<AccountExternal> AccountExternal { get; set; }
		//RestChild.Mobile.Domain.AccountHistoryLogin
		public DbSet<AccountHistoryLogin> AccountHistoryLogin { get; set; }
		//RestChild.Mobile.Domain.Bout
		public DbSet<Bout> Bout { get; set; }
		//RestChild.Mobile.Domain.BoutPersonal
		public DbSet<BoutPersonal> BoutPersonal { get; set; }
		//RestChild.Mobile.Domain.BoutTask
		public DbSet<BoutTask> BoutTask { get; set; }
		//RestChild.Mobile.Domain.Camp
		public DbSet<Camp> Camp { get; set; }
		//RestChild.Mobile.Domain.Camper
		public DbSet<Camper> Camper { get; set; }
		//RestChild.Mobile.Domain.CamperTask
		public DbSet<CamperTask> CamperTask { get; set; }
		//RestChild.Mobile.Domain.CampTask
		public DbSet<CampTask> CampTask { get; set; }
		//RestChild.Mobile.Domain.FileItem
		public DbSet<FileItem> FileItem { get; set; }
		//RestChild.Mobile.Domain.FileItemType
		public DbSet<FileItemType> FileItemType { get; set; }
		//RestChild.Mobile.Domain.FileItemUserCommentary
		public DbSet<FileItemUserCommentary> FileItemUserCommentary { get; set; }
		//RestChild.Mobile.Domain.Gift
		public DbSet<Gift> Gift { get; set; }
		//RestChild.Mobile.Domain.GiftParameter
		public DbSet<GiftParameter> GiftParameter { get; set; }
		//RestChild.Mobile.Domain.GiftReserved
		public DbSet<GiftReserved> GiftReserved { get; set; }
		//RestChild.Mobile.Domain.GroupedTime
		public DbSet<GroupedTime> GroupedTime { get; set; }
		//RestChild.Mobile.Domain.History
		public DbSet<History> History { get; set; }
		//RestChild.Mobile.Domain.Link
		public DbSet<Link> Link { get; set; }
		//RestChild.Mobile.Domain.Notification
		public DbSet<Notification> Notification { get; set; }
		//RestChild.Mobile.Domain.Party
		public DbSet<Party> Party { get; set; }
		//RestChild.Mobile.Domain.Personal
		public DbSet<Personal> Personal { get; set; }
		//RestChild.Mobile.Domain.PersonalType
		public DbSet<PersonalType> PersonalType { get; set; }
		//RestChild.Mobile.Domain.SendEmailAndSms
		public DbSet<SendEmailAndSms> SendEmailAndSms { get; set; }
		//RestChild.Mobile.Domain.State
		public DbSet<State> State { get; set; }
		//RestChild.Mobile.Domain.StateMachine
		public DbSet<StateMachine> StateMachine { get; set; }
		//RestChild.Mobile.Domain.Task
		public DbSet<Task> Task { get; set; }
		//RestChild.Mobile.Domain.TaskPeriod
		public DbSet<TaskPeriod> TaskPeriod { get; set; }
		//RestChild.Mobile.Domain.UserCommentary
		public DbSet<UserCommentary> UserCommentary { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Properties<Decimal>().Configure(c => c.HasPrecision(32,4));
			modelBuilder.Properties<Decimal>().Where(e=>e.Name  == "Latitude" || e.Name == "Longitude").Configure(c => c.HasPrecision(32, 10));
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
			modelBuilder.Properties().Where(e => e.Name == "RowVersion").Configure(c => c.IsRowVersion());
		}
	}
}
