using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestChild.Admin.Web.Models
{
   [Serializable]
   [DataContract(Name = "securitySettingsModel")]
   public class SecuritySettingsModel
   {
      [DataMember(Name = "SessionLifeTime", EmitDefaultValue = false)]
      [Required(ErrorMessage = "Укажите таймаут сессии")]
      [Display(Name = "Таймаут сессии (мин.)")]
      [Range(0, 1000)]
      public long SessionLifeTime { get; set; }

      [DataMember(Name = "maxCountUnsuccess", EmitDefaultValue = false)]
      [Required(ErrorMessage = "Укажите максимум неуспешных попыток авторизации")]
      [Display(Name = "Максимум неуспешных попыток авторизации")]
      [Range(0, 100)]
      public long MaxCountUnsuccess { get; set; }

      [DataMember(Name = "accountBlockSpan", EmitDefaultValue = false)]
      [Required(ErrorMessage = "Укажите время блокировки аккаунта")]
      [Display(Name = "Время блокировки аккаунта (мин.)")]
      [Range(0, 100)]
      public long AccountBlockSpan { get; set; }

      [DataMember(Name = "timeLogStorage", EmitDefaultValue = false)]
      [Required(ErrorMessage = "Укажите cрок хранения журнала безопасности")]
      [Display(Name = "Срок хранения журнала безопасности (дней)")]
      [Range(0, 1000)]
      public long TimeLogStorage { get; set; }

      [DataMember(Name = "minLenPassword", EmitDefaultValue = false)]
      [Required(ErrorMessage = "Укажите минимальное число знаков пароля")]
      [Display(Name = "Минимальное число знаков пароля")]
      [Range(0, 100)]
      public long MinLenPassword { get; set; }

      [DataMember(Name = "timeLifePassword", EmitDefaultValue = false)]
      [Required(ErrorMessage = "Укажите период действия пароля")]
      [Display(Name = "Период действия пароля (дни)")]
      [Range(0, 180)]
      public long TimeLifePassword { get; set; }

      [DataMember(Name = "timeLifeAccount", EmitDefaultValue = false)]
      [Required(ErrorMessage = "Укажите максимальное время неиспользования аккаунта")]
      [Display(Name = "Максимальное время неиспользования аккаунта (дни)")]
      [Range(0, 1000)]
      public long TimeLifeAccount { get; set; }

      [DataMember(Name = "errorMessage", EmitDefaultValue = false)]
      public string ErrorMessage { get; set; }
   }
}
