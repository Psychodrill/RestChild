using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Common
{
	public class RequestDiff
	{
		public string ObjectName { get; set; }
		public string Action { get; set; }
		public ICollection<RequestDiffItem> Items { get; set; }
		public ICollection<string> Messages { get; set; } 

		public RequestDiff()
		{
			Items = new List<RequestDiffItem>();
			Messages = new List<string>();
		}

		public void Append(string fieldName, string oldFieldValue, string newFieldValue)
		{
			Items.Add(new RequestDiffItem(fieldName, oldFieldValue, newFieldValue));
		}

		public void AppendMessage(string message)
		{
			Messages.Add(message);
		}
	}

	public class RequestDiffItem
	{
		public RequestDiffItem(string fieldName, string oldFieldValue, string newFieldValue)
		{
			FieldName = fieldName;
			OldFieldValue = oldFieldValue;
			NewFieldValue = newFieldValue;
		}

		public string FieldName { get; set; }
		public string OldFieldValue { get; set; }
		public string NewFieldValue { get; set; }
	}
}