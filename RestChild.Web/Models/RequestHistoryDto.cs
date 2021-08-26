using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RestChild.Comon;
using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class RequestHistoryDto
	{
		public RequestHistoryDto(HistoryRequest request)
		{
			AccountName = request.NullSafe(x => x.Account.Name);
			AccountEmail = request.NullSafe(x => x.Account.Email);
			AccountPhone = request.NullSafe(x => x.Account.Phone);
			OperationName = request.Operation;
			DateTime = request.OperationDate.ToString("dd.MM.yyyy HH:mm");
			Operation = request.Operation;
		}

		public string OperationName { get; set; }
		public string DateTime { get; set; }
		public string AccountName { get; set; }
		public string AccountEmail { get; set; }
		public string AccountPhone { get; set; }
		public string Operation { get; set; }
	}
}