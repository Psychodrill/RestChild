using System.Collections.Generic;
using RestChild.Domain;
using RestChild.Mobile.Domain;
using Bout = RestChild.Mobile.Domain.Bout;

namespace RestChild.Web.Models.NewBout
{
    /// <summary>
    ///     модель для заезда
    /// </summary>
    public class ManageModel : ViewModelBase<Bout>
    {
        /// <summary>
        ///     конструктор
        /// </summary>
        public ManageModel() : base(new Bout())
        {
        }

        /// <summary>
        ///     конструктор
        /// </summary>
        public ManageModel(Bout data) : base(data)
        {
        }

        /// <summary>
        ///     активная закладка
        /// </summary>
        public string ActiveTab { get; set; }

        /// <summary>
        ///     ИД объекта отдыха
        /// </summary>
        public long? HotelId { get; set; }

        /// <summary>
        ///     Коллекция объектов отдыха
        /// </summary>
        public List<Camp> CampsCollection { get; set; }
    }
}
