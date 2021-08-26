using RestChild.Mobile.Domain;

namespace RestChild.Web.Models.NewBout
{
    /// <summary>
    ///     модель для ребёнка
    /// </summary>
    public class ChildModel : ViewModelBase<Account>
    {
        /// <summary>
        ///     конструктор
        /// </summary>
        public ChildModel() : base(new Account())
        {
        }

        /// <summary>
        ///     конструктор
        /// </summary>
        public ChildModel(Account data) : base(data)
        {
        }

        /// <summary>
        ///     активная закладка
        /// </summary>
        public string ActiveTab { get; set; }
    }
}
