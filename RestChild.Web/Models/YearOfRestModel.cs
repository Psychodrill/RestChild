using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Web.Models
{
    public class YearOfRestModel : ViewModelBase<YearOfRest>
    {
        public YearOfRestModel() : base(new YearOfRest())
        {
        }

        public YearOfRestModel(YearOfRest data) : base(data)
        {
        }

        /// <summary>
        ///     Списки квот
        /// </summary>
        public Dictionary<string, LimitEditModel> Limits { get; set; }

        /// <summary>
        ///     Усредненная цена за отдых
        /// </summary>
        public Dictionary<string, AverageRestPrice> AveragePrices { get; set; }
    }
}
