namespace MailingDemon.Tasks
{
	using System;
	using System.Collections.Generic;
	using System.Data.SqlClient;
	using System.IO;
	using System.Security.Cryptography;
	using System.Text;
	using System.Threading;
	using System.Xml;
	using System.Xml.Serialization;

	using log4net;

	using MailingDemon.Scheduler.Triggers;

	/// <summary>
	///     Summary description for BaseTask.
	/// </summary>
	public abstract class BaseTask : ITaskController
	{
		// булева переменная "нужно приостановить работу"
		private static char _sCharPlusSign = '+';

		private static char _sCharSlash = '/';

		protected string DescriptionInternal;

		protected ILog Logger;

		private readonly ManualResetEvent _needToResume;

		// булева переменная "нужно корректно остановиться". пока не используется
		private readonly ManualResetEvent _needToStopGraceful;

		/// <summary>
		///     Этот флаг используется для синхронизации управления. Если функция Execute
		///     выставляет этот флаг, значит, она готова приостановить свою работу
		///     и сделает немедленно
		/// </summary>
		private readonly ManualResetEvent _readyToPause;

		private readonly Thread _worker;

		private bool _active = true;

		private bool _mustRetry;

		private TimeSpan _retryDelay = TimeSpan.MinValue;

		private BaseTrigger[] _triggers;

		/// <summary>
		///     Ожидабельные хэндлы от триггеров
		/// </summary>
		private WaitHandle[] _waitable;

		/// <summary>
		///     Создать пустую задачу, не привязанную к расписаниям
		/// </summary>
		protected BaseTask()
		{
			_worker = new Thread(ThreadFunc);
			_needToResume = new ManualResetEvent(true);
			_needToStopGraceful = new ManualResetEvent(false);
			_readyToPause = new ManualResetEvent(false);

			Logger = LogManager.GetLogger(GetType());
		}

		/// <summary>
		///     Gets or sets the plus sign character.
		///     Default is '+'.
		/// </summary>
		public static char CharPlusSign
		{
			get
			{
				return _sCharPlusSign;
			}

			set
			{
				_sCharPlusSign = value;
			}
		}

		/// <summary>
		///     Gets or sets the slash character.
		///     Default is '/'.
		/// </summary>
		public static char CharSlash
		{
			get
			{
				return _sCharSlash;
			}

			set
			{
				_sCharSlash = value;
			}
		}

		[XmlAttribute("active")]
		public bool Active
		{
			get
			{
				return _active;
			}

			set
			{
				_active = value;
			}
		}

		[XmlAttribute("description")]
		public string Description
		{
			get
			{
				return DescriptionInternal;
			}

			set
			{
				DescriptionInternal = value;
			}
		}

		/// <summary>
		///     Требуется ли повторять задачу в случае ошибки
		/// </summary>
		[XmlIgnore]
		public bool MustRetry
		{
			get
			{
				return _mustRetry;
			}
		}

		/// <summary>
		///     Событие, которое срабатывает при приостановке потока
		/// </summary>
		public WaitHandle PausingEvent
		{
			get
			{
				return _readyToPause;
			}
		}

		/// <summary>
		///     Пауза перед повторением задачи в случае ошибки
		/// </summary>
		[XmlIgnore]
		public TimeSpan RetryDelay
		{
			get
			{
				return _retryDelay;
			}

			set
			{
				this._mustRetry = true;
				this._retryDelay = value;
			}
		}

		[XmlAttribute("retryDelay")]
		public string RetryDelayString
		{
			get
			{
				return _mustRetry ? RetryDelay.ToString() : null;
			}

			set
			{
				this.RetryDelay = TimeSpan.Parse(value);
			}
		}

		[XmlArray("triggers")]
		public BaseTrigger[] Triggers
		{
			get
			{
				return _triggers;
			}

			set
			{
				_triggers = value;
			}
		}

		public static byte[] FromBase64(string s)
		{
			var length = s == null ? 0 : s.Length;
			if (length == 0)
			{
				return new byte[0];
			}

			var padding = 0;
			if (s != null && (length > 2 && s[length - 2] == '='))
			{
				padding = 2;
			}
			else if (s != null && (length > 1 && s[length - 1] == '='))
			{
				padding = 1;
			}

			var blocks = (length - 1) / 4 + 1;
			var bytes = blocks * 3;

			var data = new byte[bytes - padding];

			for (var i = 0; i < blocks; i++)
			{
				var finalBlock = i == blocks - 1;
				var pad2 = false;
				var pad1 = false;
				if (finalBlock)
				{
					pad2 = padding == 2;
					pad1 = padding > 0;
				}

				var index = i * 4;
				if (s != null)
				{
					var temp1 = CharToSixBit(s[index]);
					var temp2 = CharToSixBit(s[index + 1]);
					var temp3 = CharToSixBit(s[index + 2]);
					var temp4 = CharToSixBit(s[index + 3]);

					var b = (byte)(temp1 << 2);
					var b1 = (byte)((temp2 & 0x30) >> 4);
					b1 += b;

					b = (byte)((temp2 & 0x0F) << 4);
					var b2 = (byte)((temp3 & 0x3C) >> 2);
					b2 += b;

					b = (byte)((temp3 & 0x03) << 6);
					var b3 = temp4;
					b3 += b;

					index = i * 3;
					data[index] = b1;
					if (!pad2)
					{
						data[index + 1] = b2;
					}

					if (!pad1)
					{
						data[index + 2] = b3;
					}
				}
			}

			return data;
		}

		/// <summary>
		///     Аналог FromBase64String
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static byte[] FromBase64StringWithChunk(string value)
		{
			value = value.Replace("\r\n", string.Empty);
			var myInputBytes = Encoding.ASCII.GetBytes(value);
			var myTransform = new FromBase64Transform(FromBase64TransformMode.DoNotIgnoreWhiteSpaces);
			var myOutputBytes = new byte[myTransform.OutputBlockSize];
			var i = 0;
			var finBytes = new List<byte>();

			while (myInputBytes.Length - i > 4)
			{
				myTransform.TransformBlock(myInputBytes, i, 4, myOutputBytes, 0);
				i += 4;
				finBytes.AddRange(myOutputBytes);
			}

			myOutputBytes = myTransform.TransformFinalBlock(myInputBytes, i, myInputBytes.Length - i);
			myTransform.Clear();
			finBytes.AddRange(myOutputBytes);
			return finBytes.ToArray();
		}

		/// <summary>
		///     конвертирование в Base64 строку.
		/// </summary>
		/// <param name="data">данные.</param>
		/// <param name="stream">поток.</param>
		/// <returns>строка.</returns>
		public static void ToBase64(byte[] data, Stream stream)
		{
			var length = data == null ? 0 : data.Length;
			if (length == 0)
			{
				return;
			}

			var padding = length % 3;
			if (padding > 0)
			{
				padding = 3 - padding;
			}

			var blocks = (length - 1) / 3 + 1;

			var s = new char[4];

			for (var i = 0; i < blocks; i++)
			{
				var finalBlock = i == blocks - 1;
				var pad2 = false;
				var pad1 = false;
				if (finalBlock)
				{
					pad2 = padding == 2;
					pad1 = padding > 0;
				}

				var index = i * 3;
				if (data != null)
				{
					var b1 = data[index];
					var b2 = pad2 ? (byte)0 : data[index + 1];
					var b3 = pad1 ? (byte)0 : data[index + 2];

					var temp1 = (byte)((b1 & 0xFC) >> 2);

					var temp = (byte)((b1 & 0x03) << 4);
					var temp2 = (byte)((b2 & 0xF0) >> 4);
					temp2 += temp;

					temp = (byte)((b2 & 0x0F) << 2);
					var temp3 = (byte)((b3 & 0xC0) >> 6);
					temp3 += temp;

					var temp4 = (byte)(b3 & 0x3F);

					s[0] = SixBitToChar(temp1);
					s[1] = SixBitToChar(temp2);
					s[2] = pad2 ? '=' : SixBitToChar(temp3);
					s[3] = pad1 ? '=' : SixBitToChar(temp4);
				}

				var buffer = Encoding.UTF8.GetBytes(new string(s));

				stream.Write(buffer, 0, buffer.Length);
				stream.Flush();
			}
		}

		/// <summary>
		///     конвертирование в Base64 строку.
		/// </summary>
		/// <param name="data">данные.</param>
		/// <returns>строка.</returns>
		public static string ToBase64(byte[] data)
		{
			var length = data == null ? 0 : data.Length;
			if (length == 0)
			{
				return string.Empty;
			}

			var padding = length % 3;
			if (padding > 0)
			{
				padding = 3 - padding;
			}

			var blocks = (length - 1) / 3 + 1;

			var s = new char[blocks * 4];

			for (var i = 0; i < blocks; i++)
			{
				var finalBlock = i == blocks - 1;
				var pad2 = false;
				var pad1 = false;
				if (finalBlock)
				{
					pad2 = padding == 2;
					pad1 = padding > 0;
				}

				var index = i * 3;
				if (data != null)
				{
					var b1 = data[index];
					var b2 = pad2 ? (byte)0 : data[index + 1];
					var b3 = pad1 ? (byte)0 : data[index + 2];

					var temp1 = (byte)((b1 & 0xFC) >> 2);

					var temp = (byte)((b1 & 0x03) << 4);
					var temp2 = (byte)((b2 & 0xF0) >> 4);
					temp2 += temp;

					temp = (byte)((b2 & 0x0F) << 2);
					var temp3 = (byte)((b3 & 0xC0) >> 6);
					temp3 += temp;

					var temp4 = (byte)(b3 & 0x3F);

					index = i * 4;
					s[index] = SixBitToChar(temp1);
					s[index + 1] = SixBitToChar(temp2);
					s[index + 2] = pad2 ? '=' : SixBitToChar(temp3);
					s[index + 3] = pad1 ? '=' : SixBitToChar(temp4);
				}
			}

			return new string(s);
		}

		public virtual void OnDeserialize()
		{
			_waitable = new WaitHandle[_triggers.Length];
			var i = 0;
			foreach (var t in Triggers)
			{
				t.OnDeserialize();
				_waitable[i++] = t.WaitableHandle;
			}
		}

		public void Pause()
		{
			_needToResume.Reset();
		}

		public void Resume()
		{
			_needToResume.Set();
		}

		/// <summary>
		///     Запустить задачу
		/// </summary>
		public void Start()
		{
			_worker.Start();
		}

		/// <summary>
		///     Остановить задачу
		/// </summary>
		public void Stop()
		{
			Logger.DebugFormat("Terminating worker thread.");
			_worker.Abort();
		}

		/// <summary>
		///     десериализация данных в класс.
		/// </summary>
		/// <typeparam name="T">тип в который пытаемся десиреализовать.</typeparam>
		/// <param name="document">документ.</param>
		/// <returns>десериализованный класс.</returns>
		protected static T DeserializeData<T>(byte[] document) where T : class
		{
			try
			{
				var ser = new XmlSerializer(typeof(T));
				return
					(T)ser.Deserialize(new XmlTextReader(new StringReader(Encoding.GetEncoding("windows-1251").GetString(document))));
			}
			catch (Exception ex)
			{
				LogManager.GetLogger(typeof(BaseTask)).Error(ex);
				return null;
			}
		}

		/// <summary>
		///     сериализация класса в массив байт.
		/// </summary>
		protected static byte[] SerializeData<T>(T document) where T : class
		{
			try
			{
				var memoryStream = new MemoryStream();
				var xs = new XmlSerializer(typeof(T));
				var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.GetEncoding("windows-1251"));
				xs.Serialize(xmlTextWriter, document);
				memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
				return memoryStream.ToArray();
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		///     Проверить необходимость паузы.
		/// </summary>
		/// <param name="mustPause">Приостанавливать ли поток </param>
		protected bool CheckForPause(bool mustPause)
		{
			var shouldPause = !_needToResume.WaitOne(0, false);

			if (shouldPause && mustPause)
			{
				Logger.DebugFormat("Task paused.");

				_readyToPause.Set();
				_needToResume.WaitOne();
				_readyToPause.Reset();
			}

			return shouldPause;
		}

		/// <summary>
		///     Метод, в котором содержится функциональность задачи, выполняемая
		///     при каждом срабатывании триггеров.
		/// </summary>
		protected abstract void Execute();

		/// <summary>
		///     Рабочая функция потока с задачей
		/// </summary>
		protected virtual void ThreadFunc()
		{
			Logger.Debug("Task thread started.");

			// execution loop
			while (!_needToStopGraceful.WaitOne(0, false))
			{
				// дождаться события. Каждое вызванное ожидание необходимо
				// окружать признаком готовности приостановиться
				_readyToPause.Set();
				WaitHandle.WaitAny(_waitable);
				_readyToPause.Reset();

				// retry loop
				bool willRetry;
				do
				{
					willRetry = false;

					// обработать запрос на приостановку
					CheckForPause(true);

					Exception error = null;
					try
					{
						Execute();
						Logger.DebugFormat("Task completed successfully.");
					}
					catch (SqlException sqlEx)
					{
						error = sqlEx;

						// Другого способа нет. .State мало того, что документирован 
						// по-ублюдски, он к тому же == 0.
						if (sqlEx.Message == "SQL Server does not exist or access denied.")
						{
							willRetry = MustRetry;
						}
					}
					catch (Exception e)
					{
						error = e;
					}

					if (error != null)
					{
						Logger.Error(string.Format("{0}: error during task execution", Description), error);
						if (willRetry)
						{
							Logger.WarnFormat("Non-fatal error. Retrying in {0}", RetryDelay);
						}
					}

					// не повторять задачу, если хозяевА затребовали остановку
					willRetry &= !_needToStopGraceful.WaitOne(0, false);

					// выполнить необходимую задержку перед повторением 
					// только если не требуется приостановки потока
					if (willRetry && !CheckForPause(false))
					{
						_readyToPause.Set();
						Thread.Sleep(RetryDelay);
						_readyToPause.Reset();
					}
				}
				while (willRetry);
			}
		}

		private static byte CharToSixBit(char c)
		{
			byte b;
			if (c >= 'A' && c <= 'Z')
			{
				b = (byte)(c - 'A');
			}
			else if (c >= 'a' && c <= 'z')
			{
				b = (byte)(c - 'a' + 26);
			}
			else if (c >= '0' && c <= '9')
			{
				b = (byte)(c - '0' + 52);
			}
			else if (c == _sCharPlusSign)
			{
				b = 62;
			}
			else
			{
				b = 63;
			}

			return b;
		}

		private static char SixBitToChar(byte b)
		{
			char c;
			if (b < 26)
			{
				c = (char)(b + 'A');
			}
			else if (b < 52)
			{
				c = (char)(b - 26 + 'a');
			}
			else if (b < 62)
			{
				c = (char)(b - 52 + '0');
			}
			else if (b == 62)
			{
				c = _sCharPlusSign;
			}
			else
			{
				c = _sCharSlash;
			}

			return c;
		}
	}
}