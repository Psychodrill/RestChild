using System.Collections.Generic;
using RestChild.Comon.Dto;
using RestChild.Domain;
using RestChild.Extensions.Filter;

namespace RestChild.Web.Models.BaseRegistry
{
	/// <summary>
	///     фильтр поиска по БР
	/// </summary>
	public class BaseRegistrySearch
	{
		/// <summary>
		///     номер запроса в БР
		/// </summary>
		public string RegistryNumber { get; set; }

		/// <summary>
		///     строка поиска
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		///     строка поиска
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		///     строка поиска
		/// </summary>
		public string MiddleName { get; set; }

		/// <summary>
		///     номер страницы
		/// </summary>
		public int PageNumber { get; set; }

		/// <summary>
		///     результат поиска.
		/// </summary>
		public CommonPagedList<BaseRegistryCheckResult> Result { get; set; }

		/// <summary>
		/// запрос на проверку в базовом регистре.
		/// </summary>
		public BenefitCheckRequest RequestBlock { get; set; }

		/// <summary>
		/// Строка действия
		/// </summary>
		public string ActionString { get; set; }

		/// <summary>
		/// список документов
		/// </summary>
		public List<DocumentType> DocumentTypes { get; set; }
	}
}
