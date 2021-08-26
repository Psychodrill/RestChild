using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Castle.Core.Internal;

namespace RestChild.Admin.Web.Models
{
	[DataContract]
	public class ViewModelBase<TModel>
	{
		#region Типовые сообщения об ошибках

		protected const string RequaredField = "Поле обязательно для заполнения";

		protected const string NewLine = "<br/>";
		protected const string BlockStart = "<ul class=\"error-ul-block\">";
		protected const string ParagraphStart = "<li>";
		protected const string ParagraphEnd = "</li>";
		protected const string BlockEnd = "</ul>";
		protected const string LegendFormat = "<legend>{0}</legend>\n";

		#endregion

		public ViewModelBase(TModel data)
		{
			Data = data;
			IsValid = null;
			ErrorMessage = string.Empty;
		}

		[DataMember(Name = "data", EmitDefaultValue = false)]
		public TModel Data { get; protected set; }

		[DataMember(Name = "isValid", EmitDefaultValue = false)]
		public bool? IsValid { get; set; }

		[DataMember(Name = "errorMessage", EmitDefaultValue = false)]
		public string ErrorMessage { get; set; }

		public virtual TModel BuildData()
		{
			return Data;
		}

		public virtual bool CheckModel(string action = null)
		{
			IsValid = true;
			ErrorMessage = GetErrorDescription();
			return IsValid.Value;
		}

		/// <summary>
		/// получение текстовы ошибки для формы.
		/// </summary>
		public virtual string GetErrorDescription(string legend = null, bool htmlMode = true, bool closeBlock = true)
		{
			PropertyInfo[] propertys = GetType().GetProperties();
			var sb = new StringBuilder();
			if (htmlMode)
			{
				sb.AppendLine(BlockStart);
			}
			if (!string.IsNullOrEmpty(legend))
			{
				sb.AppendFormat(htmlMode ? LegendFormat : "{0}:\n", legend);
			}

			foreach (PropertyInfo propertyInfo in propertys)
			{
				if (propertyInfo.HasAttribute<DisplayAttribute>())
				{
					var val = propertyInfo.GetValue(this) as string;
					if (!string.IsNullOrEmpty(val))
					{
						var attribute = propertyInfo.GetAttribute<DisplayAttribute>();
						if (htmlMode)
						{
							sb.AppendFormat("{2} В поле \"{0}\" обнаружена ошибка: {1};{3}", attribute.Description, val, ParagraphStart,
								ParagraphEnd);
						}
						else
						{
							sb.AppendFormat("\t* В поле \"{0}\" обнаружена ошибка: {1};\n", attribute.Description, val);
						}
					}
				}
			}

			if (htmlMode && closeBlock)
			{
				sb.AppendLine(BlockEnd);
			}

			return sb.ToString();
		}
	}
}
