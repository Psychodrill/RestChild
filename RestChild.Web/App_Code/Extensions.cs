using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.Web.App_Code
{
	public static class Extensions
	{
		public static string ToGlyphicon(this bool data)
		{
			return data ? "glyphicon glyphicon-ok text-success" : "glyphicon glyphicon-remove text-danger";
		}

		public static string ToGlyphicon(this bool? data)
		{
			return data ?? false ? "glyphicon glyphicon-ok text-success" : "glyphicon glyphicon-remove text-danger";
		}

		public static string NotNeedTicketColor(long? notNeedTicketReasonId)
		{
			if (notNeedTicketReasonId == (long)NotNeedTicketReasonEnum.ComeSingly)
			{
				return "gray";
			}

			if (notNeedTicketReasonId == (long)NotNeedTicketReasonEnum.Hospitalized)
			{
				return "has-error";
			}

			if (notNeedTicketReasonId == (long)NotNeedTicketReasonEnum.NotCome)
			{
				return "has-warning";
			}

			if (notNeedTicketReasonId == (long)NotNeedTicketReasonEnum.LeftEarly)
			{
				return "has-success";
			}

			return string.Empty;
		}
	}
}
