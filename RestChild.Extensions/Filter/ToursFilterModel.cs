using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Extensions.Filter
{
    /// <summary>
    ///     Фильтр реестра размещений
    /// </summary>
    public class ToursFilterModel
    {
        public ToursFilterModel()
        {
            PageNumber = 1;
        }

        /// <summary>
        ///     наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Год компании
        /// </summary>
        public long? YearOfRestId { get; set; }

        /// <summary>
        ///     больше чем текущий год
        /// </summary>
        public bool MoreThenSelectedYear { get; set; }

        /// <summary>
        ///     Идентификатор отеля
        /// </summary>
        public long? HotelId { get; set; }

        /// <summary>
        ///     Наименование отеля
        /// </summary>
        public string HotelName { get; set; }

        /// <summary>
        ///     Место отдыха
        /// </summary>
        public long? PlaceOfRestId { get; set; }

        /// <summary>
        ///     Время отдыха
        /// </summary>
        public long? TimeOfRestId { get; set; }

        /// <summary>
        ///     Наименование времени отдыха
        /// </summary>
        public string TimeOfRestName { get; set; }

        public long? StateId { get; set; }

        /// <summary>
        ///     Идентификатор цели обращения
        /// </summary>
        public long? TypeOfRestId { get; set; }

        /// <summary>
        ///     Результат выборки
        /// </summary>
        public CommonPagedList<Tour> Result { get; set; }

        /// <summary>
        ///     Номер страницы
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        ///     Размер страницы
        /// </summary>
        public int? PageSize { get; set; }

        public long? ContractId { get; set; }
        public bool ContractFiltered { get; set; }
        public int? TypeOfServiceId { get; set; }
        public bool TypeOfServiceInclude { get; set; }

        /// <summary>
        ///     группа ограничений
        /// </summary>
        public long? RestrictionGroupId { get; set; }

        /// <summary>
        ///     Доступ к колонке "Доп. место и стоимость" и фильтру "Только без доп. услуг.".
        /// </summary>
        public bool AccessAddonServices { get; set; }

        /// <summary>
        ///     Года отдыха
        /// </summary>
        public ICollection<YearOfRest> YearsOfRests { get; set; }

        /// <summary>
        ///     Цели обращения
        /// </summary>
        public ICollection<TypeOfRest> TypesOfRests { get; set; }

        /// <summary>
        ///     Регионы отдыха
        /// </summary>
        public ICollection<PlaceOfRest> PlacesOfRest { get; set; }

        /// <summary>
        ///     Статусы
        /// </summary>
        public ICollection<StateMachineState> States { get; set; }

        /// <summary>
        ///     Вид услуги
        /// </summary>
        public ICollection<TypeOfService> TypesOfService { get; set; }

        /// <summary>
        ///     группы ограничений
        /// </summary>
        public ICollection<RestrictionGroup> GroupRestrictions { get; set; }
    }
}
