using System;
using System.IO;
using RestChild.Web.Properties;

namespace RestChild.Web.Models
{
	public class FilesStatus
	{
		public string HandlerPath = Settings.Default.StorageRoot;

		public FilesStatus()
		{
		}

		public FilesStatus(string name, FileInfo fileInfo)
		{
			SetValues(name, fileInfo.Name, (int) fileInfo.Length, fileInfo.FullName);
		}

		public FilesStatus(string fileName, string realFileName, int fileLength, string fullPath)
		{
			SetValues(fileName, realFileName, fileLength, fullPath);
		}

		public string group { get; set; }
		public string name { get; set; }
		public string realname { get; set; }
		public string type { get; set; }
		public int size { get; set; }
		public string progress { get; set; }
		public string url { get; set; }
		public string thumbnail_url { get; set; }
		public string delete_url { get; set; }
		public string delete_type { get; set; }
		public string error { get; set; }

		private void SetValues(string fileName, string realFileName, int fileLength, string fullPath)
		{
			name = fileName;
			realname = realFileName;
			type = "image/png";
			size = fileLength;
			progress = "1.0";
			url = HandlerPath + "Upload.ashx?f=" + realFileName;
			delete_url = HandlerPath + "Upload.ashx?f=" + fileName;
			delete_type = "DELETE";

			string ext = Path.GetExtension(fullPath);

			double fileSize = ConvertBytesToMegabytes(new FileInfo(fullPath).Length);
			if (fileSize > 3 || !IsImage(ext)) thumbnail_url = "/Content/img/generalFile.png";
			else thumbnail_url = @"data:image/png;base64," + EncodeFile(fullPath);
		}

		private bool IsImage(string ext)
		{
			return ext == ".gif" || ext == ".jpg" || ext == ".png";
		}

		private string EncodeFile(string fileName)
		{
			return Convert.ToBase64String(File.ReadAllBytes(fileName));
		}

		private static double ConvertBytesToMegabytes(long bytes)
		{
			return (bytes/1024f)/1024f;
		}
	}
}