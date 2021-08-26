using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace RestChild.Comon
{
    [Serializable]
    [DataContract(Name = "changePasswordModel")]
    public class ForceChangePasswordModelBase
    {
        //[DataMember(Name = "oldpassword", EmitDefaultValue = false)]
        //[Required(ErrorMessage = "Укажите старый пароль")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Пароль")]
        //public string OldPassword { get; set; }

        [DataMember(Name = "newpassword", EmitDefaultValue = false)]
        [Required(ErrorMessage = "Укажите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string NewPassword { get; set; }

        [DataMember(Name = "newpasswordrpt", EmitDefaultValue = false)]
        [Required(ErrorMessage = "Повторите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Повтор пароля")]
        public string NewPasswordRpt { get; set; }

        public bool FirstTimeAuth { get; set; }

        public string ReturnUrl { get; set; }
    }
}
