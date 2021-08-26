using System.Collections.Generic;

namespace RestChild.Web.Models
{
	/// <summary>
	///     типы файлов
	/// </summary>
	public class RequestFileTypeModel
	{
		/// <summary>
		///     тип файла
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		///     имя типа файлов.
		/// </summary>
		public string Name { get; set; }

        /// <summary>
        ///     заблокировано добавление файлов
        /// </summary>
        public bool DisableAddFiles { get; set; }

        /// <summary>
        ///     группировка по блокам
        /// </summary>
        public RequestFileTypeGrouping FileTypeGrouping { get; set; }

        /// <summary>
        ///     файлы
        /// </summary>
        public List<RequestFileModel> Files { get; set; }
	}
}
