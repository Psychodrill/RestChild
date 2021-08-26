namespace MailingDemon.Tasks
{
	using System.Xml.Serialization;

	/// <summary>
	///     таск.
	/// </summary>
	[Task]
	public class TaskName : BaseTask
	{
		[XmlElement("config")]
		public TaskNameConfig Config { get; set; }

		/// <summary>
		///     выполнение задания.
		/// </summary>
		protected override void Execute()
		{
			// что то делаем
			Logger.Info("Что то делаем.");
			Logger.InfoFormat("Параметр {0}", Config.Param1);
		}
	}
}