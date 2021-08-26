using System.ComponentModel.DataAnnotations;

namespace RestChild.Web.Models
{
    /// <summary>
    ///     время интервала
    /// </summary>
    public class MGTWorkingDayWindowTimeIntervalModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Необходимо указать время начала интервала")]
        public string TimeFromString { get; set; }

        [Required(ErrorMessage = "Необходимо указать время окончания интервала")]
        public string TimeToString { get; set; }

        public bool IsDeleted { get; set; }
    }
}
