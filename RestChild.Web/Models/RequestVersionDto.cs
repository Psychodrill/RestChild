using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class RequestVersionDto
	{
		public long RequestId { get; set; }
		public IEnumerable<RequestHistoryDto> History { get; set; }
	}
}