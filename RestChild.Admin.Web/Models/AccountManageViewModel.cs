using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RestChild.Domain;

namespace RestChild.Admin.Web.Models
{
	/// <summary>
	/// модель для работы с пользователем
	/// </summary>
	public class AccountManageViewModel : ViewModelBase<Account>
	{
		/// <summary>
		/// права привязанные к организации
		/// </summary>
		public IList<AccessRight> AccessRights { get; set; }

		/// <summary>
		/// Роли системы
		/// </summary>
		public IList<Role> AccessRoles { get; set; }

		/// <summary>
		/// права
		/// </summary>
		public IList<AccountRights> Rights { get; set; }

		/// <summary>
		/// Роли
		/// </summary>
		public IList<AccountRoles> Role { get; set; }


		public AccountManageViewModel() : base(new Account())
		{
			Role = new List<AccountRoles>();
			Rights = new List<AccountRights>();
		}

		public AccountManageViewModel(Account data) : base(data)
		{
			Role = (data.Roles ?? new List<AccountRoles>()).ToList();
			Rights = (data.Rights ?? new List<AccountRights>()).ToList();
		}

		public override Account BuildData()
		{
			var res = base.BuildData();
			res.Rights = Rights.Where(r => r.AccountId.HasValue).ToList();
			res.Roles = Role.ToList();
			return res;
		}
	}
}
