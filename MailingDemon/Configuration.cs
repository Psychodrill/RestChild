namespace MailingDemon
{
	using System.Configuration;

	/// <summary>
	/// Represents access to ACRTracker Bisiness Logic configuration settings.
	/// </summary>
	public class Configuration
	{
		// SQL

		/// <summary>
		/// Initializes class properties.
		/// </summary>
		static Configuration()
		{
			// read connection string
		}

		public static string PortalResetUrl { get; private set; }

		public static bool SyncPreTrade { get; private set; }

		public static bool SyncRoadshows { get; private set; }

		public static string PortalNotifyUrl { get; private set; }

		/// <summary>
		/// Gets DSN connection string.
		/// </summary>
		public static string ConnectionString { get; private set; }

		/// <summary>
		/// Gets PORTAL connection string.
		/// </summary>
		public static string PortalConnectionString { get; private set; }

        /// <summary>
        /// Gets Images path
        /// </summary>
        public static string ImagesPath { get; private set; }
	}
}
