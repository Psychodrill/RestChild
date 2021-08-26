using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestChild.Web.Common
{
	/// <summary>
	/// удаление файла при закрытии
	/// </summary>
	public class FileStreamDeleteOnCloseResult : FileStreamResult
	{
		private const FileOptions FileFlagNoBuffering = (FileOptions)0x20000000;

		private string fileName = null;

		public FileStreamDeleteOnCloseResult(string filename, string contentType) : base(new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite, 512, FileOptions.DeleteOnClose | FileFlagNoBuffering), contentType)
		{
			fileName = filename;
		}

		/// <summary>
		/// запись файлв
		/// </summary>
		/// <param name="response"></param>
		protected override void WriteFile(HttpResponseBase response)
		{
			base.WriteFile(response);
			try
			{
				if (File.Exists(fileName))
				{
					File.Delete(fileName);
				}
			}
			catch { }
		}
	}
}
