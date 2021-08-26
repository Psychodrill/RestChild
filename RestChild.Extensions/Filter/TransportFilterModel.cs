using System;
using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Extensions.Filter
{
    public class TransportFilterModel
    {
        public TransportFilterModel()
        {
            PageNumber = 1;
        }


        public CommonPagedList<TransportInfo> Result { get; set; }


        public int PageNumber { get; set; }

        #region Параметры фильтра

        /// <summary>
        ///     Город отправления
        /// </summary>
        public long? DepartureCityId { get; set; }

        /// <summary>
        ///     Город прибытия
        /// </summary>
        public long? ArrivalCityId { get; set; }

        /// <summary>
        ///     Год кампании
        /// </summary>
        public long? YearOfRestId { get; set; }

        /// <summary>
        ///     Статус
        /// </summary>
        public long? StateId { get; set; }

        /// <summary>
        ///     Начало интервала времени отправления
        /// </summary>
        public DateTime? DateOfDepartureBegin { get; set; }

        /// <summary>
        ///     Конец интервала времени отправления
        /// </summary>
        public DateTime? DateOfDepartureEnd { get; set; }

        /// <summary>
        ///     Признак ввода данных пользователем в фильтр (пользователь нажал кнопку "Поиск" или перешел на любую страницу)
        /// </summary>
        public bool IsFilterSet { get; set; }

        #endregion

        #region Справочники

        public ICollection<City> Cities { get; set; }
        public ICollection<StateMachineState> States { get; set; }
        public ICollection<YearOfRest> YearsOfRest { get; set; }

        #endregion
    }
}
