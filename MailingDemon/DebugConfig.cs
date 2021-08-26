using MailingDemon.Tasks;

namespace MailingDemon
{
	/// <summary>
	/// Конфигурация отладочных ключей
	/// </summary>
	public class DebugConfig : BaseConfig
	{
		private int _sleepTime = 10000;

		/// <summary>
		/// Время паузы при старте сервиса. Используется только в конфигурации Debug
		/// </summary>
		public int SleepTime
		{
			get { return _sleepTime; }
			set { _sleepTime = value; }
		}

		/// <summary>
		/// Вызывать ли Debug.Break при старте. Используется только в Debug-режиме.
		/// При установке в true параметр SleepTime игнорируется.
		/// </summary>
		public bool DebugBreak { get; set; }

		/// <summary>
		/// Включать ли вывод дебаг информации
		/// </summary>
		public bool DebugLog { get; set; }
	}
}