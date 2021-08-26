using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using RestChild.Comon.Dto.Booking;
using RestChild.Comon.Enumeration;
using RestChild.Domain;
using TimeOfRest = RestChild.Domain.TimeOfRest;

namespace RestChild.Web.Models
{
	/// <summary>
	///     поиск и бронирование места отдыха.
	/// </summary>
	public class SelectPlaceModel
	{
		private BookingVariationPlacementResponse _placement;
		private string _placementJson;
		private ResultSearch _searchResult;
		private string _searchResultJson;
		private Hotel _selectedItem;
		private string _selectedItemJson;

		public SelectPlaceModel()
		{
			ErrorMessage = string.Empty;
		}

		public Request Request { get; set; }

		/// <summary>
		/// ИД заявления
		/// </summary>
		public long? RequestId { get; set; }

		/// <summary>
		/// есть ошибка
		/// </summary>
		public bool IsError { get; set; }

		/// <summary>
		/// текст ошибки.
		/// </summary>
		public string ErrorMessage { get; set; }

		/// <summary>
		/// Наименование банка.
		/// </summary>
		public string BankName { get; set; }

		/// <summary>
		/// Счет в банке
		/// </summary>
		public string BankAccount { get; set; }

		/// <summary>
		/// БИК
		/// </summary>
		public string BankBik { get; set; }

		/// <summary>
		/// ИНН
		/// </summary>
		public string BankInn { get; set; }

		/// <summary>
		/// КПП
		/// </summary>
		public string BankKpp { get; set; }

		/// <summary>
		/// Номер карты
		/// </summary>
		public string BankCardNumber { get; set; }

		/// <summary>
		/// Корр счет
		/// </summary>
		public virtual string BankCorr { get; set; }

		/// <summary>
		/// Получатель платежа фамилия
		/// </summary>
		public virtual string BankLastName { get; set; }

		/// <summary>
		/// Получатель платежа имя
		/// </summary>
		public virtual string BankFirstName { get; set; }

		/// <summary>
		/// Получатель платежа отчество
		/// </summary>
		public virtual string BankMiddleName { get; set; }

		/// <summary>
		///     шаг выбора места.
		/// </summary>
		public SelectPlaceStepEnum SelectPlaceStep { get; set; }

		/// <summary>
		///     Изменение шага.
		/// </summary>
		public string ActionStep { get; set; }

		/// <summary>
		///     Вид отдыха.
		/// </summary>
		public long TypeOfRestId { get; set; }

		/// <summary>
		///     Вид отдыха.
		/// </summary>
		public string TypeOfRestError { get; set; }

		/// <summary>
		///     получить список видов отдыха.
		/// </summary>
		public List<TypeOfRest> TypeOfRests { get; set; }

		/// <summary>
		///     Времена отдыха.
		/// </summary>
		public List<TimeOfRest> TimeOfRests { get; set; }

		/// <summary>
		///     Года кампании.
		/// </summary>
		public List<YearOfRest> YearOfRests { get; set; }

		/// <summary>
		///     тематики смены.
		/// </summary>
		public List<SubjectOfRest> SubjectOfRests { get; set; }

		/// <summary>
		///     время отдыха.
		/// </summary>
		public long? TimeOfRestId { get; set; }

		/// <summary>
		///     Заезд.
		/// </summary>
		public long? TourId { get; set; }

		/// <summary>
		///     количество детей.
		/// </summary>
		public int CountChildren { get; set; }

		/// <summary>
		///     ключ отеля.
		/// </summary>
		public string HotelKey { get; set; }

		/// <summary>
		///  Индекс размещения.
		/// </summary>
		public int? IndexPlacement { get; set; }

		/// <summary>
		///     Вид отдыха.
		/// </summary>
		public string CountChildrenError { get; set; }

		/// <summary>
		///     количество сопровождающих.
		/// </summary>
		public int CountAttendant { get; set; }

		/// <summary>
		///     Вид отдыха.
		/// </summary>
		public string CountAttendantError { get; set; }

		/// <summary>
		///     Год кампании
		/// </summary>
		public long YearOfRestId { get; set; }

		/// <summary>
		///     место отдыха
		/// </summary>
		public long? PlaceOfRestId { get; set; }

		/// <summary>
		///     список мест отдыха.
		/// </summary>
		public List<PlaceOfRest> PlaceOfRests { get; set; }

		/// <summary>
		///     запрос на бронирование
		/// </summary>
		public BookingRequest BookingRequest { get; set; }

		/// <summary>
		///     результаты поиска заявлений.
		/// </summary>
		public ResultSearch SearchResult
		{
			get
			{
				if (_searchResult != null)
				{
					return _searchResult;
				}

				return !string.IsNullOrEmpty(_searchResultJson)
						? JsonConvert.DeserializeObject<ResultSearch>(Encoding.UTF8.GetString(Convert.FromBase64String(_searchResultJson)))
						: null;
			}
			set
			{
				_searchResult = value;
				_searchResultJson = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)));
			}
		}

		/// <summary>
		///     результаты поиска заявлений.
		/// </summary>
		public string SearchResultJson
		{
			get { return _searchResultJson; }
			set
			{
				_searchResultJson = value;
				_searchResult = !string.IsNullOrEmpty(value)
					? JsonConvert.DeserializeObject<ResultSearch>(Encoding.UTF8.GetString(Convert.FromBase64String(value)))
					: null;
			}
		}

		/// <summary>
		///     вариант размещения.
		/// </summary>
		public BookingVariationPlacementResponse Placement
		{
			get
			{

				if (_placement != null)
				{
					return _placement;
				}

				return !string.IsNullOrEmpty(_placementJson)
						? JsonConvert.DeserializeObject<BookingVariationPlacementResponse>(Encoding.UTF8.GetString(Convert.FromBase64String(_placementJson)))
						: null;
			}
			set
			{
				_placement = value;
				_placementJson = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)));
			}
		}

		/// <summary>
		///     вариант размещения.
		/// </summary>
		public string PlacementJson
		{
			get { return _placementJson; }
			set
			{
				_placementJson = value;
				_placement = !string.IsNullOrEmpty(value)
					? JsonConvert.DeserializeObject<BookingVariationPlacementResponse>(Encoding.UTF8.GetString(Convert.FromBase64String(value)))
					: null;
			}
		}

		/// <summary>
		///     результаты поиска заявлений.
		/// </summary>
		public Hotel SelectedItem
		{
			get
			{
				if (_selectedItem != null)
				{
					return _selectedItem;
				}

				return !string.IsNullOrEmpty(_selectedItemJson)
						? JsonConvert.DeserializeObject<Hotel>(Encoding.UTF8.GetString(Convert.FromBase64String(_selectedItemJson)))
						: null;
			}
			set
			{
				_selectedItem = value;
				_selectedItemJson = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)));
			}
		}

		/// <summary>
		///     результаты поиска заявлений.
		/// </summary>
		public string SelectedItemJson
		{
			get { return _selectedItemJson; }
			set
			{
				_selectedItemJson = value;
				_selectedItem = !string.IsNullOrEmpty(value)
					? JsonConvert.DeserializeObject<Hotel>(Encoding.UTF8.GetString(Convert.FromBase64String(value)))
					: null;
			}
		}

		/// <summary>
		///     Номер страницы
		/// </summary>
		public int PageNumber { get; set; }

		/// <summary>
		///     Начальная страница
		/// </summary>
		public int PageStart { get; set; }

		/// <summary>
		///     Последняя страница в пагинаторе
		/// </summary>
		public int PageEnd { get; set; }

		/// <summary>
		///     Последняя страница
		/// </summary>
		public int PageLast { get; set; }

		/// <summary>
		///     проверка модели.
		/// </summary>
		public bool CheckModel()
		{
			var isValid = true;
			if (SelectPlaceStep == SelectPlaceStepEnum.FirstSelectTypeTimeAndPlace && Request == null)
			{
				if (TypeOfRestId <= 0)
				{
					TypeOfRestError = "Не указан вид отдыха";
					isValid = false;
				}
				else
				{
					TypeOfRestError = string.Empty;
				}

				if (CountChildren <= 0 && (TypeOfRestId != (long)TypeOfRestEnum.YouthRestOrphanCamps && TypeOfRestId != (long)TypeOfRestEnum.YouthRestCamps))
				{
					CountChildrenError = "Не указано число детей льготных категорий";
					isValid = false;
				}
				else
				{
					CountChildrenError = string.Empty;
				}

				if (Booking.Logic.Booking.TypeOfRestDecode.ContainsKey(TypeOfRestId))
				{

					var typeOfRest = TypeOfRests.FirstOrDefault(t => t.Id == TypeOfRestId);

					if (CountAttendant > 0 && !(typeOfRest?.NeedAccomodation ?? false))
					{
						CountAttendantError = "Нельзя указывать сопровождающих для индивидуального отдыха";
						isValid = false;
					}

					if (CountAttendant <= 0 && (typeOfRest?.NeedAccomodation ?? true))
					{
						CountAttendantError = "При совместном отдыхе должен быть хотя бы один сопровождающий";
						isValid = false;
					}

					if (CountAttendant > 1 &&
					    (typeOfRest?.Id == (long) TypeOfRestEnum.RestWithParentsComplex ||
					     typeOfRest?.Id == (long) TypeOfRestEnum.RestWithParentsInvalid))
					{
						if (CountChildren < 2)
						{
							CountAttendantError = "Для двоих сопровождающих должно быть более 2 детей льготных категорий";
							isValid = false;
						}
					} else if (CountAttendant > 1 && CountChildren < 4 && (typeOfRest?.NeedAccomodation ?? true))
					{
						CountAttendantError = "Для двоих сопровождающих должно быть более 4 детей льготных категорий";
						isValid = false;
					}
				}
			}

			return isValid;
		}
	}
}
