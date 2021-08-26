using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestChild.Domain;

namespace RestChild.Web.Models
{
	/// <summary>
	/// модель с файлами
	/// </summary>
	public class RequestFileModel : ViewModelBase<RequestFile>
	{
		public RequestFileModel(): this(new RequestFile())
		{
			
		}
		public RequestFileModel(RequestFile data) : base(data)
		{
		}
	}
}