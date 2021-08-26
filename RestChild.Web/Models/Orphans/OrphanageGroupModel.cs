using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using RestChild.Domain;

namespace RestChild.Web.Models.Orphans
{
    /// <summary>
    ///     Модель группы (потребности)
    /// </summary>
    public class OrphanageGroupModel : ViewModelBase<PupilGroup>
    {
        public OrphanageGroupModel() : base(new PupilGroup())
        {
        }

        public OrphanageGroupModel(PupilGroup group) : base(group)
        {
            HealthStatuses = group.PupilsHealthStatuses?.ToDictionary(ss => ss.Id.ToString(), ss => ss) ??
                             new Dictionary<string, PupilsHealthStatus>();
            RequestsForPeriodOfRest = group.Requests?.ToDictionary(ss => ss.Id.ToString(), sx => sx) ??
                                      new Dictionary<string, RequestForPeriodOfRest>();
            OrganisationName = group.Organization?.Name;
        }

        /// <summary>
        ///     Список годов потребности
        /// </summary>
        public IDictionary<string, string> YearsOfRest { get; set; }

        /// <summary>
        ///     Список форм отдыха и оздоровления
        /// </summary>
        public IDictionary<string, string> FormsOfRest { get; set; }

        /// <summary>
        ///     Информация о состоянии здоровья воспитанников
        /// </summary>
        public IDictionary<string, PupilsHealthStatus> HealthStatuses { get; set; }

        /// <summary>
        ///     Информация о состоянии здоровья воспитанников
        /// </summary>
        public IDictionary<string, RequestForPeriodOfRest> RequestsForPeriodOfRest { get; set; }

        /// <summary>
        ///     Каникулярные периоды
        /// </summary>
        public IDictionary<string, string> VacationPeriods { get; set; }

        /// <summary>
        ///     Статус
        /// </summary>
        public ViewModelState State { get; set; }

        /// <summary>
        ///     Статус в который будет переведено
        /// </summary>
        public string StateMachineActionString { get; set; }

        /// <summary>
        ///     Активная вкладка
        /// </summary>
        public string ActiveTab { get; set; }

        /// <summary>
        ///     Название приюта
        /// </summary>
        public string OrganisationName { get; set; }

        /// <summary>
        ///     Год потребности (с валидацией)
        /// </summary>
        [Required]
        [Range(0, long.MaxValue)]
        public long? YearOfRestId
        {
            get => Data.YearOfRestId;
            set => Data.YearOfRestId = value;
        }

        /// <summary>
        ///     Форма отдыха и оздоровления (с валидацией)
        /// </summary>
        [Required]
        [Range(0, long.MaxValue)]
        public long? FormOfRestId
        {
            get => Data.FormOfRestId;
            set => Data.FormOfRestId = value;
        }

        /// <summary>
        ///     Существует ли квота
        /// </summary>
        public bool HasLimit { get; set; }

        /// <summary>
        ///     сбор данных
        /// </summary>
        public override PupilGroup BuildData()
        {
            var res = base.BuildData();
            res.Name = res.Name ?? string.Empty;
            res.PupilsHealthStatuses = HealthStatuses?.Values.ToList() ?? new List<PupilsHealthStatus>();
            res.Requests = RequestsForPeriodOfRest?.Values.ToList() ?? new List<RequestForPeriodOfRest>();
            return res;
        }
    }
}
