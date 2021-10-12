using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models
{
    /// <summary>
    ///     модель записи на приём в МГТ
    /// </summary>
    public class BookingMosgorturReestrBooking
    {
        /// <summary>
        ///     Субкласс - ребенок
        /// </summary>
        public class Child
        {
            /// <summary>
            /// Фамилия
            /// </summary>
            public string LastName { get; set; }

            /// <summary>
            /// Имя
            /// </summary>
            public string FirstName { get; set; }

            /// <summary>
            /// Отчество
            /// </summary>
            public string MiddleName { get; set; }

            /// <summary>
            /// Нет отчества
            /// </summary>
            public bool NoMiddleName { get; set; }

            /// <summary>
            /// Льгота
            /// </summary>
            public string BenefitType { get; set; }

            /// <summary>
            /// Удален
            /// </summary>
            public bool IsDeleted { get; set; }

            /// <summary>
            /// Дата рождения
            /// </summary>
            public DateTime? DateOfBirth { get; set; }
        }

        public BookingMosgorturReestrBooking()
        {
            Children = new List<Child>();
            SlotsCount = 1;
        }

        /// <summary>
        ///     Идентифкатор записи
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Дата
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать дату")]
        [Display(Description = "Дата")]
        public DateTime Date { get; set; }

        /// <summary>
        ///     Время
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать время")]
        [Display(Description = "Время")]
        public TimeSpan Time { get; set; }

        /// <summary>
        ///     Цель визита
        /// </summary>
        [Required(ErrorMessage = "Необходимо задать цели визита")]
        [Display(Description = "Цель визита")]
        public long SelectedTarget { get; set; }

        /// <summary>
        ///     Дети
        /// </summary>
        public List<Child> Children { get; set; }

        /// <summary>
        ///     Код брони
        /// </summary>
        public string BookingCode { get; set; }

        /// <summary>
        ///     Фамилия
        /// </summary>
        [Required(ErrorMessage = "Необходимо ввести фамилию")]
        [Display(Description = "Фамилия")]
        public string LastName { get; set; }

        /// <summary>
        ///     Имя
        /// </summary>
        [Required(ErrorMessage = "Необходимо ввести имя")]
        [Display(Description = "Имя")]
        public string FirstName { get; set; }

        /// <summary>
        ///     Отчество
        /// </summary>
        [Display(Description = "Отчество")]
        public string MiddleName { get; set; }

        /// <summary>
        ///     Пол
        /// </summary>
        [Required(ErrorMessage = "Необходимо задать пол")]
        [Display(Description = "Пол")]
        public bool? Male { get; set; }

        /// <summary>
        ///     Дата рождения
        /// </summary>
        [Required(ErrorMessage = "Необходимо ввести дату рождения")]
        [Display(Description = "Дата рождения")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        ///     Телефон
        /// </summary>
        [Required(ErrorMessage = "Необходимо задать телефон")]
        [Display(Description = "Телефон")]
        public string Phone { get; set; }

        /// <summary>
        ///     СНИЛС
        /// </summary>
        [Required(ErrorMessage = "Необходимо ввести СНИЛС")]
        [Display(Description = "СНИЛС")]
        public string Snils { get; set; }

        /// <summary>
        ///     Кол-во слотов
        /// </summary>
        [Required(ErrorMessage = "Необходимо ввести кол-во слотов")]
        [Display(Description = "Кол-во слотов")]
        public int SlotsCount { get; set; }

        /// <summary>
        ///     Электронная почта
        /// </summary>
        [Display(Description = "Электронная почта")]
        public string Email { get; set; }

        /// <summary>
        ///     Нет отчества
        /// </summary>
        public bool NoMiddleName { get; set; }

        /// <summary>
        ///     Текст сообщения об ошибке
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     Отменено
        /// </summary>
        public bool Canceld { get; set; }

        /// <summary>
        ///     Идентификатор статуса
        /// </summary>
        public long? StatusId { get; set; }

        /// <summary>
        ///     наименование статуса
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        ///     Ссылка на историю
        /// </summary>
        public long? HistoryLinkId { get; set; }
        /// <summary>
        ///     Идентификатор отдела МГТ
        /// </summary>
        public long? DepartmentId { get; set; }
    }
}
