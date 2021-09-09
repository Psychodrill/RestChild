using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Castle.Core.Internal;

namespace RestChild.Web.Models
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
            return IsValid != null && IsValid.Value;
        }

        /// <summary>
        ///     получение текстовых ошибок формы
        /// </summary>
        public virtual string GetErrorDescription(string legend = null, bool htmlMode = true, bool closeBlock = true)
        {
            var props = GetType().GetProperties();
            var sb = new StringBuilder();
            if (htmlMode)
            {
                sb.AppendLine(BlockStart);
            }

            if (!string.IsNullOrEmpty(legend))
            {
                sb.AppendFormat(htmlMode ? LegendFormat : "{0}:\n", legend);
            }

            foreach (var propertyInfo in props)
            {
                if (propertyInfo.HasAttribute<DisplayAttribute>())
                {
                    var val = propertyInfo.GetValue(this) as string;
                    if (!string.IsNullOrEmpty(val))
                    {
                        var attribute = propertyInfo.GetAttribute<DisplayAttribute>();
                        sb.Append(htmlMode
                            ? $"{ParagraphStart} В поле \"{attribute.Description}\" обнаружена ошибка: {val};{ParagraphEnd}"
                            : $"\t* В поле \"{attribute.Description}\" обнаружена ошибка: {val};\n");
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
