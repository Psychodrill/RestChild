namespace MailingDemon.Tasks
{
	/// <summary>
	///     Интерфейс управления задачей
	/// </summary>
	public interface ITaskController
	{
		/// <summary>
		///     Приостановить выполнение задачи
		/// </summary>
		void Pause();

		/// <summary>
		///     Возобновить выполнение задачи
		/// </summary>
		void Resume();

		/// <summary>
		///     Запустить задачу
		/// </summary>
		void Start();

		/// <summary>
		///     Остановить задачу
		/// </summary>
		void Stop();
	}
}