using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace RestChild.Comon.Dto
{
    [Serializable]
    [DataContract(Name = "loginModel")]
    public class LoginModelBase
    {
        [DataMember(Name = "userName", EmitDefaultValue = false)]
        [Required(ErrorMessage = "Укажите логин")]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [DataMember(Name = "password", EmitDefaultValue = false)]
        [Required(ErrorMessage = "Укажите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
