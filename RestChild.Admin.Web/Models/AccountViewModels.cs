using System.ComponentModel.DataAnnotations;

namespace RestChild.Admin.Web.Models
{
	using RestChild.Comon;
	using RestChild.Comon.Dto;

	public class LoginViewModel : LoginModelBase
	{
		[Display(Name = "Запомнить")]
		public bool RememberMe { get; set; }
	}
}
