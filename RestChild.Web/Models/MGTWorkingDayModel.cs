using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RestChild.Booking.Logic.Logic;

namespace RestChild.Web.Models
{
    /// <summary>
    ///     Модель рабочего дня для сетки записи на приём
    /// </summary>
    [Serializable]
    public class MGTWorkingDayModel
    {
        private DateTime date;

        public MGTWorkingDayModel()
        {
            Windows = new List<MGTWorkingDayWindowModel>();
        }

        /// <summary>
        ///     Уникальный идентификатор рабочего дня
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Дата
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать дату")]
        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                TimeInterval = MGTWorkingDayLogic.TimeIntervalOn(value);
            }
        }

        /// <summary>
        ///     Временной интервал
        /// </summary>
        [Required(ErrorMessage = "Необходимо задать рабочий интервал")]
        public short TimeInterval { get; set; }

        /// <summary>
        ///     Окна приёма посетителей
        /// </summary>
        [Required(ErrorMessage = "Необходимо задать окна")]
        public List<MGTWorkingDayWindowModel> Windows { get; set; }

        /// <summary>
        ///     Интервал до приёма
        /// </summary>
        [Required(ErrorMessage = "Необходимо заполнить интервал до приёма")]
        public long? SuoVisitTooEarly { get; set; }

        /// <summary>
        ///     Интервал после приёма
        /// </summary>
        [Required(ErrorMessage = "Необходимо заполнить интервал после приёма")]
        public long? SuoVisitTooLate { get; set; }

        /// <summary>
        ///     Удалён
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        ///     Текст сообщения об ошибке
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     Кол-во активных броней
        /// </summary>
        public long BookingCount { get; set; }
    }
}
