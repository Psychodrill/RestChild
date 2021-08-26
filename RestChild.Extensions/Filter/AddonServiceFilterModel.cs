using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Extensions.Filter
{
    /// <summary>
    ///     поиск доп услуг.
    /// </summary>
    public class AddonServiceFilterModel
    {
        public AddonServiceFilterModel()
        {
            PageNumber = 1;
        }

        public CommonPagedList<AddonServices> Result { get; set; }
        public int PageNumber { get; set; }
        public long? HotelId { get; set; }
        public string HotelName { get; set; }
        public string Name { get; set; }
        public long? StateId { get; set; }
        public long? TypeId { get; set; }
        public ICollection<StateMachineState> States { get; set; }

        public ICollection<TypeOfService> Types { get; set; }
    }
}
