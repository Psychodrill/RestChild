namespace MailingDemon.Tasks
{
	public class ExchangeBaseRegistryConfig : BaseConfig
	{
		/// <summary>
		///     интервал
		/// </summary>
		public int Sleep { get; set; }

		/// <summary>
		///     вид документа
		/// </summary>
		public long DocType { get; set; }

		/// <summary>
		///     количество на исполнение
		/// </summary>
		public int CountOnExecution { get; set; }
	}
}
